param (
    [switch]$UpdateGit,
    [switch]$BuildAnalyzers,
    [switch]$BuildMonoGame
)

# ---------------
# BEGIN VARIABLES
# ---------------

$AnalyzerVersion = "1.1.3"
$MonoGameBuildVersion = "1.3.3.7-cpt";


$WorkingDirectory = Get-Location;
Set-Location $PSScriptRoot;

$SolutionDirectory = "$($PSScriptRoot)/.." | Resolve-Path
$AnalyzerDirectory = "$($SolutionDirectory)/analyzers" | Resolve-Path
$NugetDirectory = "$($SolutionDirectory)/.nuget"
$MonoGameDirectory = "$($PSScriptRoot)/../libraries/MonoGame" | Resolve-Path;

Write-Host "Installing Mantis..." -ForegroundColor Gray
Write-Host "UpdateGit: $UpdateGit" -ForegroundColor ($($UpdateGit ? 'Green' : 'Red'))
Write-Host "BuildAnalyzers: $BuildAnalyzers, $AnalyzerVersion" -ForegroundColor ($($BuildAnalyzers ? 'Green' : 'Red'))
Write-Host "BuildMonoGame: $BuildMonoGame, $MonoGameBuildVersion" -ForegroundColor ($($BuildMonoGame ? 'Green' : 'Red'))

if($UpdateGit)
{
    Write-Host "Updating Git submodules..." -ForegroundColor Gray
    git submodule update --init --recursive
}

if($BuildAnalyzers)
{
  Write-Host "Building Analyzers..." -ForegroundColor Gray

  if (-not (Test-Path -Path $NugetDirectory)) {
    New-Item -Path $NugetDirectory -ItemType Directory | Out-Null
  }
  
  $NugetDirectory = $NugetDirectory | Resolve-Path

  # Build Analyzer package
  dotnet build -c Release "$($AnalyzerDirectory)/Mantis.Analyzer.Core.Common"
  dotnet pack -o $NugetDirectory /p:Version=$AnalyzerVersion "$($AnalyzerDirectory)/Mantis.Analyzer.Core.Common"

@'
<!-- Generated via Mantis/scripts/install.ps1 -->
<Project>

  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  </PropertyGroup>

  <ItemGroup>
    <!-- Mantis packages are added to a local nuget feed via the 'Mantis/nuget.config' file -->
    <PackageReference Include="Mantis.Analyzer.Core.Common" Version="{0}" />
  </ItemGroup>

</Project>
'@ -f $AnalyzerVersion | Out-File -FilePath "$($SolutionDirectory)/Mantis.Core.Common.g.targets"

@'
<!-- Generated via Mantis/scripts/install.ps1 -->
<configuration>
  <packageSources>
    <add key="MantisPackages" value="{0}" />
  </packageSources>
</configuration>
'@ -f $NugetDirectory | Out-File -FilePath "$($SolutionDirectory)/nuget.config"
}

if($BuildMonoGame)
{
  Write-Host "Building MonoGame..." -ForegroundColor Gray

  # Build MonoGame
  Set-Location $MonoGameDirectory;

  if((Test-Path "./build.cake") -eq $true)
  {
      dotnet tool restore
      dotnet cake --build-version $MonoGameBuildVersion

      $MonoGameBuildVersion = "$($MonoGameBuildVersion)-develop"
  }
  else {
      dotnet run --project "$($MonoGameDirectory)/build/Build.csproj" -- "--build-version" $MonoGameBuildVersion

      # Why isn't this included in Build.csproj tho?
      if($IsWindows)
      {
        dotnet pack -o "$($MonoGameDirectory)/Artifacts/NuGet" /p:Version=$MonoGameBuildVersion "$($MonoGameDirectory)/Tools/MonoGame.Content.Builder.Editor.Launcher/MonoGame.Content.Builder.Editor.Launcher.Windows.csproj"
      }
      if($IsLinux)
      {
        dotnet pack -o "$($MonoGameDirectory)/Artifacts/NuGet" /p:Version=$MonoGameBuildVersion "$($MonoGameDirectory)/Tools/MonoGame.Content.Builder.Editor.Launcher/MonoGame.Content.Builder.Editor.Launcher.Windows.csproj"
      }
  }

  git reset --hard HEAD

  Set-Location $WorkingDirectory;

  Write-Host "Uninstalling Old MonoGame Tools..." -ForegroundColor Gray
  # Uninstall old tools (if any)
  if (dotnet tool list | Select-String -Quiet "dotnet-mgcb") {
    dotnet tool uninstall --create-manifest-if-needed dotnet-mgcb
  }

  if (dotnet tool list | Select-String -Quiet "dotnet-mgcb-editor-windows") {
    dotnet tool uninstall --create-manifest-if-needed dotnet-mgcb-editor-windows
  }

  if (dotnet tool list | Select-String -Quiet "dotnet-mgcb-editor-linux") {
    dotnet tool uninstall --create-manifest-if-needed dotnet-mgcb-editor-linux
  }

  if (dotnet tool list | Select-String -Quiet "dotnet-mgcb-editor") {
    dotnet tool uninstall --create-manifest-if-needed dotnet-mgcb-editor
  }

  # https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create
  # https://learn.microsoft.com/en-us/dotnet/core/tools/local-tools-how-to-use
  dotnet tool install --create-manifest-if-needed --version $MonoGameBuildVersion --add-source "$($MonoGameDirectory)/Artifacts/NuGet" dotnet-mgcb
  dotnet tool install --create-manifest-if-needed --version $MonoGameBuildVersion --add-source "$($MonoGameDirectory)/Artifacts/NuGet" dotnet-mgcb-editor

  if($IsWindows)
  {
      dotnet tool install --create-manifest-if-needed --version $MonoGameBuildVersion --add-source "$($MonoGameDirectory)/Artifacts/NuGet" dotnet-mgcb-editor-windows
  }

  if($IsLinux)
  {
      dotnet tool install --create-manifest-if-needed --version $MonoGameBuildVersion --add-source "$($MonoGameDirectory)/Artifacts/NuGet" dotnet-mgcb-editor-windows
  }
}
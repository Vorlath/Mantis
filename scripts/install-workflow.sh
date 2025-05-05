#!/bin/bash

## Stripped down version of other install scripts
## Designed for github workflow - to speed up the
## average run time.

# ---------------
# BEGIN VARIABLES
# ---------------

MONOGAME_BUILD_VERSION="1.3.3.7-cpt"

WORKING_DIRECTORY=$(pwd)
SOLUTION_DIRECTORY=$(dirname $(dirname "$(realpath "$0")"))
ANALYZER_DIRECTORY="$(realpath "$SOLUTION_DIRECTORY/analyzers")"
NUGET_DIRECTORY="$SOLUTION_DIRECTORY/.nuget"
MONOGAME_DIRECTORY=$(realpath "$SOLUTION_DIRECTORY/libraries/MonoGame")

# ----------------------
# BEGIN PACKAGE BUILDING
# ----------------------

# Confirm .nuget directory exists
if [ ! -d "$NUGET_DIRECTORY" ]; then
    mkdir -p "$NUGET_DIRECTORY"
fi

# Build Analyzer package
dotnet build -c Release "$ANALYZER_DIRECTORY/Mantis.Analyzer.Core.Common"
dotnet pack -o "$NUGET_DIRECTORY" /p:Version="$ANALYZER_VERSION" "$ANALYZER_DIRECTORY/Mantis.Analyzer.Core.Common"
echo $ANALYZER_DIRECTORY

# ---------------------------
# BEGIN MONOGAME INSTALLATION
# ---------------------------

# Build MonoGame

cd "$MONOGAME_DIRECTORY"

dotnet pack -o "$MONOGAME_DIRECTORY/artifacts/NuGet" \
    /p:Version="$MONOGAME_BUILD_VERSION" \
    "$MONOGAME_DIRECTORY/Tools/MonoGame.Content.Builder/MonoGame.Content.Builder.csproj"

git reset --hard HEAD

cd "$WORKING_DIRECTORY"


# Install new tools
dotnet tool install --local --create-manifest-if-needed --add-source "$MONOGAME_DIRECTORY/artifacts/NuGet" dotnet-mgcb

# ---------------------
# BEGIN FILE GENERATION
# ---------------------

cat <<EOF > "$SOLUTION_DIRECTORY/Mantis.Core.Common.g.targets"
<!-- Generated via Mantis/scripts/install.sh -->
<Project>

  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  </PropertyGroup>

  <ItemGroup>
    <!-- Mantis packages are added to a local nuget feed via the 'Mantis/nuget.config' file -->
    <PackageReference Include="Mantis.Analyzer.Core.Common" Version="$ANALYZER_VERSION" />
  </ItemGroup>

</Project>
EOF

cat <<EOF > "$SOLUTION_DIRECTORY/nuget.config"
<!-- Generated via Mantis/scripts/install.sh -->
<configuration>
  <packageSources>
    <add key="MantisPackages" value="$NUGET_DIRECTORY" />
  </packageSources>
</configuration>
EOF

echo "CREATED FILES"
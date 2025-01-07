#!/bin/bash

# ---------------
# BEGIN VARIABLES
# ---------------

MONOGAME_BUILD_VERSION="1.3.3.7-cpt"

WORKING_DIRECTORY=$(pwd)
SCRIPT_DIR=$(dirname "$(realpath "$0")")
MONOGAME_DIRECTORY=$(realpath "$SCRIPT_DIR/../libraries/MonoGame")

# Update submodules
cd "$SCRIPT_DIR/.."
git submodule update --init --recursive

# ---------------------------
# BEGIN MONOGAME INSTALLATION
# ---------------------------

# Build MonoGame
cd "$MONOGAME_DIRECTORY"

if [[ -f "./build.cake" ]]; then
    dotnet tool restore
    dotnet cake --build-version "$MONOGAME_BUILD_VERSION"

    MONOGAME_BUILD_VERSION="${MONOGAME_BUILD_VERSION}-develop"
else
    dotnet run --project "$MONOGAME_DIRECTORY/build/Build.csproj" -- "--build-version" "$MONOGAME_BUILD_VERSION"

    # Why isn't this included in Build.csproj tho?
    dotnet pack -o "$MONOGAME_DIRECTORY/Artifacts/NuGet" \
        /p:Version="$MONOGAME_BUILD_VERSION" \
        "$MONOGAME_DIRECTORY/Tools/MonoGame.Content.Builder.Editor.Launcher/MonoGame.Content.Builder.Editor.Launcher.Windows.csproj"
fi

git reset --hard HEAD

cd "$WORKING_DIRECTORY"

# Uninstall old tools (if any)
dotnet tool uninstall dotnet-mgcb
dotnet tool uninstall dotnet-mgcb-editor-windows
dotnet tool uninstall dotnet-mgcb-editor

# Install new tools
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/Artifacts/NuGet" dotnet-mgcb
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/Artifacts/NuGet" dotnet-mgcb-editor-windows
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/Artifacts/NuGet" dotnet-mgcb-editor
#!/bin/bash

# ---------------
# BEGIN VARIABLES
# ---------------

MONOGAME_BUILD_VERSION="1.3.3.7-cpt"

WORKING_DIRECTORY=$(pwd)
SCRIPT_DIR=$(dirname "$(realpath "$0")")
MONOGAME_DIRECTORY=$(realpath "$SCRIPT_DIR/../libraries/MonoGame")


# Default flag value
GIT_UPDATE=true

# Parse CLI arguments for a specific flag (e.g., --bypass)
for arg in "$@"; do
  if [[ "$arg" == "--no-git-update" ]]; then
    GIT_UPDATE=false
  fi
done

# Update submodules
if [ "$GIT_UPDATE" = true ]; then
    cd "$SCRIPT_DIR/.."
    git submodule update --init --recursive
fi

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
    echo run --project "$MONOGAME_DIRECTORY/build/Build.csproj" -- "--build-version" "$MONOGAME_BUILD_VERSION"
    dotnet run --project "$MONOGAME_DIRECTORY/build/Build.csproj" -- "--build-version" "$MONOGAME_BUILD_VERSION"

    # Why isn't this included in Build.csproj tho?
    dotnet pack -o "$MONOGAME_DIRECTORY/artifacts/NuGet" \
        /p:Version="$MONOGAME_BUILD_VERSION" \
        "$MONOGAME_DIRECTORY/Tools/MonoGame.Content.Builder.Editor.Launcher/MonoGame.Content.Builder.Editor.Launcher.Linux.csproj"
fi

git reset --hard HEAD

cd "$WORKING_DIRECTORY"

# Uninstall old tools (if any)
dotnet tool uninstall dotnet-mgcb
dotnet tool uninstall dotnet-mgcb-editor-linux
dotnet tool uninstall dotnet-mgcb-editor

# Install new tools
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/artifacts/NuGet" dotnet-mgcb
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/artifacts/NuGet" dotnet-mgcb-editor-linux
dotnet tool install --version "$MONOGAME_BUILD_VERSION" --add-source "$MONOGAME_DIRECTORY/artifacts/NuGet" dotnet-mgcb-editor
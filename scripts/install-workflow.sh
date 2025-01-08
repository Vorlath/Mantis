#!/bin/bash

## Stripped down version of other install scripts
## Designed for github workflow - to speed up the
## average run time.

# ---------------
# BEGIN VARIABLES
# ---------------

MONOGAME_BUILD_VERSION="1.3.3.7-cpt"

WORKING_DIRECTORY=$(pwd)
SCRIPT_DIR=$(dirname "$(realpath "$0")")
MONOGAME_DIRECTORY=$(realpath "$SCRIPT_DIR/../libraries/MonoGame")

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
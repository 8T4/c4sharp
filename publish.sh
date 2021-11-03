#!/bin/sh
VERSION="2.2.2"
NUGET_KEY="oy2fixxyzuqmc5w5eo2qcfyotdglykxufejegfb3rzxfgi"
NUGET_INDEX="https://api.nuget.org/v3/index.json"
BUILD_TYPE="Release"

dotnet build --configuration $BUILD_TYPE
dotnet pack --no-restore --configuration $BUILD_TYPE -p:PackageVersion=$VERSION
dotnet nuget push "./src/C4Sharp/bin/${BUILD_TYPE}/C4Sharp.${VERSION}.nupkg" --api-key $NUGET_KEY --source $NUGET_INDEX

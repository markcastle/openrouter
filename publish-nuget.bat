@echo off
rem ************************************************************************
rem * Utility for building and pushing OpenRouter nuget packages
rem * Author: Mark Castle / Captive Reality Ltd
rem * (c) 2025 Captive Reality Ltd.  All Rights Reserved.
rem *
rem * Usage: publish-nuget.bat [version] [source]
rem *   [version] = the version of the nuget packages
rem *   [source]  = the nuget source (default: nuget.org)
rem *
rem * Example: publish-nuget.bat 1.0.0
rem *          publish-nuget.bat 1.0.0 "C:\\local-nuget-server"
rem ************************************************************************

IF "%1"=="" GOTO Usage
SET version=%1
IF "%2"=="" (
    echo Pushing to Nuget.org
    SET source=nuget.org
    SET command=push
) ELSE (
    echo Pushing to Local Nuget Server: %2
    SET source=%2
    SET command=add
)

REM Build all projects in Release mode
call dotnet build OpenRouter.sln -c Release

REM Pack all main projects
call dotnet pack OpenRouter.Abstractions\OpenRouter.Abstractions.csproj -c Release -p:PackageVersion=%version% --no-build
call dotnet pack OpenRouter.Client.Core\OpenRouter.Client.Core.csproj -c Release -p:PackageVersion=%version% --no-build
call dotnet pack OpenRouter.Client.SystemTextJson\OpenRouter.Client.SystemTextJson.csproj -c Release -p:PackageVersion=%version% --no-build
call dotnet pack OpenRouter.Client.NewtonsoftJson\OpenRouter.Client.NewtonsoftJson.csproj -c Release -p:PackageVersion=%version% --no-build
call dotnet pack OpenRouter.Client.DependencyInjection\OpenRouter.Client.DependencyInjection.csproj -c Release -p:PackageVersion=%version% --no-build
call dotnet pack OpenRouter.Client.Resilience\OpenRouter.Client.Resilience.csproj -c Release -p:PackageVersion=%version% --no-build

REM Push or add each package
for %%P in (
    OpenRouter.Abstractions
    OpenRouter.Client.Core
    OpenRouter.Client.SystemTextJson
    OpenRouter.Client.NewtonsoftJson
    OpenRouter.Client.DependencyInjection
    OpenRouter.Client.Resilience
) do (
    if exist %%P\bin\Release\%%P.%version%.nupkg (
        nuget %command% "%%P\bin\Release\%%P.%version%.nupkg" -src %source%
    ) else (
        echo Package %%P\bin\Release\%%P.%version%.nupkg not found!
    )
)

echo Done.
goto End

:Usage

echo Usage: %~n0 [version] [source]
echo.
echo Publish nuget packages to nuget.org or a local server.
echo.
echo Where
 echo   [version] = the version of the nuget packages
 echo   [source]  = the path to nuget local server. If omitted, pushes to nuget.org
 echo.
echo Example:
 echo   %~n0 1.0.0
 echo   %~n0 1.0.0 "C:\\local-nuget-server"
echo.
echo Note: All packages must have the same version. Set your NuGet API key with 'nuget setapikey' before pushing to nuget.org.
echo.

:End

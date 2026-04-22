@echo off
setlocal

set "ROOT=%~dp0"
dotnet run --project "%ROOT%ObscuraProfileManager\ObscuraProfileManager.csproj"
exit /b %ERRORLEVEL%

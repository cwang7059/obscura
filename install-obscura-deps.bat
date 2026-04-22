@echo off
setlocal EnableExtensions

REM Double-click to install Obscura build dependencies on Windows.
REM This installs:
REM   1. Rust toolchain via rustup
REM   2. Visual Studio C++ Build Tools
REM   3. NASM assembler for BoringSSL builds

set "ROOT=%~dp0"
cd /d "%ROOT%"

if not defined OBSCURA_NO_PAUSE set "OBSCURA_NO_PAUSE=0"

set "CARGO="
where cargo >nul 2>&1 && set "CARGO=cargo"
if not defined CARGO if exist "%USERPROFILE%\.cargo\bin\cargo.exe" set "CARGO=%USERPROFILE%\.cargo\bin\cargo.exe"
set "VSWHERE=C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe"
set "VC_TOOLS_PATH="
set "NASM_EXE="
set "NASM_VERSION=3.01"
set "TOOLS_DIR=%ROOT%.tools"
set "NASM_DIR=%TOOLS_DIR%\nasm-%NASM_VERSION%"
set "NASM_ZIP=%TEMP%\nasm-%NASM_VERSION%-win64.zip"
set "NASM_URL=https://www.nasm.us/pub/nasm/releasebuilds/%NASM_VERSION%/win64/nasm-%NASM_VERSION%-win64.zip"

if defined CARGO (
    echo [1/3] Rust toolchain already found.
) else (
    echo [1/3] Installing Rust toolchain...
    powershell -NoProfile -ExecutionPolicy Bypass -Command "Invoke-WebRequest https://win.rustup.rs/x86_64 -OutFile $env:TEMP\rustup-init.exe"
    if errorlevel 1 (
        echo.
        echo [ERROR] Failed to download rustup-init.exe
        goto fail
    )

    "%TEMP%\rustup-init.exe" -y --default-toolchain stable
    if errorlevel 1 (
        echo.
        echo [ERROR] Rust installation failed.
        goto fail
    )

    set "PATH=%PATH%;%USERPROFILE%\.cargo\bin"
    echo Rust installed.
)

echo.
if exist "%VSWHERE%" (
    for /f "usebackq delims=" %%I in (`"%VSWHERE%" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
        set "VC_TOOLS_PATH=%%I"
    )
)

if defined VC_TOOLS_PATH (
    echo [2/3] Visual Studio C++ Build Tools already found:
    echo   %VC_TOOLS_PATH%
) else (
    echo [2/3] Installing Visual Studio C++ Build Tools...
    echo This step can take a while.

    powershell -NoProfile -ExecutionPolicy Bypass -Command "Invoke-WebRequest https://aka.ms/vs/17/release/vs_BuildTools.exe -OutFile $env:TEMP\vs_BuildTools.exe"
    if errorlevel 1 (
        echo.
        echo [ERROR] Failed to download vs_BuildTools.exe
        goto fail
    )

    start /wait "" "%TEMP%\vs_BuildTools.exe" --quiet --wait --norestart --nocache --installPath C:\BuildTools --add Microsoft.VisualStudio.Workload.VCTools --includeRecommended
    set "VS_EXIT=%ERRORLEVEL%"
    if not "%VS_EXIT%"=="0" if not "%VS_EXIT%"=="3010" (
        echo.
        echo [ERROR] Visual Studio Build Tools installation failed with exit code %VS_EXIT%.
        goto fail
    )
)

echo.
where nasm >nul 2>&1 && for /f "usebackq delims=" %%I in (`where nasm`) do (
    if not defined NASM_EXE set "NASM_EXE=%%I"
)
if not defined NASM_EXE if exist "%NASM_DIR%\nasm.exe" set "NASM_EXE=%NASM_DIR%\nasm.exe"

if defined NASM_EXE (
    echo [3/3] NASM already found:
    echo   %NASM_EXE%
) else (
    echo [3/3] Installing NASM %NASM_VERSION% into:
    echo   %NASM_DIR%

    if not exist "%TOOLS_DIR%" mkdir "%TOOLS_DIR%"
    if exist "%NASM_DIR%" rmdir /s /q "%NASM_DIR%"

    powershell -NoProfile -ExecutionPolicy Bypass -Command "Invoke-WebRequest '%NASM_URL%' -OutFile '%NASM_ZIP%'; if (Test-Path '%NASM_DIR%') { Remove-Item '%NASM_DIR%' -Recurse -Force }; Expand-Archive -LiteralPath '%NASM_ZIP%' -DestinationPath '%TOOLS_DIR%' -Force"
    if errorlevel 1 (
        echo.
        echo [ERROR] Failed to download or extract NASM.
        goto fail
    )

    if not exist "%NASM_DIR%\nasm.exe" (
        echo.
        echo [ERROR] NASM extraction finished, but nasm.exe was not found.
        goto fail
    )

    set "NASM_EXE=%NASM_DIR%\nasm.exe"
    echo NASM installed.
)

echo.
echo Dependency install finished.
echo Now run start-obscura.bat
goto success

:success
if "%OBSCURA_NO_PAUSE%"=="1" exit /b 0
echo.
pause
exit /b 0

:fail
if "%OBSCURA_NO_PAUSE%"=="1" exit /b 1
echo.
pause
exit /b 1

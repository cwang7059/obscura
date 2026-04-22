@echo off
setlocal EnableExtensions

REM Double-click to build and start Obscura on Windows.
REM Optional environment overrides:
REM   OBSCURA_PORT=9222
REM   OBSCURA_ENABLE_STEALTH=1
REM   OBSCURA_FORCE_BUILD=0
REM   OBSCURA_BUILD_ONLY=0
REM   OBSCURA_NO_PAUSE=0
REM   OBSCURA_NASM_EXE=D:\path\to\nasm.exe

set "ROOT=%~dp0"
cd /d "%ROOT%"

if not defined OBSCURA_PORT set "OBSCURA_PORT=9222"
if not defined OBSCURA_ENABLE_STEALTH set "OBSCURA_ENABLE_STEALTH=1"
if not defined OBSCURA_FORCE_BUILD set "OBSCURA_FORCE_BUILD=0"
if not defined OBSCURA_BUILD_ONLY set "OBSCURA_BUILD_ONLY=0"
if not defined OBSCURA_NO_PAUSE set "OBSCURA_NO_PAUSE=0"

if not defined CARGO_HOME set "CARGO_HOME=%ROOT%.cargo-home"
if not exist "%CARGO_HOME%" mkdir "%CARGO_HOME%" >nul 2>&1
set "V8_FROM_SOURCE=0"
set "__VSCMD_ARG_NO_LOGO=1"

set "OBSCURA_EXE=%ROOT%target\release\obscura.exe"
set "WORKER_EXE=%ROOT%target\release\obscura-worker.exe"
set "NASM_EXE="
set "VSWHERE=C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe"
set "VSROOT="
set "VSDEVCMD="

set "CARGO="
where cargo >nul 2>&1 && set "CARGO=cargo"
if not defined CARGO if exist "%USERPROFILE%\.cargo\bin\cargo.exe" set "CARGO=%USERPROFILE%\.cargo\bin\cargo.exe"

if exist "%VSWHERE%" (
    for /f "usebackq delims=" %%I in (`"%VSWHERE%" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
        set "VSROOT=%%I"
    )
)
if defined VSROOT if exist "%VSROOT%\Common7\Tools\VsDevCmd.bat" set "VSDEVCMD=%VSROOT%\Common7\Tools\VsDevCmd.bat"

if defined OBSCURA_NASM_EXE if exist "%OBSCURA_NASM_EXE%" set "NASM_EXE=%OBSCURA_NASM_EXE%"
if not defined NASM_EXE (
    where nasm >nul 2>&1 && for /f "usebackq delims=" %%I in (`where nasm`) do (
        if not defined NASM_EXE set "NASM_EXE=%%I"
    )
)
if not defined NASM_EXE (
    for /f "usebackq delims=" %%I in (`dir /b /s "%ROOT%.tools\nasm.exe" 2^>nul`) do (
        if not defined NASM_EXE set "NASM_EXE=%%I"
    )
)
if defined NASM_EXE (
    for %%I in ("%NASM_EXE%") do set "PATH=%%~dpI;%PATH%"
    set "CMAKE_ASM_NASM_COMPILER=%NASM_EXE%"
)

if not defined CARGO (
    echo [ERROR] Rust toolchain not found.
    echo Run install-obscura-deps.bat first.
    echo After that, run start-obscura.bat again.
    goto fail
)

if "%OBSCURA_FORCE_BUILD%"=="1" goto build
if not exist "%OBSCURA_EXE%" goto build
if not exist "%WORKER_EXE%" goto build
goto run

:build
echo [1/2] Building Obscura...
echo.
if not defined NASM_EXE (
    echo [ERROR] NASM not found.
    echo Run install-obscura-deps.bat first.
    echo Or set OBSCURA_NASM_EXE to your nasm.exe path.
    goto fail
)

if defined VSDEVCMD (
    call "%VSDEVCMD%" -arch=x64 -host_arch=x64 >nul
    if errorlevel 1 (
        echo [ERROR] Failed to initialize Visual Studio build environment.
        goto fail
    )
)

"%CARGO%" build --release -p obscura-cli --features stealth
if errorlevel 1 (
    echo.
    echo [ERROR] Build failed.
    echo Make sure install-obscura-deps.bat has completed successfully.
    goto fail
)

if "%OBSCURA_BUILD_ONLY%"=="1" (
    echo.
    echo Build completed:
    echo   %OBSCURA_EXE%
    goto success
)

:run
if not exist "%OBSCURA_EXE%" (
    echo [ERROR] Executable not found:
    echo   %OBSCURA_EXE%
    goto fail
)

set "SERVE_ARGS=serve --port %OBSCURA_PORT%"
if "%OBSCURA_ENABLE_STEALTH%"=="1" set "SERVE_ARGS=%SERVE_ARGS% --stealth"

echo [2/2] Starting Obscura...
echo.
echo CDP WebSocket:
echo   ws://127.0.0.1:%OBSCURA_PORT%/devtools/browser
echo JSON API:
echo   http://127.0.0.1:%OBSCURA_PORT%/json/version
echo.
echo Press Ctrl+C to stop.
echo.

"%OBSCURA_EXE%" %SERVE_ARGS%
set "EXITCODE=%ERRORLEVEL%"

if not "%EXITCODE%"=="0" (
    echo.
    echo [ERROR] Obscura exited with code %EXITCODE%.
    goto fail
)

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

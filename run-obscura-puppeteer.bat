@echo off
setlocal

set "ROOT=%~dp0"
set "EXE=%ROOT%obscura.exe"
set "SCRIPT=%ROOT%scripts\puppeteer-obscura-demo.js"
set "URL=%~1"

if "%URL%"=="" set "URL=https://example.com"

if not exist "%EXE%" (
  echo [ERROR] obscura.exe not found: %EXE%
  exit /b 1
)

if not exist "%SCRIPT%" (
  echo [ERROR] demo script not found: %SCRIPT%
  exit /b 1
)

powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "Get-Process obscura -ErrorAction SilentlyContinue | Stop-Process -Force; Start-Sleep -Seconds 1; Invoke-CimMethod -ClassName Win32_Process -MethodName Create -Arguments @{ CommandLine='\"%EXE%\" serve --port 9222' } | Out-Null; Start-Sleep -Seconds 3"

node "%SCRIPT%" "%URL%"
set "RC=%ERRORLEVEL%"

exit /b %RC%

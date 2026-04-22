@echo off
setlocal

set "ROOT=%~dp0"
set "CONFIG=%~1"

if "%CONFIG%"=="" set "CONFIG=configs\acc_001.json"

if not exist "%ROOT%package.json" (
  echo [ERROR] package.json not found in %ROOT%
  exit /b 1
)

if not exist "%ROOT%node_modules\playwright-core" (
  echo [INFO] Installing Node dependencies...
  call npm install
  if errorlevel 1 exit /b %errorlevel%
)

node "%ROOT%scripts\open-profile.js" "%ROOT%%CONFIG%"
exit /b %ERRORLEVEL%

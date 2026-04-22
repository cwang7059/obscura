@echo off
setlocal
call "%~dp0open-profile.bat" "configs\acc_001.json"
exit /b %ERRORLEVEL%

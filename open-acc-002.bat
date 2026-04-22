@echo off
setlocal
call "%~dp0open-profile.bat" "configs\acc_002.json"
exit /b %ERRORLEVEL%

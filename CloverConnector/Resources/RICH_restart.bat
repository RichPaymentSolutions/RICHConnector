@echo off

set exe_name=Rich Connector.exe
set batch_file=%~dp0RICH_restart.bat

@echo off

set vbs_file=%~dp0RICH_restart.vbs
set shortcut_file=%userprofile%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\RICH Restart.lnk

echo Creating shortcut for %vbs_file%...
(
echo Set WshShell = WScript.CreateObject("WScript.Shell"^)
echo Set oShellLink = WshShell.CreateShortcut("%shortcut_file%"^)
echo oShellLink.TargetPath = "wscript.exe"
echo oShellLink.Arguments = """%vbs_file%"""
echo oShellLink.WindowStyle = 7
echo oShellLink.WorkingDirectory = "%~dp0"
echo oShellLink.Save
) > "%temp%\shortcut.vbs"
cscript //nologo "%temp%\shortcut.vbs" >nul
del "%temp%\shortcut.vbs" >nul

echo Shortcut created successfully at %shortcut_file%.

echo Checking if %exe_name% is running...
:loop
tasklist | find /i "%exe_name%" >nul
if errorlevel 1 (
    echo %exe_name% is not running, restarting...
    start "" /b /min "%exe_name%"
)
ping -n 10 127.0.0.1 >nul
goto loop

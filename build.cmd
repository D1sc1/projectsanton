@echo off
rem Сборка всех проектов: готовые программы появятся в папке dist
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*
if errorlevel 1 (
    echo.
    echo Build FAILED - see messages above.
    pause
    exit /b 1
)
echo.
echo Build OK. Programs: "%~dp0dist"
explorer "%~dp0dist"
pause

@echo off
chcp 65001 >nul
rem Запуск веб-версии игры "Реверси" в IIS Express на http://localhost:8380/
set PORT=8380
set "IISEXPRESS=%ProgramFiles%\IIS Express\iisexpress.exe"
if not exist "%IISEXPRESS%" set "IISEXPRESS=%ProgramFiles(x86)%\IIS Express\iisexpress.exe"
if not exist "%IISEXPRESS%" goto noiis

echo Запускается сайт игры: http://localhost:%PORT%/
echo Чтобы остановить сайт, закройте окно IIS Express.
start "IIS Express - Reversi Web" "%IISEXPRESS%" /path:"%~dp0site" /port:%PORT%
timeout /t 3 /nobreak >nul
start "" "http://localhost:%PORT%/Game"
exit /b 0

:noiis
echo Не найден IIS Express. Он устанавливается вместе с Visual Studio
echo или отдельно: https://www.microsoft.com/download/details.aspx?id=48264
pause
exit /b 1

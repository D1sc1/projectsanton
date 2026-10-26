@echo off
chcp 65001 >nul
rem Запуск сервера игры "Реверси" (ASP.NET Web API) в IIS Express на http://localhost:8387/
rem Серверу нужна база PostgreSQL: строка подключения EFMain в файле Server\Web.config
set PORT=8387
set "IISEXPRESS=%ProgramFiles%\IIS Express\iisexpress.exe"
if not exist "%IISEXPRESS%" set "IISEXPRESS=%ProgramFiles(x86)%\IIS Express\iisexpress.exe"
if not exist "%IISEXPRESS%" goto noiis

echo Запускается сервер: http://localhost:%PORT%/
echo Чтобы остановить сервер, закройте окно IIS Express.
start "IIS Express - Reversi Server" "%IISEXPRESS%" /path:"%~dp0Server" /port:%PORT%
timeout /t 3 /nobreak >nul
start "" "http://localhost:%PORT%/"
exit /b 0

:noiis
echo Не найден IIS Express. Он устанавливается вместе с Visual Studio
echo или отдельно: https://www.microsoft.com/download/details.aspx?id=48264
pause
exit /b 1

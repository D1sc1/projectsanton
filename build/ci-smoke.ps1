<#
.SYNOPSIS
    Проверка собранных программ на сервере сборки (GitHub Actions), запускается после build.ps1.

.DESCRIPTION
    1. Запускает PostgreSQL из образа сервера сборки и задает пароль, как в строках подключения проектов.
    2. Запускает сайт игры (8) и сервер (10) из папки dist в IIS Express - так же, как это делают .cmd-файлы,
       и открывает страницы (так проверяются представления Razor и публикация сайтов).
    3. Прогоняет все тесты проектов 9 и 10, в том числе работающие с БД и с сервером через HTTP.
#>
$ErrorActionPreference = 'Stop'

$root = Split-Path $PSScriptRoot
$projects = Join-Path $root 'Projects.24.3.24.4'
$dist = Join-Path $root 'dist'
$processes = @()

function Write-Step([string]$text) {
    Write-Host ''
    Write-Host "==> $text" -ForegroundColor Cyan
}

function Invoke-Native([string]$exe, [string[]]$arguments) {
    & $exe @arguments
    if ($LASTEXITCODE -ne 0) {
        throw "Команда завершилась с ошибкой ($LASTEXITCODE): $exe $($arguments -join ' ')"
    }
}

function Start-Site([string]$path, [int]$port) {
    $iisExpress = Join-Path $env:ProgramFiles 'IIS Express\iisexpress.exe'
    if (-not (Test-Path $iisExpress)) {
        throw "Не найден IIS Express: $iisExpress"
    }

    $process = Start-Process $iisExpress -ArgumentList "/path:`"$path`"", "/port:$port" -PassThru -WindowStyle Hidden
    $script:processes += $process

    for ($i = 0; $i -lt 60; $i++) {
        try {
            Invoke-WebRequest "http://localhost:$port/" -UseBasicParsing -TimeoutSec 60 | Out-Null
            return
        }
        catch {
            Start-Sleep -Seconds 2
        }
    }

    throw "Сайт на порту $port не отвечает"
}

function Assert-Page([string]$url, [string]$expectedText, $session) {
    $response = Invoke-WebRequest $url -UseBasicParsing -WebSession $session -TimeoutSec 120
    if ($response.StatusCode -ne 200 -or -not $response.Content.Contains($expectedText)) {
        throw "Страница $url не содержит '$expectedText'"
    }

    Write-Host "  OK  $url"
    return $response
}

function Set-ServerAddress([string]$configPath) {
    $text = [IO.File]::ReadAllText($configPath).Replace('https://localhost:44387/', 'http://localhost:8387/')
    [IO.File]::WriteAllText($configPath, $text, (New-Object Text.UTF8Encoding $true))
}

try {
    Write-Step 'PostgreSQL'
    $service = Get-Service -Name 'postgresql*' | Select-Object -First 1
    Set-Service -Name $service.Name -StartupType Manual
    Start-Service -Name $service.Name

    # В образе сервера сборки у пользователя postgres пароль root, в проектах - Qwerty123
    $psql = if ($env:PGBIN) { Join-Path $env:PGBIN 'psql.exe' } else {
        Get-ChildItem 'C:\Program Files\PostgreSQL\*\bin\psql.exe' | Select-Object -Last 1 -ExpandProperty FullName
    }
    $env:PGPASSWORD = 'root'
    Invoke-Native $psql @('-U', 'postgres', '-h', 'localhost', '-c', "ALTER USER postgres PASSWORD 'Qwerty123';")
    $env:PGPASSWORD = 'Qwerty123'
    Invoke-Native $psql @('-U', 'postgres', '-h', 'localhost', '-c', 'CREATE DATABASE "ReversiGame.1.Dev.24.3.24.4";')
    Invoke-Native $psql @('-U', 'postgres', '-h', 'localhost', '-d', 'ReversiGame.1.Dev.24.3.24.4', '-v', 'ON_ERROR_STOP=1',
        '-f', (Join-Path $dist '9. Game DB\CreateTables.sql'))

    Write-Step '8. Game Web: сайт в IIS Express'
    Start-Site (Join-Path $dist '8. Game Web\site') 8380
    $session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
    Assert-Page 'http://localhost:8380/' 'Реверси' $session | Out-Null
    Assert-Page 'http://localhost:8380/Home/About' 'Правила' $session | Out-Null
    Assert-Page 'http://localhost:8380/Game' 'reversi-board' $session | Out-Null
    Assert-Page 'http://localhost:8380/Game/MakeMove?row=2&column=3' 'Ходов сделано: 2' $session | Out-Null
    Assert-Page 'http://localhost:8380/Game/NewGame?vsComputer=false' 'Игра вдвоем' $session | Out-Null

    Write-Step '10. Game Client-Server: сервер в IIS Express'
    Start-Site (Join-Path $dist '10. Game Client-Server\Server') 8387
    Assert-Page 'http://localhost:8387/' 'Сервер игры' $session | Out-Null
    Assert-Page 'http://localhost:8387/api/users/get-all' 'Users' $session | Out-Null

    Write-Step 'Тесты проектов 9 и 10 (с базой данных и сервером)'
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    $vstest = & $vswhere -latest -products * -find '**\TestPlatform\vstest.console.exe' | Select-Object -First 1

    $clientTests = Join-Path $projects '10. Game Client-Server\ReversiGame\ReversiGame.ClientUnitTests\bin\Release\ReversiGame.ClientUnitTests.dll'
    Set-ServerAddress "$clientTests.config"

    Invoke-Native $vstest @(
        (Join-Path $projects '9. Game DB\ReversiGame\ReversiGame.UnitTests\bin\Release\ReversiGame.UnitTests.dll'),
        (Join-Path $projects '10. Game Client-Server\ReversiGame\ReversiGame.UnitTests\bin\Release\ReversiGame.UnitTests.dll'),
        $clientTests)

    Write-Step 'Проверка завершена успешно'
}
finally {
    foreach ($process in $processes) {
        if (-not $process.HasExited) {
            Stop-Process -Id $process.Id -Force
        }
    }
}

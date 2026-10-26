<#
.SYNOPSIS
    Собирает все 10 проектов и складывает готовые программы (exe) в папку dist.

.DESCRIPTION
    1. Находит MSBuild (Visual Studio 2019/2022 или Build Tools) и nuget.exe (при необходимости скачивает).
    2. Для каждого решения восстанавливает NuGet-пакеты и собирает конфигурацию Release.
       Решения собираются по порядку 1..10: проект 10 использует библиотеки из проекта 9.
    3. Копирует результат в dist\<номер. название проекта>:
       - WinForms/WPF-программы - exe со всеми dll и config;
       - веб-проекты (8 - сайт игры, 10 - сервер) публикуются в папку и запускаются .cmd-файлом через IIS Express.

.PARAMETER Test
    После сборки запустить модульные тесты, которым не нужна база данных (проект 9: правила и сервисы).

.PARAMETER MSBuildPath
    Полный путь к MSBuild.exe, если его не удается найти автоматически.

.EXAMPLE
    .\build.ps1
    .\build.ps1 -Test
#>
param(
    [switch]$Test,
    [string]$MSBuildPath
)

$ErrorActionPreference = 'Stop'
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

$root = $PSScriptRoot
$projects = Join-Path $root 'Projects.24.3.24.4'
$dist = Join-Path $root 'dist'
$templates = Join-Path $root 'build\templates'
$toolsDir = Join-Path $root 'build\.tools'

function Write-Step([string]$text) {
    Write-Host ''
    Write-Host "==> $text" -ForegroundColor Cyan
}

function Find-VsWhere {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (Test-Path $vswhere) { return $vswhere }
    return $null
}

function Find-MSBuild {
    if ($MSBuildPath) { return $MSBuildPath }

    $vswhere = Find-VsWhere
    if ($vswhere) {
        $found = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
        if ($found) { return $found }
    }

    $cmd = Get-Command msbuild.exe -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }

    throw 'MSBuild не найден. Установите Visual Studio 2022 (рабочие нагрузки ".NET desktop development" и "ASP.NET and web development") или укажите путь: .\build.ps1 -MSBuildPath <путь к MSBuild.exe>'
}

function Find-NuGet {
    $cmd = Get-Command nuget.exe -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }

    $nuget = Join-Path $toolsDir 'nuget.exe'
    if (-not (Test-Path $nuget)) {
        New-Item -ItemType Directory -Force -Path $toolsDir | Out-Null
        Write-Host 'Скачивание nuget.exe...'
        Invoke-WebRequest 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile $nuget -UseBasicParsing
    }

    return $nuget
}

function Invoke-Native([string]$exe, [string[]]$arguments) {
    & $exe @arguments
    if ($LASTEXITCODE -ne 0) {
        throw "Команда завершилась с ошибкой ($LASTEXITCODE): $exe $($arguments -join ' ')"
    }
}

function Build-Solution([string]$relativePath, [string]$publishDir) {
    $solution = Join-Path $projects $relativePath
    Write-Step "Сборка $relativePath"

    Invoke-Native $script:nuget @('restore', $solution, '-NonInteractive', '-Verbosity', 'quiet')

    $msbuildArgs = @($solution, '/m', '/nologo', '/verbosity:minimal', '/p:Configuration=Release')
    if ($publishDir) {
        # Веб-проект решения публикуется в папку (как "Publish -> Folder" в Visual Studio)
        $msbuildArgs += @('/p:DeployOnBuild=true', '/p:DeployDefaultTarget=WebPublish', '/p:WebPublishMethod=FileSystem',
            "/p:PublishUrl=$publishDir", '/p:DeleteExistingFiles=true')
    }

    Invoke-Native $script:msbuild $msbuildArgs
}

function Copy-App([string]$binRelativePath, [string]$target) {
    $bin = Join-Path $projects $binRelativePath
    $destination = Join-Path $dist $target
    New-Item -ItemType Directory -Force -Path $destination | Out-Null

    # Файлы отладки и XML-документация пакетов для запуска не нужны
    Get-ChildItem -LiteralPath $bin | Where-Object { $_.Extension -notin '.pdb', '.xml' } |
        Copy-Item -Destination $destination -Recurse -Force
}

function Copy-Template([string]$relativePath) {
    $source = Join-Path $templates $relativePath
    $destination = Join-Path $dist $relativePath
    New-Item -ItemType Directory -Force -Path (Split-Path $destination) | Out-Null
    Copy-Item -LiteralPath $source -Destination $destination -Force
}

$msbuild = Find-MSBuild
$nuget = Find-NuGet
Write-Host "MSBuild: $msbuild"
Write-Host "NuGet:   $nuget"

if (Test-Path $dist) {
    Remove-Item $dist -Recurse -Force
}
New-Item -ItemType Directory -Path $dist | Out-Null

# 1-7: настольные программы
Build-Solution '1. AlarmClock\AlarmClock\AlarmClock.sln'
Copy-App '1. AlarmClock\AlarmClock\AlarmClock\bin\Release' '1. AlarmClock'

Build-Solution '2. Drawing\Drawing\Drawing.sln'
Copy-App '2. Drawing\Drawing\Drawing\bin\Release' '2. Drawing'

Build-Solution '3. Calculator\Calculator\Calculator.sln'
Copy-App '3. Calculator\Calculator\Calculator\bin\Release' '3. Calculator'

Build-Solution '4. Calculator WPF\Calculator\Calculator.sln'
Copy-App '4. Calculator WPF\Calculator\Calculator.Wpf\bin\Release' '4. Calculator WPF'

Build-Solution '5. CardFile\CardFile\CardFile.sln'
Copy-App '5. CardFile\CardFile\CardFile.Wpf\bin\Release' '5. CardFile'

Build-Solution '6. GraphEditor\GraphEditor\GraphEditor.sln'
Copy-App '6. GraphEditor\GraphEditor\GraphEditor\bin\Release' '6. GraphEditor'

Build-Solution '7. Game WPF\ReversiGame\ReversiGame.sln'
Copy-App '7. Game WPF\ReversiGame\ReversiGame.Wpf\bin\Release' '7. Game WPF'

# 8: сайт игры
Build-Solution '8. Game Web\ReversiGame\ReversiGame.sln' (Join-Path $dist '8. Game Web\site')
Copy-Template '8. Game Web\Запустить игру.cmd'

# 9: игра с базой данных
Build-Solution '9. Game DB\ReversiGame\ReversiGame.sln'
Copy-App '9. Game DB\ReversiGame\ReversiGame.Wpf\bin\Release' '9. Game DB'
Copy-Item (Join-Path $projects '9. Game DB\ReversiGame\ReversiGame.DataAccess\Database\CreateTables.sql') (Join-Path $dist '9. Game DB')

# 10: сервер и клиент
Build-Solution '10. Game Client-Server\ReversiGame\ReversiGame.Server.sln' (Join-Path $dist '10. Game Client-Server\Server')
Build-Solution '10. Game Client-Server\ReversiGame\ReversiGame.Client.sln'
Copy-App '10. Game Client-Server\ReversiGame\ReversiGame.Wpf\bin\Release' '10. Game Client-Server\Client'
Copy-Template '10. Game Client-Server\1. Запустить сервер.cmd'
Copy-Template '10. Game Client-Server\2. Запустить клиент.cmd'

# Готовый клиент обращается к серверу, запущенному "1. Запустить сервер.cmd" (http, порт 8387).
# В исходном App.config остается адрес IIS Express из Visual Studio.
$clientConfig = Join-Path $dist '10. Game Client-Server\Client\ReversiGame.Wpf.exe.config'
$configText = [IO.File]::ReadAllText($clientConfig)
$configText = $configText.Replace('https://localhost:44387/', 'http://localhost:8387/')
[IO.File]::WriteAllText($clientConfig, $configText, (New-Object Text.UTF8Encoding $true))

Copy-Template 'КАК ЗАПУСТИТЬ.txt'

# Проверка, что в dist есть все нужное для запуска
$expected = @(
    '1. AlarmClock\AlarmClock.exe', '1. AlarmClock\AlarmClock.exe.config', '1. AlarmClock\Images',
    '2. Drawing\Drawing.exe',
    '3. Calculator\Calculator.exe', '3. Calculator\Calculator.Business.dll',
    '4. Calculator WPF\Calculator.Wpf.exe', '4. Calculator WPF\Calculator.Business.dll',
    '5. CardFile\CardFile.Wpf.exe', '5. CardFile\CardFile.DataStore.dll', '5. CardFile\AutoMapper.dll',
    '6. GraphEditor\GraphEditor.exe', '6. GraphEditor\GraphEditor.Business.dll',
    '7. Game WPF\ReversiGame.Wpf.exe', '7. Game WPF\ReversiGame.Business.dll',
    '8. Game Web\site\Web.config', '8. Game Web\site\Global.asax', '8. Game Web\site\bin\ReversiGame.Web.dll',
    '8. Game Web\site\Views\Game\Index.cshtml', '8. Game Web\site\Content\Site.css', '8. Game Web\Запустить игру.cmd',
    '9. Game DB\ReversiGame.Wpf.exe', '9. Game DB\ReversiGame.Wpf.exe.config', '9. Game DB\ReversiGame.DataAccess.EF.dll',
    '9. Game DB\EntityFramework6.Npgsql.dll', '9. Game DB\CreateTables.sql',
    '10. Game Client-Server\Server\Web.config', '10. Game Client-Server\Server\bin\ReversiGame.WebApi.dll',
    '10. Game Client-Server\Server\bin\ReversiGame.DataAccess.EF.dll',
    '10. Game Client-Server\Client\ReversiGame.Wpf.exe', '10. Game Client-Server\Client\ReversiGame.BusinessProxy.dll',
    '10. Game Client-Server\1. Запустить сервер.cmd', '10. Game Client-Server\2. Запустить клиент.cmd',
    'КАК ЗАПУСТИТЬ.txt'
)
$missing = $expected | Where-Object { -not (Test-Path -LiteralPath (Join-Path $dist $_)) }
if ($missing) {
    throw "В папке dist не хватает файлов: $($missing -join ', ')"
}

if (-not ([IO.File]::ReadAllText($clientConfig)).Contains('http://localhost:8387/')) {
    throw 'В конфигурации клиента не удалось заменить адрес сервера'
}

if ($Test) {
    Write-Step 'Модульные тесты (без базы данных)'

    $vstest = $null
    $vswhere = Find-VsWhere
    if ($vswhere) {
        $vstest = & $vswhere -latest -products * -find '**\TestPlatform\vstest.console.exe' | Select-Object -First 1
    }

    if (-not $vstest) {
        Write-Warning 'vstest.console.exe не найден, тесты пропущены'
    }
    else {
        $testDll = Join-Path $projects '9. Game DB\ReversiGame\ReversiGame.UnitTests\bin\Release\ReversiGame.UnitTests.dll'
        Invoke-Native $vstest @($testDll, '/TestCaseFilter:FullyQualifiedName~RulesTests|FullyQualifiedName~ServiceTests')
    }
}

Write-Step "Готово. Программы лежат в папке $dist"
Get-ChildItem $dist -Recurse -Filter *.exe | Where-Object { $_.FullName -notlike '*\roslyn\*' } | ForEach-Object { Write-Host ('  ' + $_.FullName.Substring($dist.Length + 1)) }
Get-ChildItem $dist -Recurse -Filter *.cmd | ForEach-Object { Write-Host ('  ' + $_.FullName.Substring($dist.Length + 1)) }

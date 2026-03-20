# ASCII Art
Write-Host "  _____ _    ____    ____      _                     " -ForegroundColor Cyan
Write-Host " |  ___/ \  / ___|  |  _ \ ___| | ___  __ _ ___  ___ " -ForegroundColor Cyan
Write-Host " | |_ / _ \ \___ \  | |_) / _ \ |/ _ \/ _`` / __|/ _ \" -ForegroundColor Cyan
Write-Host " |  _/ ___ \ ___) | |  _ <  __/ |  __/ (_| \__ \  __/" -ForegroundColor Cyan
Write-Host " |_|/_/   \_\____/  |_| \_\___|_|\___|\__,_|___/\___|" -ForegroundColor Cyan
Write-Host
# 定义
$scriptDir = $PSScriptRoot # 脚本路径
$projectRoot = Join-Path $scriptDir ".." -Resolve # 项目根目录

$assemblyInfoPath = Join-Path $projectRoot "My Project\AssemblyInfo.vb"
$whatsNewPath = Join-Path $projectRoot "src\Docs\WHATSNEW.txt"
$changelogPath = Join-Path $projectRoot "docs\CHANGELOG.txt"

$assemblyInfo = Get-Content $assemblyInfoPath

# 读取版本号
$versionLine = $assemblyInfo | Where-Object { $_ -match "AssemblyVersion" }

if ($versionLine -match '"(\d+\.\d+\.\d+)\.\d+"') {
    $version = $matches[1]
}

# 当版本号为空时直接退出
if (-not $version) {
    Write-Error "Version not detected!" -ForegroundColor Red
    Start-Sleep -Seconds 5 # 自动退出
    exit 1
}

Write-Host "Detected version: $version"

$tagName = "v$version"

# 更新 CHANGELOG
$whatsNew = Get-Content $whatsNewPath -Raw 
$changelog = ""

if (Test-Path $changelogPath) {
    $changelog = Get-Content $changelogPath -Raw
}

$newContent = $whatsNew.TrimEnd() + "`r`n`r`n" + $changelog.TrimStart()
Set-Content $changelogPath $newContent -Encoding UTF8

Write-Host "------ WHATSNEW PREVIEW ------"
Write-Host $whatsNew
Write-Host "-------------------------------"

Write-Host "CHANGELOG updated."

# 提交
git add $changelogPath
git commit -m "Version $tagName"

# 创建 tag
git tag $tagName

# 推送
git push
git push origin $tagName

Write-Host "Release $tagName pushed successfully." -ForegroundColor Green
Start-Sleep -Seconds 5 # 自动退出
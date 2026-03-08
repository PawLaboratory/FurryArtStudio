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
    Write-Error "Version not detected!"
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

Write-Host "------ CHANGELOG PREVIEW ------"
Write-Host $newContent
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

Write-Host "Release $tagName pushed successfully."
Start-Sleep -Seconds 5 # 自动退出
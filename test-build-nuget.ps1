$env:BuildConfiguration = 'Release'
$env:GITHUB_OUTPUT = ''

.\build-nuget.ps1

Write-Host "GITHUB_OUTPUT: $env:GITHUB_OUTPUT"
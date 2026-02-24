$env:BuildConfiguration = 'Release'
$env:GITHUB_OUTPUT = ''

.\build-nuget.ps1

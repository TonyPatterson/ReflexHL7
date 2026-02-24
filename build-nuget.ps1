$nuspecPath = "ReflexHL7.nuspec"
$outputDirectory = "publish"
$publishVersion = 'publish-version.txt'

dotnet format --verify-no-changes

dotnet test --configuration $env:BuildConfiguration

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed in dotnet test"
    exit 1
}

if ($env:BuildConfiguration -ne 'Release') {
    Write-Host "ngfilename=" >> $env:GITHUB_OUTPUT

    Write-Host "Exiting without publishing, because not a Release build"

    exit 0
}

# Determine whether the 'publish-version' file has been updated - we publish a new nuget package whenever this changes
$publish = git show --name-only --pretty="" HEAD | Where-Object { $_ -like $publishVersion }

if (-not $publish) {
    Write-Host "ngfilename=" >> $env:GITHUB_OUTPUT

    Write-Host "Exiting without publishing, because publish-version file has not been changed"

    exit 0
}

# Get the current commit ID and write to the nuspec file
$repoUrl = git config --get remote.origin.url
$repoBranch = git rev-parse --abbrev-ref HEAD
$commitId = git rev-parse HEAD
$currentYear = git log -1 --format=%ad --date=format:'%Y'

# Get first non-comment line from 'publish-version' file
$versionNumber = Get-Content $publishVersion | Where-Object { $_ -and ($_ -notmatch '^\s*#') } | Select-Object -First 1
$versionNumberNoSuffix = $versionNumber.Split('-')[0]

[xml]$nuspec = Get-Content $nuspecPath

$copyright = "Copyright (C) " + $nuspec.package.metadata.owners + " " + $currentYear

$ngfilename = "publish\" + $nuspec.package.metadata.id + "." + $versionNumber + ".nupkg"

# Run the nuget pack command
dotnet clean
dotnet build --configuration $env:BuildConfiguration /p:PackageVersionNoSuffix=$versionNumberNoSuffix /p:PackageCopyright=$copyright
dotnet pack $nuspecPath --configuration $env:BuildConfiguration --output $outputDirectory /p:PackageVersion=$versionNumber /p:RepositoryUrl=$repoUrl /p:RepositoryBranch=$repoBranch /p:RepositoryCommitId=$commitId /p:PackageCopyright=$copyright

if ($LASTEXITCODE -eq 0) {
    Write-Host "ngfilename=$ngfilename" >> $env:GITHUB_OUTPUT

    Write-Host "Publishing output to $ngfilename"
} else {
    Write-Host "Failed in dotnet pack"
    exit 1
}

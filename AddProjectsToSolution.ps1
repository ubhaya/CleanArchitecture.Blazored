param (
    [string]$solutionPath,
    [string]$projectsPath,
    [string]$projectsToExclude
)

# Check if solutionPath and projectsPath are provided
if (-not $solutionPath -or -not $projectsPath) {
    Write-Host "Usage: AddProjectsToSolution.ps1 -solutionPath <Path\To\YourSolution.sln> -projectsPath <Path\To\Your\Projects>"
    exit 1
}

$isSolutionExists = Test-Path $solutionPath

if (-not $isSolutionExists){
    $solutionName = [System.IO.Path]::GetFileNameWithoutExtension($solutionPath)
    $newSolutionCommand = "dotnet new sln -n $solutionName"
    Invoke-Expression $newSolutionCommand
    Write-Host "Solution $solutionName is created"
}

# Get a list of all .csproj files in the projects directory
$projectFiles = Get-ChildItem -Path $projectsPath -Filter *.csproj -Recurse

# Iterate through each project file and add it to the solution
foreach ($projectFile in $projectFiles) {
    # Get the project file name without extension
     $projectName = [System.IO.Path]::GetFileNameWithoutExtension($projectFile.Name)

    # Add the project to the solution
    $addProjectCommand = "dotnet sln $solutionPath add $($projectFile.FullName)"
    Invoke-Expression $addProjectCommand

    Write-Host "Added project '$projectName' to solution."
}

if (-not $null -eq $projectsToExclude) {
    $projectToExclude = Join-Path -Path $projectsPath -ChildPath $projectsToExclude
    $isProjectToExcludeExists = Test-Path $projectToExclude

    if ($isProjectToExcludeExists)
    {
        dotnet sln $solutionPath remove $projectToExclude
    }
}
Write-Host "Build process completed."
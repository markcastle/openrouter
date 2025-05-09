# This script parses the Cobertura coverage report and updates the coverage badge in README.md

$ErrorActionPreference = 'Stop'

# Find the coverage report
$coverageFile = Get-ChildItem -Recurse -Filter coverage.cobertura.xml | Select-Object -First 1
if (-not $coverageFile) {
    Write-Host "No coverage.cobertura.xml file found."
    exit 1
}

[xml]$xml = Get-Content $coverageFile.FullName
$lineRate = [math]::Round(($xml.coverage.'line-rate' * 100), 2)

Write-Host "Coverage: $lineRate%"

# Update the badge in README.md
$readmePath = "README.md"
$badgePattern = 'https://img.shields.io/badge/coverage-[0-9]+(\.[0-9]+)?%25-[a-z]+.svg'
$newBadge = "https://img.shields.io/badge/coverage-$lineRate%25-brightgreen.svg"

$readme = Get-Content $readmePath -Raw
$updated = [System.Text.RegularExpressions.Regex]::Replace($readme, $badgePattern, $newBadge)
Set-Content -Path $readmePath -Value $updated

# Output the new coverage value for workflow use
Write-Output "::set-output name=coverage::$lineRate"

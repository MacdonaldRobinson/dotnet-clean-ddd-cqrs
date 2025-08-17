$ProjectName = "TestApp.Tests"
$CodeCoveragePath = "$ProjectName/CodeCoverage"
$Threshold = 90

# Run tests with coverage (using Coverlet collector, not XPlat)
dotnet test "$ProjectName/$ProjectName.csproj "`
    --collect:"XPlat Code Coverage" `
    --settings "$CodeCoveragePath/coverage.runsettings" `
    --results-directory "$CodeCoveragePath/TestResults"`

#Generate coverage reports (HTML + TextSummary)
reportgenerator `
    -reports:"$CodeCoveragePath/TestResults/**/*.xml" `
    -targetdir:"$CodeCoveragePath/TestResults/coverage-report" `
    -reporttypes:"Html;TextSummary" `

# Print summary to console
cat "$CodeCoveragePath/TestResults/coverage-report/Summary.txt"

# Fail build if total line coverage is below threshold
$SummaryFile = "$CodeCoveragePath/TestResults/coverage-report/Summary.txt"
$LineCoverage = Select-String -Path $SummaryFile -Pattern "Line coverage:\s+(\d+\.?\d*)%" | ForEach-Object {
    [double]($_.Matches[0].Groups[1].Value)
}

if ($LineCoverage -lt $Threshold) {
    Write-Error "Total line coverage $LineCoverage% is below threshold $Threshold%"
    exit 1
}
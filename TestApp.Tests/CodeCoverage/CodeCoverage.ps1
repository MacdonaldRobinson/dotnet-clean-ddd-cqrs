$ProjectName = "TestApp.Tests"
$CodeCoveragePath = "$ProjectName/CodeCoverage"

# Run tests with coverage (using Coverlet collector, not XPlat)
dotnet test "$ProjectName/$ProjectName.csproj "`
    --collect:"XPlat Code Coverage" `
    --settings "$CodeCoveragePath/coverage.runsettings" `
    --results-directory "$CodeCoveragePath/TestResults"`
    /p:UseSourceLink=false

#Generate coverage reports (HTML + TextSummary)
reportgenerator `
    -reports:"$CodeCoveragePath/TestResults/**/*.xml" `
    -targetdir:"$CodeCoveragePath/TestResults/coverage-report" `
    -reporttypes:"Html;TextSummary" `

# Print summary to console
cat "$CodeCoveragePath/TestResults/coverage-report/Summary.txt"

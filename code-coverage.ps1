# run-tests.ps1

# Run tests with coverage (using Coverlet collector, not XPlat)
dotnet test TestApp.Tests/TestApp.Tests.csproj `
    --collect:"XPlat Code Coverage" `
    --settings TestApp.Tests/coverlet/coverage.runsettings `
    --results-directory TestApp.Tests/TestResults `
    /p:UseSourceLink=false

# Generate coverage reports (HTML + TextSummary)
reportgenerator `
    -reports:"TestApp.Tests/TestResults/**/*.xml" `
    -targetdir:"TestApp.Tests/TestResults/coverage-report" `
    -reporttypes:"Html;TextSummary" `
    -sourcedirs:"TestApp.Application;TestApp.Domain;TestApp.Infrastructure;TestApp.WebApi"

# Print summary to console
cat TestApp.Tests/TestResults/coverage-report/Summary.txt

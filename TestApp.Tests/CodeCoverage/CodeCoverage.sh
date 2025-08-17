#!/bin/sh

ProjectName="TestApp.Tests"
CodeCoveragePath="$ProjectName/CodeCoverage"
Threshold=90

# Run tests with coverage
dotnet test "$ProjectName/$ProjectName.csproj" \
    --collect:"XPlat Code Coverage" \
    --settings "$CodeCoveragePath/coverage.runsettings" \
    --results-directory "$CodeCoveragePath/TestResults"

# Generate coverage reports
reportgenerator \
    -reports:"$CodeCoveragePath/TestResults/**/*.xml" \
    -targetdir:"$CodeCoveragePath/TestResults/coverage-report" \
    -reporttypes:"Html;TextSummary"

# Print summary
cat "$CodeCoveragePath/TestResults/coverage-report/Summary.txt"

# --- Threshold check without bc ---
SummaryFile="$CodeCoveragePath/TestResults/coverage-report/Summary.txt"
# Extract numeric part of line coverage
LineCoverage=$(grep "Line coverage:" "$SummaryFile" | awk '{print $3}' | tr -d '%')
# Take integer part for comparison
LineCoverageInt=${LineCoverage%.*}

if [ "$LineCoverageInt" -lt "$Threshold" ]; then
    echo "ERROR: Total line coverage $LineCoverage% is below threshold $Threshold%"
    exit 1
fi

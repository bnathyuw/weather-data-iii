using FluentAssertions;
using NUnit.Framework;
using static System.StringSplitOptions;
using static WeatherData.III.AcceptanceTests.AnalyticsTestJig;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class MonthlyMaximumShould
    {
        [Test]
        public void ShowMaximumTemperatureForEachMonthOfTheYear()
        {
            // Given some known input data
            CopyToDataRoot("input\\metOfficeObservations\\aberporthdata.txt");
            // When the monthly maximum script is run
            Run(AnalyticsScript("monthlyMaximum.usql"));
            // And the results are read
            var output = ReadOutput("monthlyMaximum.csv");
            // Then the report has twelve lines (should it?)
            var lines = output.Split(new []{'\r', '\n'}, RemoveEmptyEntries);
            lines.Length.Should().Be(12);
            // One for each month of the year
        }
    }
}
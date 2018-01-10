using NUnit.Framework;
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
            // And the results are read
            // Then the report has twelve lines
            // One for each month of the year
        }
    }
}
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using WeatherData.III.Objects;
using WeatherData.III.Objects.Adla;
using static WeatherData.III.Analytics.Tests.AnalyticsTestJig;

namespace WeatherData.III.Analytics.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class MonthlyMaximumShould
    {
        private const string Location = "narnia";
        private const int Year = 1999;
        private const int Month = 5;
        private const double MaximumTemperature = 12.3;

        [SetUp]
        public void SetUp()
        {
            CreateInputFolder("metOfficeObservations");
        }

        [Test]
        public void ExtractExpectedValues()
        {
            var input = new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = MaximumTemperature};
            Write($"input\\metOfficeObservations\\{Location}data.txt", input);
            Run(AnalyticsScript("monthlyMaximum.usql"));
            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");
            output.Should().Be(new MaximumTemperatureOutput{Location = Location,Year = Year,Month = Month,MaximumTemperature = MaximumTemperature});
        }
    }
}

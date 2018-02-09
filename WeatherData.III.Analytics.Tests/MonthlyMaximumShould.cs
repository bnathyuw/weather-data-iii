using FluentAssertions;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using static WeatherData.III.Analytics.Tests.AnalyticsTestJig;

namespace WeatherData.III.Analytics.Tests
{
    [TestFixture]
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
            var input = new MaximumTemperatureInput
            {
                Year = Year,
                Month = Month,
                MaximumTemperature = MaximumTemperature
            };
            Write($"input\\metOfficeObservations\\{Location}data.txt", input);

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");
            output.Should().Be(new MaximumTemperatureOutput
            {
                Location = Location,
                Year = Year,
                Month = Month,
                MaximumTemperature = MaximumTemperature
            });
        }

        [Test]
        public void SelectMaximumValueFromInput()
        {
            Write($"input\\metOfficeObservations\\{Location}data.txt",
                new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = 1},
                new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = 2},
                new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = 3},
                new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = 4},
                new MaximumTemperatureInput {Year = Year, Month = Month, MaximumTemperature = 5});

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Should().Be(new MaximumTemperatureOutput
            {
                Location = Location,
                Year = Year,
                Month = Month,
                MaximumTemperature = 5
            });
        }
    }
}

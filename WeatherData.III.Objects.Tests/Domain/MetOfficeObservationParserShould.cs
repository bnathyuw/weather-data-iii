using FluentAssertions;
using NUnit.Framework;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Domain
{
    [TestFixture]
    public class MetOfficeObservationParserShould
    {
        private MetOfficeObservationParser _metOfficeObservationParser;

        [SetUp]
        public void SetUp()
        {
            _metOfficeObservationParser = new MetOfficeObservationParser();
        }

        [Test]
        public void ParseYearFromFirstElement()
        {
            var metOfficeObservation = _metOfficeObservationParser.Parse("1979 2 3");

            metOfficeObservation.Year.Should().Be(1979);
        }

        [Test]
        public void ParseMonthFromSecondElement()
        {
            var metOfficeObservation = _metOfficeObservationParser.Parse("1979 11 3");

            metOfficeObservation.Month.Should().Be(11);
        }

        [Test]
        public void ParseMaximumTemperatureFromThirdElement()
        {
            var metOfficeObservation = _metOfficeObservationParser.Parse("1979 11 12.3");

            metOfficeObservation.MaximumTemperature.Should().Be(12.3);
        }

        [Test]
        public void ParseNullMaximumTemperatureFromWhenNoObservationSupplied()
        {
            var metOfficeObservation = _metOfficeObservationParser.Parse("1979 11 ---");

            metOfficeObservation.MaximumTemperature.Should().Be(null);
        }
    }
}
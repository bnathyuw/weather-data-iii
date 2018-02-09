using FluentAssertions;
using NUnit.Framework;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Domain
{
    [TestFixture]
    public class CreateObservationShould
    {
        private CreateObservation _createObservation;

        [SetUp]
        public void SetUp()
        {
            _createObservation = new CreateObservation();
        }

        [Test]
        public void ParseYearFromFirstElement()
        {
            var metOfficeObservation = _createObservation.FromLine("1979 2 3");

            metOfficeObservation.Year.Should().Be(1979);
        }

        [Test]
        public void ParseMonthFromSecondElement()
        {
            var metOfficeObservation = _createObservation.FromLine("1979 11 3");

            metOfficeObservation.Month.Should().Be(11);
        }

        [Test]
        public void ParseMaximumTemperatureFromThirdElement()
        {
            var metOfficeObservation = _createObservation.FromLine("1979 11 12.3");

            metOfficeObservation.MaximumTemperature.Should().Be(12.3);
        }

        [Test]
        public void ParseNullMaximumTemperatureFromWhenNoObservationSupplied()
        {
            var metOfficeObservation = _createObservation.FromLine("1979 11 ---");

            metOfficeObservation.MaximumTemperature.Should().Be(null);
        }
    }
}
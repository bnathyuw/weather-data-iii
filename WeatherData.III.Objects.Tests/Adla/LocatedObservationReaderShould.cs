using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class LocatedObservationReaderShould
    {
        private const string Location = "Clerkenwell";
        private const int Year = 2018;
        private const int Month = 2;
        private const double MaximumTemperature = 12.3;

        private IRow _input;

        private LocatedObservationReader _locatedObservationReader;

        [SetUp]
        public void SetUp()
        {
            _input = Substitute.For<IRow>();

            _locatedObservationReader = new LocatedObservationReader();
        }

        [Test]
        public void ReadValuesFromInput()
        {
            _input.Get<string>("location").Returns(Location);
            _input.Get<int>("year").Returns(Year);
            _input.Get<int>("month").Returns(Month);
            _input.Get<double?>("maximumTemperature").Returns(MaximumTemperature);

            var locatedObservation = _locatedObservationReader.ReadFrom(_input);

            locatedObservation.Should().BeEquivalentTo(new LocatedObservation
            {
                Location = Location,
                MaximumTemperature = MaximumTemperature,
                Year = Year,
                Month = Month
            });
        }
    }
}
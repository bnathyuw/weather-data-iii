using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class RowFactoryShould
    {
        private RowFactory _rowFactory;
        private IUpdatableRow _output;
        private readonly IRow _expectedRow = Substitute.For<IRow>();

        [SetUp]
        public void SetUp()
        {
            _rowFactory = new RowFactory();
            _output = Substitute.For<IUpdatableRow>();
        }

        [Test]
        public void CollaborateWithOutputToCreateOutputRow(
            [Values(1999, 2001)] int year,
            [Values(1, 12)] int month,
            [Values(null, -12.3, 12.3)] double? maximumTemperature)
        {
            var metOfficeObservation = new MetOfficeObservation {Year = year, Month = month, MaximumTemperature = maximumTemperature};
            _rowFactory.Create(_output, metOfficeObservation);

            Received.InOrder(() =>
            {
                _output.Set("year", year);
                _output.Set("month", month);
                _output.Set("maximumTemperature", maximumTemperature);
                _output.AsReadOnly();
            });
        }

        [Test]
        public void ReturnCreatedRow()
        {
            _output.AsReadOnly().Returns(_expectedRow);

            var row = _rowFactory.Create(_output, new MetOfficeObservation());

            row.Should().Be(_expectedRow);
        }
    }
}
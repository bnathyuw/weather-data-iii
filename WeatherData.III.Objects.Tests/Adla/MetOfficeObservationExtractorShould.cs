using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class MetOfficeObservationExtratorShould
    {
        private MetOfficeObservationReader _metOfficeObservationReader;
        private MetOfficeObservationExtractor _metOfficeObservationExtractor;
        private IUnstructuredReader _input;
        private IUpdatableRow _output;

        private readonly IEnumerable<string> _lines = new[] {"Line1", "Line2", "Line3"};
        private readonly IRow _row1 = Substitute.For<IRow>();
        private readonly IRow _row2 = Substitute.For<IRow>();
        private readonly IRow _row3 = Substitute.For<IRow>();
        private readonly MetOfficeObservation _observation1 = new MetOfficeObservation{Year = 2001, Month = 1, MaximumTemperature = null};
        private readonly MetOfficeObservation _observation2 = new MetOfficeObservation{Year = 2002, Month = 2, MaximumTemperature = 12.3};
        private readonly MetOfficeObservation _observation3 = new MetOfficeObservation{Year = 2003, Month = 3, MaximumTemperature = 23.4};

        private List<IRow> _actualRows;

        [SetUp]
        public void SetUp()
        {
            _input = Substitute.For<IUnstructuredReader>();
            _input.BaseStream.Returns(StreamWithLines(_lines));

            _metOfficeObservationReader = Substitute.For<MetOfficeObservationReader>((CreateObservation)null);
            _metOfficeObservationReader.ReadObservations(Arg.Any<IEnumerable<string>>())
                .Returns(new[] {_observation1, _observation2, _observation3});

            _output = Substitute.For<IUpdatableRow>();
            _output.AsReadOnly().Returns(_row1, _row2, _row3);

            _metOfficeObservationExtractor = new MetOfficeObservationExtractor(_metOfficeObservationReader);

            _actualRows = _metOfficeObservationExtractor.Extract(_input, _output).ToList();
        }

        private static MemoryStream StreamWithLines(IEnumerable<string> lines) => new MemoryStream(Encoding.UTF8.GetBytes(string.Join("\r\n", lines)));

        [Test]
        public void ReadLinesFromInputStream()
        {
            _metOfficeObservationReader.Received()
                .ReadObservations(Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(_lines)));
        }

        [Test]
        public void WriteEachObservationNToOutput()
        {
            Received.InOrder(() =>
            {
                CallsToOutputWithValuesFrom(_observation1);
                CallsToOutputWithValuesFrom(_observation2);
                CallsToOutputWithValuesFrom(_observation3);
            });
        }

        private void CallsToOutputWithValuesFrom(MetOfficeObservation observation)
        {
            _output.Set("year", observation.Year);
            _output.Set("month", observation.Month);
            _output.Set("maximumTemperature", observation.MaximumTemperature);
            _output.AsReadOnly();
        }

        [Test]
        public void ReturnRowsFromOutput()
        {
            _actualRows.Should().BeEquivalentTo(_row1, _row2, _row3);
        }
    }
}

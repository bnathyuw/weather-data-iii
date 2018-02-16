using System.Collections.Generic;
using System.Linq;
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
        private MetOfficeDatasetParser _metOfficeDatasetParser;
        private MetOfficeObservationExtractor _metOfficeObservationExtractor;
        private IUpdatableRow _output;

        private readonly IEnumerable<string> _lines = new string[] { };
        private readonly IRow _row1 = Substitute.For<IRow>();
        private readonly IRow _row2 = Substitute.For<IRow>();
        private readonly IRow _row3 = Substitute.For<IRow>();
        private readonly MetOfficeObservation _observation1 = new MetOfficeObservation();
        private readonly MetOfficeObservation _observation2 = new MetOfficeObservation();
        private readonly MetOfficeObservation _observation3 = new MetOfficeObservation();

        private InputReader _inputReader;
        private IUnstructuredReader _input;
        private RowFactory _rowFactory;

        [SetUp]
        public void SetUp()
        {
            _input = Substitute.For<IUnstructuredReader>();
            _output = Substitute.For<IUpdatableRow>();

            _inputReader = Substitute.For<InputReader>();
            _metOfficeDatasetParser = Substitute.For<MetOfficeDatasetParser>((MetOfficeObservationParser)null);
            _rowFactory = Substitute.For<RowFactory>();

            _metOfficeObservationExtractor = new MetOfficeObservationExtractor(_inputReader, _metOfficeDatasetParser, _rowFactory);
        }

        [Test]
        public void ReadParseWriteAndReturnValues()
        {
            _inputReader.ReadLines(_input).Returns(_lines);
            _metOfficeDatasetParser.Parse(_lines).Returns(new[] { _observation1, _observation2, _observation3 });
            _rowFactory.Create(_output, _observation1).Returns(_row1);
            _rowFactory.Create(_output, _observation2).Returns(_row2);
            _rowFactory.Create(_output, _observation3).Returns(_row3);

            var actualRows = _metOfficeObservationExtractor.Extract(_input, _output).ToList();

            actualRows.Should().BeEquivalentTo(_row1, _row2, _row3);
        }
    }
}

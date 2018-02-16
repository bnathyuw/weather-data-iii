using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class MetOfficeObservationOutputterShould
    {
        private readonly LocatedObservation _locatedObservation = new LocatedObservation();
        private const string OutputString = "Output string";

        private IRow _input;
        private IUnstructuredWriter _output;

        private LocatedObservationReader _locatedObservationReader;
        private LocatedObervationWriter _locatedObervationWriter;
        private OutputWriter _outputWriter;

        private MetOfficeObservationOutputter _metOfficeObservationOutputter;

        [SetUp]
        public void SetUp()
        {
            _input = Substitute.For<IRow>();
            _output = Substitute.For<IUnstructuredWriter>();

            _locatedObservationReader = Substitute.For<LocatedObservationReader>();
            _locatedObervationWriter = Substitute.For<LocatedObervationWriter>();
            _outputWriter = Substitute.For<OutputWriter>();

            _metOfficeObservationOutputter = new MetOfficeObservationOutputter(_locatedObservationReader, _locatedObervationWriter, _outputWriter);

        }

        [Test]
        public void WriteDataToOutput()
        {
            _locatedObservationReader.ReadFrom(_input).Returns(_locatedObservation);
            _locatedObervationWriter.OutputString(_locatedObservation).Returns(OutputString);

            _metOfficeObservationOutputter.Output(_input, _output);

            _outputWriter.Received().WriteTo(_output, OutputString);
        }
    }
}
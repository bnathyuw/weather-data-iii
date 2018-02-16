using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Domain
{
    [TestFixture]
    public class MetOfficeDatasetParserShould
    {
        private const string UnitsHeader = "              degC    degC    days      mm   hours";
        private const string Line4 = "line4";
        private const string Line5 = "line5";
        private const string Line6 = "line6";

        private readonly IEnumerable<string> _lines = new []
        {
            "line1",
            "line2",
            "line3",
            UnitsHeader,
            Line4,
            Line5,
            Line6
        };

        private readonly MetOfficeObservation _observation1 = new MetOfficeObservation();
        private readonly MetOfficeObservation _observation2 = new MetOfficeObservation();
        private readonly MetOfficeObservation _observation3 = new MetOfficeObservation();

        private MetOfficeObservationParser _metOfficeObservationParser;
        private MetOfficeDatasetParser _metOfficeDatasetParser;

        [SetUp]
        public void SetUp()
        {
            _metOfficeObservationParser = Substitute.For<MetOfficeObservationParser>();

            _metOfficeDatasetParser = new MetOfficeDatasetParser(_metOfficeObservationParser);
        }

        [Test]
        public void ReturnObservationsParsedFromLinesAfterUnitsHeader()
        {
            _metOfficeObservationParser.Parse(Line4).Returns(_observation1);
            _metOfficeObservationParser.Parse(Line5).Returns(_observation2);
            _metOfficeObservationParser.Parse(Line6).Returns(_observation3);

            var observations = _metOfficeDatasetParser.Parse(_lines).ToList();

            observations.Should().BeEquivalentTo(_observation1, _observation2, _observation3);
        }
    }
}
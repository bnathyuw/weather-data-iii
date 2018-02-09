using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Domain
{
    [TestFixture]
    public class MetOfficeObservationReaderShould
    {
        private CreateObservation _createObservation;
        private MetOfficeObservationReader _metOfficeObservationReader;

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

        [SetUp]
        public void SetUp()
        {
            _createObservation = Substitute.For<CreateObservation>();

            _metOfficeObservationReader = new MetOfficeObservationReader(_createObservation);
        }

        [Test]
        public void ReadObservationsFromLinesAfterUnitsHeader()
        {
            _metOfficeObservationReader.ReadObservations(_lines).ToList();

            Received.InOrder(() =>
            {
                _createObservation.FromLine(Line4);
                _createObservation.FromLine(Line5);
                _createObservation.FromLine(Line6);
            });
        }

        [Test]
        public void ReturnObservations()
        {
            var observation1 = new MetOfficeObservation();
            var observation2 = new MetOfficeObservation();
            var observation3 = new MetOfficeObservation();
            _createObservation.FromLine(Arg.Any<string>()).Returns(observation1, observation2, observation3);

            var observations = _metOfficeObservationReader.ReadObservations(_lines).ToList();

            observations.Should().BeEquivalentTo(observation1, observation2, observation3);
        }
    }
}
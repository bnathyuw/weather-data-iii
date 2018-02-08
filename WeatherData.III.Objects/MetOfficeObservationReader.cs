using System.Collections.Generic;
using System.Linq;

namespace WeatherData.III.Objects
{
    internal interface IMetOfficeObservationReader
    {
        IEnumerable<MetOfficeObservation> ReadObservations(IEnumerable<string> lines);
    }

    internal class MetOfficeObservationReader : IMetOfficeObservationReader
    {
        private const string UnitsLine = "              degC    degC    days      mm   hours";

        private readonly ICreateObservation _createObservation;
        
        public MetOfficeObservationReader(ICreateObservation createObservation)
        {
            _createObservation = createObservation;
        }

        public IEnumerable<MetOfficeObservation> ReadObservations(IEnumerable<string> lines)
        {
            return lines.SkipUntil(Units).Select(_createObservation.FromLine);
        }

        private static bool Units(string line) => line == UnitsLine;
    }
}
using System.Collections.Generic;
using System.Linq;

namespace WeatherData.III.Objects.Domain
{
    internal class MetOfficeObservationReader
    {
        private const string UnitsLine = "              degC    degC    days      mm   hours";

        private readonly CreateObservation _createObservation;
        
        public MetOfficeObservationReader(CreateObservation createObservation)
        {
            _createObservation = createObservation;
        }

        public virtual IEnumerable<MetOfficeObservation> ReadObservations(IEnumerable<string> lines)
        {
            return lines.SkipUntil(Units).Select(_createObservation.FromLine);
        }

        private static bool Units(string line) => line == UnitsLine;
    }
}
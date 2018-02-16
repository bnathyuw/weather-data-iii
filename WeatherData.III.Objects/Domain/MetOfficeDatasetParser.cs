using System.Collections.Generic;
using System.Linq;

namespace WeatherData.III.Objects.Domain
{
    internal class MetOfficeDatasetParser
    {
        private const string UnitsLine = "              degC    degC    days      mm   hours";

        private readonly MetOfficeObservationParser _metOfficeObservationParser;
        
        public MetOfficeDatasetParser(MetOfficeObservationParser metOfficeObservationParser)
        {
            _metOfficeObservationParser = metOfficeObservationParser;
        }

        public virtual IEnumerable<MetOfficeObservation> Parse(IEnumerable<string> lines) => lines.SkipUntil(Units).Select(_metOfficeObservationParser.Parse);

        private static bool Units(string line) => line == UnitsLine;
    }
}
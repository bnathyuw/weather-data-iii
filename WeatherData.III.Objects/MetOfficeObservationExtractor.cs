using System.Collections.Generic;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            throw new System.NotImplementedException();
        }
    }
}
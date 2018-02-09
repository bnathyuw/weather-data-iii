using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationExtractor : IExtractor
    {
        private readonly JavaScriptSerializer _javaScriptSerializer;

        public MetOfficeObservationExtractor()
        {
            _javaScriptSerializer = new JavaScriptSerializer();
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (var streamReader = new StreamReader(input.BaseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    var maximumTemperatureInput = _javaScriptSerializer.Deserialize<MaximumTemperatureInput>(streamReader.ReadLine());
                    yield return maximumTemperatureInput.WriteTo(output);
                }
                
            }
        }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationExtractor : IExtractor
    {
        public static MetOfficeObservationExtractor WithDefaultDependencies()
        {
            return new MetOfficeObservationExtractor();
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            foreach (var lineStream in input.Split(Encoding.UTF8.GetBytes("\r\n")))
            {
                var serializer = new DataContractJsonSerializer(typeof(MaximumTemperatureInput));
                var thing = (MaximumTemperatureInput)serializer.ReadObject(lineStream);
                yield return thing.WriteTo(output);

            }
        }
    }
}
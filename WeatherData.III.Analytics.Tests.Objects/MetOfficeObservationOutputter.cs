using System.Runtime.Serialization.Json;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationOutputter : IOutputter
    {
        public override void Output(IRow input, IUnstructuredWriter output)
        {
            var serializer = new DataContractJsonSerializer(typeof(MaximumTemperatureOutput));
            var value = MaximumTemperatureOutput.ReadFrom(input);
            serializer.WriteObject(output.BaseStream, value);
        }
    }
}
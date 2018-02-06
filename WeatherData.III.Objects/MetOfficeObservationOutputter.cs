using System.IO;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationOutputter : IOutputter
    {
        public override void Output(IRow input, IUnstructuredWriter output)
        {
            using (var streamWriter = new StreamWriter(output.BaseStream))
            {
                var location = input.Get<string>("location");
                var year = input.Get<int>("year");
                var month = input.Get<int>("month");
                var maximumTemperature = input.Get<double?>("maximumTemperature");
                streamWriter.WriteLine($"The maximum temperature in {location} in {month}/{year} was {maximumTemperature} degrees Celsius");
            }
        }
    }
}

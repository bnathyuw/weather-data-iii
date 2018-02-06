using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (var streamReader = new StreamReader(input.BaseStream))
            {
                string line;
                while ((line = streamReader.ReadLine())!= "              degC    degC    days      mm   hours")
                {

                }
                while (!string.IsNullOrEmpty(line = streamReader.ReadLine()))
                {
                    var parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    output.Set("year", int.Parse(parts[0]));
                    output.Set("month", int.Parse(parts[1]));
                    output.Set("maximumTemperature", ParseNullableDouble(parts[2]));
                    yield return output.AsReadOnly();
                }
            }
        }

        private static double? ParseNullableDouble(string part)
        {
            return part == "---" ? (double?) null : double.Parse(part);
        }
    }
}
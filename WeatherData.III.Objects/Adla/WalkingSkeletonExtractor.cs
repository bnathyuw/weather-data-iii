using System.Collections.Generic;
using System.IO;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    public class WalkingSkeletonExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (var streamReader = new StreamReader(input.BaseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var value = int.Parse(line);
                    output.Set("value", value);
                    yield return output.AsReadOnly();
                }

            }
        }
    }
}
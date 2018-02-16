using System.Collections.Generic;
using System.IO;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    internal class InputReader
    {
        public virtual IEnumerable<string> ReadLines(IUnstructuredReader input)
        {
            using (var streamReader = new StreamReader(input.BaseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }
    }
}
using System.IO;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    internal class OutputWriter
    {
        public virtual void WriteTo(IUnstructuredWriter output, string outputString)
        {
            using (var streamWriter = new StreamWriter(output.BaseStream))
            {
                streamWriter.WriteLine(outputString);
            }
        }
    }
}
using System.IO;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class WalkingSkeletonOutputter : IOutputter
    {
        public override void Output(IRow input, IUnstructuredWriter output)
        {
            using (var streamWriter = new StreamWriter(output.BaseStream))
            {
                var value = input.Get<int>("value");
                streamWriter.WriteLine(value);
            }
        }
    }
}
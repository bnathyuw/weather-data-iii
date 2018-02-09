using System.IO;
using System.Web.Script.Serialization;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationOutputter : IOutputter
    {
        private readonly JavaScriptSerializer _javaScriptSerializer;

        public MetOfficeObservationOutputter()
        {
            _javaScriptSerializer = new JavaScriptSerializer();
        }

        public override void Output(IRow input, IUnstructuredWriter output)
        {
            using (var streamWriter = new StreamWriter(output.BaseStream))
            {
                streamWriter.WriteLine(_javaScriptSerializer.Serialize(MaximumTemperatureOutput.ReadFrom(input)));
            }
        }
    }
}
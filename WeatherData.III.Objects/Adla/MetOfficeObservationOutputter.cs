using System.IO;
using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationOutputter : IOutputter
    {
        private readonly LocatedObervationWriter _locatedObervationWriter;

        internal MetOfficeObservationOutputter(LocatedObervationWriter locatedObervationWriter)
        {
            _locatedObervationWriter = locatedObervationWriter;
        }

        public override void Output(IRow input, IUnstructuredWriter output)
        {
            using (var streamWriter = new StreamWriter(output.BaseStream))
            {
                var locatedObservation = ReadFrom(input);
                streamWriter.WriteLine(_locatedObervationWriter.OutputString(locatedObservation));
            }
        }

        private static LocatedObservation ReadFrom(IRow input)
        {
            return new LocatedObservation
            {
                Location = input.Get<string>("location"),
                Month = input.Get<int>("month"),
                Year = input.Get<int>("year"),
                MaximumTemperature = input.Get<double?>("maximumTemperature")
            };
        }
    }
}

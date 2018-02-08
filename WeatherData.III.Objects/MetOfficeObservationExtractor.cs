using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MetOfficeObservationExtractor : IExtractor
    {
        public static MetOfficeObservationExtractor WithDefaultDependencies()
        {
            return new MetOfficeObservationExtractor(new MetOfficeObservationReader(new CreateObservation()));
        }

        private readonly IMetOfficeObservationReader _metOfficeObservationReader;

        internal MetOfficeObservationExtractor(IMetOfficeObservationReader metOfficeObservationReader)
        {
            _metOfficeObservationReader = metOfficeObservationReader;
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            return _metOfficeObservationReader.ReadObservations(ReadLines(input))
                .Select(metOfficeObservation => Write(output, metOfficeObservation));
        }

        private static IEnumerable<string> ReadLines(IUnstructuredReader input)
        {
            using (var streamReader = new StreamReader(input.BaseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }

        private static IRow Write(IUpdatableRow output, MetOfficeObservation metOfficeObservation)
        {
            output.Set("year", metOfficeObservation.Year);
            output.Set("month", metOfficeObservation.Month);
            output.Set("maximumTemperature", metOfficeObservation.MaximumTemperature);
            return output.AsReadOnly();
        }
    }
}
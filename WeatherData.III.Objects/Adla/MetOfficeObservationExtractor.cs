using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationExtractor : IExtractor
    {
        private readonly MetOfficeObservationReader _metOfficeObservationReader;

        internal MetOfficeObservationExtractor(MetOfficeObservationReader metOfficeObservationReader)
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
using System.Collections.Generic;
using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationExtractor : IExtractor
    {
        private readonly MetOfficeDatasetParser _metOfficeDatasetParser;
        private readonly InputReader _inputReader;
        private readonly RowFactory _rowFactory;

        internal MetOfficeObservationExtractor(InputReader inputReader,
            MetOfficeDatasetParser metOfficeDatasetParser, RowFactory rowFactory)
        {
            _metOfficeDatasetParser = metOfficeDatasetParser;
            _inputReader = inputReader;
            _rowFactory = rowFactory;
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            var inputLines = _inputReader.ReadLines(input);
            var metOfficeObservations = _metOfficeDatasetParser.Parse(inputLines);
            foreach (var metOfficeObservation in metOfficeObservations)
            {
                yield return _rowFactory.Create(output, metOfficeObservation);
            }
        }
    }
}
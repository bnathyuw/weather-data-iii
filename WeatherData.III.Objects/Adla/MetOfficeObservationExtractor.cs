using System.Collections.Generic;
using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationExtractor : IExtractor
    {
        private readonly MetOfficeDatasetParser _metOfficeDatasetParser;
        private readonly InputReader _inputReader;
        private readonly OutputWriter _outputWriter;

        internal MetOfficeObservationExtractor(InputReader inputReader,
            MetOfficeDatasetParser metOfficeDatasetParser, OutputWriter outputWriter)
        {
            _metOfficeDatasetParser = metOfficeDatasetParser;
            _inputReader = inputReader;
            _outputWriter = outputWriter;
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            var inputLines = _inputReader.ReadLines(input);
            var metOfficeObservations = _metOfficeDatasetParser.Parse(inputLines);
            foreach (var metOfficeObservation in metOfficeObservations)
            {
                yield return _outputWriter.Write(output, metOfficeObservation);
            }
        }
    }
}
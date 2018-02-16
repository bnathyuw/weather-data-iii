using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationOutputter : IOutputter
    {
        private readonly LocatedObervationWriter _locatedObervationWriter;
        private readonly LocatedObservationReader _locatedObservationReader;
        private readonly OutputWriter _outputWriter;

        internal MetOfficeObservationOutputter(LocatedObservationReader locatedObservationReader,
            LocatedObervationWriter locatedObervationWriter, OutputWriter outputWriter)
        {
            _locatedObservationReader = locatedObservationReader;
            _locatedObervationWriter = locatedObervationWriter;
            _outputWriter = outputWriter;
        }

        public override void Output(IRow input, IUnstructuredWriter output)
        {
            var locatedObservation = _locatedObservationReader.ReadFrom(input);
            var outputString = _locatedObervationWriter.OutputString(locatedObservation);
            _outputWriter.WriteTo(output, outputString);
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Microsoft.Analytics.Interfaces;
using static System.Text.Encoding;

namespace WeatherData.III.Objects.Adla
{
    internal class MetOfficeObservationExtractor : IExtractor
    {
        private readonly byte[] _rowdelimiter = UTF8.GetBytes("\r\n");
        private readonly DataContractJsonSerializer _dataContractJsonSerializer;

        public MetOfficeObservationExtractor()
        {
            _dataContractJsonSerializer = new DataContractJsonSerializer(typeof(MaximumTemperatureInput));
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            foreach (var lineStream in input.Split(_rowdelimiter))
            {
                var maximumTemperatureInput = (MaximumTemperatureInput)_dataContractJsonSerializer.ReadObject(lineStream);
                yield return maximumTemperatureInput.WriteTo(output);
            }
        }
    }
}
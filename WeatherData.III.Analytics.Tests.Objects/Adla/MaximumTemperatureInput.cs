using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    public struct MaximumTemperatureInput
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double? MaximumTemperature { get; set; }

        public IRow WriteTo(IUpdatableRow output)
        {
            output.Set("year", Year);
            output.Set("month", Month);
            output.Set("maximumTemperature", MaximumTemperature);
            return output.AsReadOnly();
        }
    }
}
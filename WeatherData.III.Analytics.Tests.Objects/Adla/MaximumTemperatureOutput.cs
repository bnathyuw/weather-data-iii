using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects.Adla
{
    public struct MaximumTemperatureOutput
    {
        public string Location { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double? MaximumTemperature { get; set; }

        public static MaximumTemperatureOutput ReadFrom(IRow input)
        {
            return new MaximumTemperatureOutput
            {
                Location = input.Get<string>("location"),
                Year = input.Get<int>("year"),
                Month = input.Get<int>("month"),
                MaximumTemperature = input.Get<double?>("maximumTemperature")
            };
        }
    }
}
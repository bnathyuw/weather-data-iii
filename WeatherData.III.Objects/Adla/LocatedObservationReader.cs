using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class LocatedObservationReader
    {
        public virtual LocatedObservation ReadFrom(IRow input)
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
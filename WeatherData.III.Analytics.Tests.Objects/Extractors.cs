using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Adla;

namespace WeatherData.III.Objects
{
    public static class Extractors
    {
        public static IExtractor MetOfficeObservations => new MetOfficeObservationExtractor();
    }
}
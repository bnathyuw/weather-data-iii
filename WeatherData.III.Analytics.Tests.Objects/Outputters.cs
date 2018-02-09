using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Adla;

namespace WeatherData.III.Objects
{
    public static class Outputters
    {
        public static IOutputter MetOfficeObservations => new MetOfficeObservationOutputter();
    }
}
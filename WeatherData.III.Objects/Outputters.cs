using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects
{
    public static class Outputters
    {
        public static IOutputter WalkingSkeleton => new WalkingSkeletonOutputter();

        public static IOutputter MetOfficeObservations => new MetOfficeObservationOutputter(new LocatedObervationWriter());

    }
}
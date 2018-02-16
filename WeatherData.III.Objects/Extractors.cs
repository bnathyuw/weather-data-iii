using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects
{
    public static class Extractors
    {
        public static IExtractor WalkingSkeleton => new WalkingSkeletonExtractor();

        public static IExtractor MetOfficeObservations => new MetOfficeObservationExtractor(new InputReader(), new MetOfficeDatasetParser(new MetOfficeObservationParser()), new RowFactory());
    }
}
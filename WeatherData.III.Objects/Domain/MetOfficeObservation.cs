namespace WeatherData.III.Objects.Domain
{
    public struct MetOfficeObservation
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double? MaximumTemperature { get; set; }
    }
}
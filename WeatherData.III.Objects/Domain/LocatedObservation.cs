namespace WeatherData.III.Objects.Domain
{
    public struct LocatedObservation
    {
        public string Location { get;  set; }
        public int Month { get;  set; }
        public int Year { get;  set; }
        public double? MaximumTemperature { get;  set; }
    }
}
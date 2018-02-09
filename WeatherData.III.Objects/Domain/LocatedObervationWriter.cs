namespace WeatherData.III.Objects.Domain
{
    internal class LocatedObervationWriter
    {
        internal virtual string OutputString(LocatedObservation locatedObservation)
        {
            return $"The maximum temperature in {locatedObservation.Location} in {locatedObservation.Month}/{locatedObservation.Year} was {locatedObservation.MaximumTemperature} degrees Celsius";
        }
    }
}
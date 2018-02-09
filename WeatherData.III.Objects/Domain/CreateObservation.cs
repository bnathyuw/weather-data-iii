using System;

namespace WeatherData.III.Objects.Domain
{
    internal class CreateObservation
    {
        private const string NoObservation = "---";

        public virtual MetOfficeObservation FromLine(string line) =>
            ObservationFromParts(PartsSeparatedBySpaces(line));

        private static string[] PartsSeparatedBySpaces(string line) => line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

        private static MetOfficeObservation ObservationFromParts(string[] parts) => new MetOfficeObservation
        {
            Year = ReadYear(parts),
            Month = ReadMonth(parts),
            MaximumTemperature = ReadMaximumTemperature(parts)
        };

        private static int ReadYear(string[] parts) => int.Parse(parts[0]);

        private static int ReadMonth(string[] parts) => int.Parse(parts[1]);

        private static double? ReadMaximumTemperature(string[] parts) => ParseNullableDouble(parts[2]);

        private static double? ParseNullableDouble(string part) => part == NoObservation ? (double?) null : double.Parse(part);
    }
}
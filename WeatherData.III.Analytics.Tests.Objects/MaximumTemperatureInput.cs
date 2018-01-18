using Microsoft.Analytics.Interfaces;

namespace WeatherData.III.Objects
{
    public class MaximumTemperatureInput
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double? MaximumTemperature { get; set; }

        public IRow WriteTo(IUpdatableRow output)
        {
            output.Set("year", Year);
            output.Set("month", Month);
            output.Set("maximumTemperature", MaximumTemperature);
            return output.AsReadOnly();
        }
    }

    public class MaximumTemperatureOutput
    {
        public string Location { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double? MaximumTemperature { get; set; }

        protected bool Equals(MaximumTemperatureOutput other)
        {
            return string.Equals(Location, other.Location) && Year == other.Year && Month == other.Month && MaximumTemperature.Equals(other.MaximumTemperature);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MaximumTemperatureOutput) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ Month;
                hashCode = (hashCode * 397) ^ MaximumTemperature.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Location)}: {Location}, {nameof(Year)}: {Year}, {nameof(Month)}: {Month}, {nameof(MaximumTemperature)}: {MaximumTemperature}";
        }

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
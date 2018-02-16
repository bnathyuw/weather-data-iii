﻿using Microsoft.Analytics.Interfaces;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Adla
{
    internal class OutputWriter
    {
        public virtual IRow Write(IUpdatableRow output, MetOfficeObservation metOfficeObservation)
        {
            output.Set("year", metOfficeObservation.Year);
            output.Set("month", metOfficeObservation.Month);
            output.Set("maximumTemperature", metOfficeObservation.MaximumTemperature);
            return output.AsReadOnly();
        }
    }
}
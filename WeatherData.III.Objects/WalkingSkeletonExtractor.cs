using Microsoft.Analytics.Interfaces;
using System;
using System.Collections.Generic;

namespace WeatherData.III.Objects
{
    public class WalkingSkeletonExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            throw new NotImplementedException();
        }
    }
}
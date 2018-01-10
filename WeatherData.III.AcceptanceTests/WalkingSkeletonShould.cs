using FluentAssertions;
using Microsoft.Analytics.LocalRun;
using NUnit.Framework;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class WalkingSkeletonShould
    {
        [Test]
        public void Walk()
        {
            var localRunHelper = new LocalRunHelper();

            localRunHelper.DoRun().Should().BeTrue("script should execute successfully");
        }
    }
}

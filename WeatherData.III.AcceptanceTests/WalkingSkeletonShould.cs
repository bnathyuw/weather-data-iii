using NUnit.Framework;
using static WeatherData.III.AcceptanceTests.AnalyticsTestJig;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class WalkingSkeletonShould
    {
        [Test]
        public void Walk()
        {
            CopyToDataRoot("input\\walkingSkeleton\\data.txt");
            Run(AnalyticsScript("WalkingSkeleton.usql"));
        }
    }
}

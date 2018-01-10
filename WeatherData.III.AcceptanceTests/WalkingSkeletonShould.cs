using NUnit.Framework;
using static WeatherData.III.AcceptanceTests.AnalyticsTestJig;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class WalkingSkeletonShould
    {
        [SetUp]
        public void SetUp()
        {
            CopyToDataRoot("input\\walkingSkeleton\\data.txt");
        }

        [Test]
        public void Walk()
        {
            Run(AnalyticsScript("WalkingSkeleton.usql"));
        }
    }
}

using FluentAssertions;
using Microsoft.Analytics.LocalRun;
using NUnit.Framework;
using static System.IO.Path;
using static NUnit.Framework.TestContext;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class WalkingSkeletonShould
    {
        [Test]
        public void Walk()
        {
            var localRunHelper = new LocalRunHelper
            {
                ScriptPath = PathToAnalyticsScript("WalkingSkeleton.usql")
            };

            localRunHelper.DoRun().Should().BeTrue("script should execute successfully");
        }

        private static string PathToAnalyticsScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.Analytics", scriptName);
        }
    }
}

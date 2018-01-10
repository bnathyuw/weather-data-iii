using FluentAssertions;
using Microsoft.Analytics.LocalRun;
using NUnit.Framework;
using static System.IO.Directory;
using static System.IO.File;
using static System.IO.Path;
using static NUnit.Framework.TestContext;

namespace WeatherData.III.AcceptanceTests
{
    [TestFixture]
    public class WalkingSkeletonShould
    {
        private string _dataRoot;

        [SetUp]
        public void SetUp()
        {
            _dataRoot = Combine(CurrentContext.TestDirectory, "UsqlDataRoot");
            CreateDirectory(_dataRoot);
            CreateDirectory(Combine(_dataRoot, "input", "walkingSkeleton"));
            Copy(Combine(CurrentContext.TestDirectory, "input", "walkingSkeleton", "data.txt"),
                Combine(_dataRoot, "input", "walkingSkeleton", "data.txt"), true);
        }

        [Test]
        public void Walk()
        {
            var localRunHelper = new LocalRunHelper
            {
                ScriptPath = PathToAnalyticsScript("WalkingSkeleton.usql"),
                DataRoot = _dataRoot
            };

            localRunHelper.DoRun().Should().BeTrue("script should execute successfully");
        }

        private static string PathToAnalyticsScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.Analytics", scriptName);
        }
    }
}

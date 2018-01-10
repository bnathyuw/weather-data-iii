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
            CopyToDataRoot("input\\walkingSkeleton\\data.txt");
            CopyToDataRoot("WeatherData.III.Objects.dll");

            Run(DataDefinitionScript("CreateDatabase.usql"));
            Run(DataDefinitionScript("RegisterObjectsAssembly.usql"));
        }

        [Test]
        public void Walk()
        {
            Run(AnalyticsScript("WalkingSkeleton.usql"));
        }

        private void Run(string script)
        {
            var localRunHelper = new LocalRunHelper
            {
                ScriptPath = script,
                DataRoot = _dataRoot
            };

            localRunHelper.DoRun().Should().BeTrue("script should execute successfully");
        }

        private static string AnalyticsScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.Analytics", scriptName);
        }

        private void CopyToDataRoot(string file)
        {
            Copy(Combine(CurrentContext.TestDirectory, file), Combine(_dataRoot, file), true);
        }

        private static string DataDefinitionScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.DataDefinition", scriptName);
        }
    }
}

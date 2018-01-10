using FluentAssertions;
using Microsoft.Analytics.LocalRun;
using NUnit.Framework;
using static System.IO.Directory;
using static System.IO.File;
using static System.IO.Path;
using static NUnit.Framework.TestContext;

namespace WeatherData.III.AcceptanceTests
{
    [SetUpFixture]
    public class AnalyticsTestJig
    {
        private static readonly string DataRoot;

        static AnalyticsTestJig()
        {
            DataRoot = Combine(CurrentContext.TestDirectory, "UsqlDataRoot");
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            CreateDirectory(DataRoot);
            Run(DataDefinitionScript("CreateDatabase.usql"));

            CopyToDataRoot("WeatherData.III.Objects.dll");
            Run(DataDefinitionScript("RegisterObjectsAssembly.usql"));

            CreateDirectory(Combine(DataRoot, "input", "walkingSkeleton"));
        }

        public static void Run(string script)
        {
            var localRunHelper = new LocalRunHelper
            {
                ScriptPath = script,
                DataRoot = DataRoot
            };

            localRunHelper.DoRun().Should().BeTrue("script should execute successfully");
        }

        public static string AnalyticsScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.Analytics", scriptName);
        }

        public static void CopyToDataRoot(string file)
        {
            Copy(Combine(CurrentContext.TestDirectory, file), Combine(DataRoot, file), true);
        }

        private static string DataDefinitionScript(string scriptName)
        {
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.DataDefinition", scriptName);
        }
    }
}
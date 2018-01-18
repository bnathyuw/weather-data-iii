using System.IO;
using System.Runtime.Serialization.Json;
using FluentAssertions;
using Microsoft.Analytics.LocalRun;
using NUnit.Framework;
using static System.IO.Directory;
using static System.IO.File;
using static System.IO.Path;
using static NUnit.Framework.TestContext;

namespace WeatherData.III.Analytics.Tests
{
    [SetUpFixture]
    public class AnalyticsTestJig
    {
        public static readonly string DataRoot;

        static AnalyticsTestJig()
        {
            DataRoot = Combine(CurrentContext.TestDirectory, "UsqlDataRoot");
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            CreateDirectory(DataRoot);
            Run(DataDefinitionScript("CreateDatabase.usql"));

            CopyToDataRoot("WeatherData.III.Analytics.Tests.Objects.dll");
            Run(DataDefinitionScript("RegisterObjectsAssembly.usql"));

        }

        public static void CreateInputFolder(params string[] inputFolders)
        {
            Delete(Combine(DataRoot, "input"), true);
            foreach (var inputFolder in inputFolders)
            {
                CreateDirectory(Combine(DataRoot, "input", inputFolder));
            }
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
            return Combine(CurrentContext.TestDirectory, "..", "..", "..", "WeatherData.III.Analytics.Tests.DD", scriptName);
        }

        public static void Write<T>(string fileName, T input)
        {
            using (var stream = new FileStream(Combine(DataRoot, fileName), FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, input);
            }
        }

        public static T Read<T>(string fileName)
        {
            using (var stream = new FileStream(Combine(DataRoot, fileName), FileMode.Open))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T) serializer.ReadObject(stream);
            }
        }
    }
}
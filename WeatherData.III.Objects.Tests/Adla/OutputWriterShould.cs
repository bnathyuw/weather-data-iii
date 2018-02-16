using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class OutputWriterShould
    {
        private MemoryStream _memoryStream;
        private OutputWriter _outputWriter;
        private IUnstructuredWriter _output;
        private const string OutputString = "Output string";

        [SetUp]
        public void SetUp()
        {
            _outputWriter = new OutputWriter();

            _output = Substitute.For<IUnstructuredWriter>();
            _memoryStream = new MemoryStream();
            _output.BaseStream.Returns(_memoryStream);
        }

        [Test]
        public void WriteToBaseStream()
        {
            _outputWriter.WriteTo(_output, OutputString);

            StringWrittenTo(_memoryStream).Should().Be($"{OutputString}\r\n");
        }

        private static string StringWrittenTo(MemoryStream memoryStream) => GetString(memoryStream.ToArray());

        private static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }
}
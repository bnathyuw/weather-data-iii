using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using WeatherData.III.Objects.Domain;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class MetOfficeObservationOutputterShould
    {
        private LocatedObervationWriter _locatedObervationWriter;
        private MemoryStream _memoryStream;
        private const string Location = "Clerkenwell";
        private const int Year = 2018;
        private const int Month = 2;
        private const double MaximumTemperature = 12.3;
        private const string OutputString = "Output string";

        [SetUp]
        public void SetUp()
        {
            var input = Substitute.For<IRow>();
            input.Get<string>("location").Returns(Location);
            input.Get<int>("year").Returns(Year);
            input.Get<int>("month").Returns(Month);
            input.Get<double?>("maximumTemperature").Returns(MaximumTemperature);

            _locatedObervationWriter = Substitute.For<LocatedObervationWriter>();
            _locatedObervationWriter.OutputString(Arg.Any<LocatedObservation>()).Returns(OutputString);

            var output = Substitute.For<IUnstructuredWriter>();
            _memoryStream = new MemoryStream();
            output.BaseStream.Returns(_memoryStream);

            var outputter = new MetOfficeObservationOutputter(_locatedObervationWriter);

            outputter.Output(input, output);
        }

        [Test]
        public void ReadDataFromInput()
        {
            _locatedObervationWriter.Received().OutputString(new LocatedObservation
            {
                Location = Location,
                MaximumTemperature = MaximumTemperature,
                Year = Year,
                Month = Month
            });
        }

        [Test]
        public void WriteDataToOutput()
        {
            StringWrittenTo(_memoryStream).Should().Be($"{OutputString}\r\n");
        }

        private static string StringWrittenTo(MemoryStream memoryStream) => GetString(memoryStream.ToArray());

        private static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }
}
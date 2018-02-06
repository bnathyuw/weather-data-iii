using System.IO;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using static System.Text.Encoding;

namespace WeatherData.III.Objects.Tests
{
    [TestFixture]
    public class MetOfficeObservationOutputterShould
    {
        [Test]
        public void Blah()
        {
            var outputter = new MetOfficeObservationOutputter();

            var input = Substitute.For<IRow>();
            input.Get<string>("location").Returns("Clerkenwell");
            input.Get<int>("year").Returns(2018);
            input.Get<int>("month").Returns(2);
            input.Get<double?>("maximumTemperature").Returns(12.3);
            var output = Substitute.For<IUnstructuredWriter>();
            var memoryStream = new MemoryStream();
            output.BaseStream.Returns(memoryStream);
            outputter.Output(input, output);

            var bytes = memoryStream.ToArray();
            UTF8.GetString(bytes, 0, bytes.Length).Should().Be("The maximum temperature in Clerkenwell in 2/2018 was 12.3 degrees Celsius\r\n");
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace WeatherData.III.Objects.Tests
{
    [TestFixture]
    public class MetOfficeObservationExtractorShould
    {
        [Test]
        public void InteractWithOutput()
        {
            var metOfficeObservationExtractor = new MetOfficeObservationExtractor();

            var input = Substitute.For<IUnstructuredReader>();

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(Text));
            input.BaseStream.Returns(memoryStream);

            var output = Substitute.For<IUpdatableRow>();

            metOfficeObservationExtractor.Extract(input, output).ToList();

            Received.InOrder(() =>
            {
                CallsToReturnValue(output, 1958, 3, 7.6);
                CallsToReturnValue(output, 1958, 4, 10.6);
                CallsToReturnValue(output, 1958, 5, 13.4);
                CallsToReturnValue(output, 1941, 12, null);
            });
        }

        [Test]
        public void ReturnRowsFromOutput()
        {
            var metOfficeObservationExtractor = new MetOfficeObservationExtractor();

            var input = Substitute.For<IUnstructuredReader>();

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(Text));
            input.BaseStream.Returns(memoryStream);

            var output = Substitute.For<IUpdatableRow>();

            var row1 = Substitute.For<IRow>();
            var row2 = Substitute.For<IRow>();
            var row3 = Substitute.For<IRow>();
            var row4 = Substitute.For<IRow>();
            output.AsReadOnly().Returns(row1, row2, row3, row4);

            var actualRows = metOfficeObservationExtractor.Extract(input, output).ToArray();

            actualRows.Should().BeEquivalentTo(row1, row2, row3, row4);
        }

        private static void CallsToReturnValue(IUpdatableRow output, int year, int month, double? maximumTemperature)
        {
            output.Set("year", year);
            output.Set("month", month);
            output.Set("maximumTemperature", maximumTemperature);
            output.AsReadOnly();
        }

        private const string Text = @"Aberporth
Location: 224100E 252100N, Lat 52.139 Lon -4.570, 133 metres amsl
Estimated data is marked with a * after the value.
Missing data (more than 2 days missing in month) is marked by  ---.
Sunshine data taken from an automatic Kipp & Zonen sensor marked with a #, otherwise sunshine data taken from a Campbell Stokes recorder.
   yyyy  mm   tmax    tmin      af    rain     sun
              degC    degC    days      mm   hours
   1958   3    7.6     1.7       8    21.1   128.8
   1958   4   10.6     4.6       4    17.8   169.0
   1958   5   13.4     7.8       0    95.3   190.8
   1941  12    ---     ---    ---     86.5     ---
";
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.Analytics.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;

namespace WeatherData.III.Objects.Tests.Adla
{
    [TestFixture]
    public class InputReaderShould
    {
        private readonly IEnumerable<string> _lines = new[] { "Line1", "Line2", "Line3" };
        private InputReader _inputReader;
        private IUnstructuredReader _unstructuredReader;

        [SetUp]
        public void SetUp()
        {
            _unstructuredReader = Substitute.For<IUnstructuredReader>();

            _inputReader = new InputReader();
        }

        [Test]
        public void ReturnEachLineFromInputStream()
        {
            _unstructuredReader.BaseStream.Returns(StreamWithLines(_lines));

            var linesReturned = _inputReader.ReadLines(_unstructuredReader).ToList();

            linesReturned.Should().BeEquivalentTo(_lines);
        }

        private static MemoryStream StreamWithLines(IEnumerable<string> lines) => new MemoryStream(Encoding.UTF8.GetBytes(string.Join("\r\n", lines)));

    }
}
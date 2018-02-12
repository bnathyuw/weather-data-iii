using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WeatherData.III.Objects.Adla;
using static WeatherData.III.Analytics.Tests.AnalyticsTestJig;

namespace WeatherData.III.Analytics.Tests
{
    [TestFixture]
    public class MonthlyMaximumShould
    {
        private const string Location = "narnia";
        private const int Month = 5;

        [SetUp]
        public void SetUp()
        {
            CreateInputFolder("metOfficeObservations");
        }

        [Test]
        public void SelectMaximumAcrossSeveralYears()
        {
            Write($"input\\metOfficeObservations\\{Location}data.txt",
                new MaximumTemperatureInput { Year = 1991, Month = Month, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1992, Month = Month, MaximumTemperature = 5 },
                new MaximumTemperatureInput { Year = 1993, Month = Month, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1994, Month = Month, MaximumTemperature = 3 },
                new MaximumTemperatureInput { Year = 1995, Month = Month, MaximumTemperature = 2 });

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Should().BeEquivalentTo(new MaximumTemperatureOutput
            {
                Location = Location,
                Year = 1992,
                Month = Month,
                MaximumTemperature = 5
            });
        }

        [Test]
        public void SelectMaximumAcrossSeveralLocations()
        {
            Write($"input\\metOfficeObservations\\narniadata.txt",
                new MaximumTemperatureInput {Year = 1991, Month = Month, MaximumTemperature = 1});

            Write($"input\\metOfficeObservations\\theradata.txt",
                new MaximumTemperatureInput { Year = 1991, Month = Month, MaximumTemperature = 8 });

            Write($"input\\metOfficeObservations\\trisolarisdata.txt",
                new MaximumTemperatureInput { Year = 1991, Month = Month, MaximumTemperature = 4 });

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Should().BeEquivalentTo(new MaximumTemperatureOutput
            {
                Location = "thera",
                Year = 1991,
                Month = Month,
                MaximumTemperature = 8
            });
        }

        [Test]
        public void SelectMaximumForEachMonth()
        {
            Write($"input\\metOfficeObservations\\{Location}data.txt",
                new MaximumTemperatureInput { Year = 1991, Month = 1, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1991, Month = 2, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1992, Month = 1, MaximumTemperature = 5 },
                new MaximumTemperatureInput { Year = 1992, Month = 2, MaximumTemperature = 8 },
                new MaximumTemperatureInput { Year = 1993, Month = 1, MaximumTemperature = 5 },
                new MaximumTemperatureInput { Year = 1993, Month = 2, MaximumTemperature = 2 },
                new MaximumTemperatureInput { Year = 1994, Month = 1, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1994, Month = 2, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1995, Month = 1, MaximumTemperature = 7 },
                new MaximumTemperatureInput { Year = 1995, Month = 2, MaximumTemperature = 2 });

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Should().BeEquivalentTo(new MaximumTemperatureOutput
            {
                Location = Location,
                Year = 1995,
                Month = 1,
                MaximumTemperature = 7
            }, new MaximumTemperatureOutput
            {
                Location = Location,
                Year = 1992,
                Month = 2,
                MaximumTemperature = 8
            });
        }

        [Test]
        public void OutputMonthsInOrder()
        {
            Write($"input\\metOfficeObservations\\{Location}data.txt",
                new MaximumTemperatureInput { Year = 1991, Month = 1, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1991, Month = 2, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 3, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 4, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 5, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 6, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 7, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 8, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 9, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 10, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 11, MaximumTemperature = 4 },
                new MaximumTemperatureInput { Year = 1991, Month = 12, MaximumTemperature = 4 });

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Should().BeInAscendingOrder(x => x.Month);
        }

        [Test]
        public void OnlySelectOneObservationWhenTheSameReadingOccursMoreThanOnce()
        {
            Write($"input\\metOfficeObservations\\{Location}data.txt",
                new MaximumTemperatureInput { Year = 1991, Month = 1, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1992, Month = 1, MaximumTemperature = 1 },
                new MaximumTemperatureInput { Year = 1993, Month = 1, MaximumTemperature = 1 });

            Run(AnalyticsScript("monthlyMaximum.usql"));

            var output = Read<MaximumTemperatureOutput>("output\\monthlyMaximum.csv");

            output.Single().MaximumTemperature.Should().Be(1);
        }
    }
}

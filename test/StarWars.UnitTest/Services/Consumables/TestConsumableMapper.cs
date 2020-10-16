using StarWars.Service.Consumables;
using System;
using Xunit;

namespace StarWars.UnitTest.Services.Consumables
{
    public class TestConsumableMapper
    {
        [Theory]
        [InlineData("1 day", 24)]
        [InlineData("2 days", 48)]
        [InlineData("1 week", 168)]
        [InlineData("2 weeks", 336)]
        [InlineData("1 Month", 720)]
        [InlineData("2 Months", 1440)]
        [InlineData("1 Year", 8760)]
        [InlineData("2 Years", 17520)]
        public void Test_ConsumableMapper_With_ValidValues(string value, int expectedResult)
        {
            var result = ConsumableMapper.GetConsumablesDurationInHours(value);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Test_ConsumableMapper_With_NonValidValues()
        {
            int? result = ConsumableMapper.GetConsumablesDurationInHours("test");
            Assert.Null(result);
        }

        [Fact]
        public void Test_ConsumableMapper_With_UnknownTime()
        {
            Assert.Throws<NotSupportedException>(() => ConsumableMapper.GetConsumablesDurationInHours("1 test"));
        }
    }
}

using FluentAssertions;
using GildedRose.Core.Strategies;
using GildedRose.Models;

namespace GildedRose.Core.Tests.Strategies
{
    public class UpdateBackstagePassStrategyTests
    {
        [Fact]
        public void IfBackstagePass_IncreasesQualityOverTimeBy3Between5and0Days()
        {
            // Arrange
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 20
            };

            var sut = new UpdateBackstagePassStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(23);
        }

        [Fact]
        public void IfBackstagePass_QualityDropsToZeroOnOrAfterSellDate()
        {
            // Arrange
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 0,
                Quality = 20
            };

            var sut = new UpdateBackstagePassStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(0);
        }

    }
}

using FluentAssertions;
using GildedRose.Core.Strategies;
using GildedRose.Models;

namespace GildedRose.Core.Tests.Strategies
{
    public class UpdateConjuredItemStrategyTests
    {
        [Fact]
        public void BeforeSellDate_QualityDecreasesByTwo()
        {
            // Arrange
            var item = new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 };
            var sut = new UpdateConjuredItemStrategy();

            // Act
            sut.Update(item);
                    
            // Assert
            item.Quality.Should().Be(18);
        }

        [Fact]
        public void OnSellDate_QualityDecreasesByTwo()
        {
            // Arrange
            var item = new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 20 };
            var sut = new UpdateConjuredItemStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(16);
        }

        [Fact]
        public void AfterSellDate_QualityDecreasesByTwo()
        {
            // Arrange
            var item = new Item { Name = "Conjured Mana Cake", SellIn = -6, Quality = 20 };
            var sut = new UpdateConjuredItemStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(16);
        }
    }
}

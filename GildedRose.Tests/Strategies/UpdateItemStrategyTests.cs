using FluentAssertions;
using GildedRose.Core.Strategies;
using GildedRose.Models;

namespace GildedRose.Core.Tests.Strategies
{
    public class UpdateItemStrategyTests
    {
        [Fact]
        public void BeforeSellDate_QualityDecreasesByOne()
        {
            // Arrange
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var sut = new UpdateItemStrategy();

            // Act
            sut.Update(item);
                    
            // Assert
            item.Quality.Should().Be(19);
        }

        [Fact]
        public void OnSellDate_QualityDecreasesByTwo()
        {
            // Arrange
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 };
            var sut = new UpdateItemStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(18);
        }

        [Fact]
        public void AfterSellDate_QualityDecreasesByTwo()
        {
            // Arrange
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = -6, Quality = 20 };
            var sut = new UpdateItemStrategy();

            // Act
            sut.Update(item);

            // Assert
            item.Quality.Should().Be(18);
        }
    }
}

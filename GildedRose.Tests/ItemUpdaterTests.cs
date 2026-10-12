using FluentAssertions;
using GildedRose.Core;
using GildedRose.Models;

namespace GildedRose.Tests
{
    public class ItemUpdaterTests
    {
        /*  Business Rules

            Once the sell by date has passed, Quality degrades twice as fast
            The Quality of an item is never negative
            The Quality of an item is never more than 50, however "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.

            "Aged Brie" actually increases in Quality the older it gets
            "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.

            "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches;
            "Backstage passes" Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
        */

        [Fact]
        public void LongAfterSaleDate_DegradingItemsQualityIsZero()
        {
            // Arrange
            var items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                        },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

            var amountOfDays = 50;

            var sut = new ItemUpdater();

            // Act. Run for 50 days
            for(int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.All(i => i.Quality == 0).Should().BeTrue();
        }

        [Fact]
        public void BeforeSellDate_QualityIsPositive()
        {
            // Arrange
            var items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                        },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

            var amountOfDays = 3;

            var sut = new ItemUpdater();

            // Act. Run for 3 days
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.All(i => i.Quality > 0).Should().BeTrue();
        }

        [Fact]
        public void IfNotSulfuras_CanNotExceedQualityMaximum()
        {
            // Arrange
            var maxQuality = 50;
            var amountOfDays = 50;

            var items = new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 5},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 50,
                            Quality = 45,
                        },
                };

            var sut = new ItemUpdater();

            // Act. Run for 50 days
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            foreach(var item in items)
            {
                item.Quality.Should().Be(50);
            }
        }

        [Fact]
        public void IfAgedBrie_IncreasesQualityOverTime()
        {
            // Arrange
            var amountOfDays = 5;

            var items = new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 5}
                };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.First().Quality.Should().Be(10);
        }

        [Fact]
        public void IfBackstagePass_IncreasesQualityOverTimeBy1Before15Days()
        {
            // Arrange
            var amountOfDays = 5;
            var initialQuality = 20;
            var initialSellIn = 50;

            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = initialSellIn,
                    Quality = initialQuality
                }
            };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.First().Quality.Should().Be(25);
        }

        [Fact]
        public void IfBackstagePass_IncreasesQualityOverTimeBy2Between10and5Days()
        {
            // Arrange
            var amountOfDays = 5;
            var initialQuality = 20;
            var initialSellIn = 10;

            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = initialSellIn,
                    Quality = initialQuality
                }
            };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.First().Quality.Should().Be(30);
        }

        [Fact]
        public void IfBackstagePass_IncreasesQualityOverTimeBy3Between5and0Days()
        {
            // Arrange
            var amountOfDays = 5;
            var initialQuality = 20;
            var initialSellIn = 5;

            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = initialSellIn,
                    Quality = initialQuality
                }
            };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.First().Quality.Should().Be(35);
        }

        [Fact]
        public void IfBackstagePass_QualityDropsTo0AfterSellDate()
        {
            // Arrange
            var amountOfDays = 6;
            var initialQuality = 20;
            var initialSellIn = 5;

            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = initialSellIn,
                    Quality = initialQuality
                }
            };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.First().Quality.Should().Be(0);
        }

        [Fact]
        public void WhenALargeAmountOfDaysHasPassed_DegradingItemsQualityIsZero()
        {
            // Arrange
            var amountOfDays = 50;

            var items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                        },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

            var sut = new ItemUpdater();

            // Act. Run for 50 days
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.Should().NotBeEmpty();
            items.All(i => i.Quality == 0).Should().BeTrue();
        }

        // Once the sell by date has passed, Quality degrades twice as fast
        [Fact]
        public void IfDegradingItem_WhenPassedSellDate_QualityDegradesTwiceAsFast()
        {
            // Arrange
            var amountOfDays = 10;

            var items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 6, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 20},
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 20}
                };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.First(i => i.Name == "+5 Dexterity Vest").Quality.Should().Be(6);  // 20 - 6 (BeforeSell) - 4 * 2 (AfterSell) = 6. 
            items.First(i => i.Name == "Elixir of the Mongoose").Quality.Should().Be(5);  // 20 - 5 (BeforeSell) - 5 * 2 (AfterSell) = 5. 
            items.First(i => i.Name == "Conjured Mana Cake").Quality.Should().Be(3);  // 20 - 3 (BeforeSell) - 7 * 2 (AfterSell) = 3. 
        }

        [Fact]
        public void IfSulfuras_ParametersRemainConstant()
        {
            // Arrange
            var amountOfDays = 10;

            var items = new List<Item>
                {
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}
                };

            var sut = new ItemUpdater();

            // Act
            for (int i = 0; i < amountOfDays; i++)
            {
                sut.UpdateQuality(items);
            }

            // Assert
            items.First().Quality.Should().Be(80);
            items.First().SellIn.Should().Be(0);
        }
    }
}
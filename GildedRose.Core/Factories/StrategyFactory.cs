using GildedRose.Core.Strategies;

namespace GildedRose.Core.Factories
{
    public class StrategyFactory
    {
        public static IUpdateItemStrategy CreateStrategy(string itemName)
        {
            return itemName switch
            {
                "Aged Brie" => new UpdateAgedBrieStrategy(),
                "Backstage passes to a TAFKAL80ETC concert" => new UpdateBackstagePassStrategy(),
                "Sulfuras, Hand of Ragnaros" => new UpdateLegendaryItemStrategy(),
                "Conjured Item" => new UpdateConjuredItemStrategy(),
                _ => new UpdateItemStrategy()
            };
        }
    }
}

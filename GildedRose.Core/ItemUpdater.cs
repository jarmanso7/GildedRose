using GildedRose.Core.Factories;
using GildedRose.Models;

namespace GildedRose.Core
{
    public class ItemUpdater
    {
        public void UpdateQuality(IList<Item> items)
        {
            foreach (var item in items)
            {
                var strategy = StrategyFactory.CreateStrategy(item.Name);
                strategy.Update(item);
            }
        }
    }
}

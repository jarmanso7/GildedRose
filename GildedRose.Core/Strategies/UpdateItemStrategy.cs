using GildedRose.Core.Helper;
using GildedRose.Models;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Default implementation for generic items without specific business rules.
    /// The item quality decreases 1 if befor or on sell date, decreases 2 otherwise, to a minumum of 0.
    /// </summary>
    public class UpdateItemStrategy : IUpdateItemStrategy
    {
        public void Update(Item item)
        {
            var decreasingFactor = item.SellIn < 0 ? 2 : 1;

            item.Quality = item.Quality - decreasingFactor;
            item.Quality = item.Quality.SubstractDownToMin(1, 0);

            item.SellIn--;
        }
    }
}

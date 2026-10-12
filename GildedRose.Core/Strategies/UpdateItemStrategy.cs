using GildedRose.Core.Helper;
using GildedRose.Models;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Default implementation for generic items without specific business rules.
    /// The item quality decreases 1 if before sell date, decreases 2 otherwise, to a minumum of 0.
    /// </summary>
    public class UpdateItemStrategy : IUpdateItemStrategy
    {
        public virtual int DecreasingFactor { get; set; } = 1;

        public void Update(Item item)
        {
            var decrement = item.SellIn <= 0 ? 2 * DecreasingFactor : 1 * DecreasingFactor;
            item.Quality = item.Quality.SubstractDownToMin(decrement, 0);

            item.SellIn--;
        }
    }
}

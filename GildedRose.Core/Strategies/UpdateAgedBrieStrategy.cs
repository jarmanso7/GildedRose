using GildedRose.Core.Helper;
using GildedRose.Models;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Increases quality by 1 over time, to a maximum of 50.
    /// </summary>
    public class UpdateAgedBrieStrategy : IUpdateItemStrategy
    {
        public void Update(Item item)
        {
            item.Quality = item.Quality.AddUpToMax(1, 50);

            item.SellIn--;
        }
    }
}

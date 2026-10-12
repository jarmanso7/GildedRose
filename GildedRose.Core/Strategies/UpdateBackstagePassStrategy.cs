using GildedRose.Core.Helper;
using GildedRose.Models;
using System.Runtime.CompilerServices;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Increases quality over time before an on sell date. Collapses quality to 0 after sell date.
    /// 10 days to 6 days before sell date, increases quality by 2. 5 days to the sell date, increases quality by 3.
    /// </summary>
    public class UpdateBackstagePassStrategy : IUpdateItemStrategy
    {
        public void Update(Item item)
        {

            if (item.SellIn > 10)
            {
                item.Quality = item.Quality.AddUpToMax(1,50);

            }

            else if (10 >= item.SellIn && item.SellIn > 5)
            {
                item.Quality = item.Quality.AddUpToMax(2, 50);
            }

            else if (5 >= item.SellIn && item.SellIn >= 0)
            {
                item.Quality = item.Quality.AddUpToMax(3, 50);
            }

            else
            {
                item.Quality = 0;
            }

            item.SellIn--;
        }
    }
}

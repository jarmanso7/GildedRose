using GildedRose.Models;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Legendary items keep their original stats indefinitely
    /// </summary>
    public class UpdateLegendaryItemStrategy : IUpdateItemStrategy
    {
        public void Update(Item item)
        {
            //do nothing.
        }
    }
}

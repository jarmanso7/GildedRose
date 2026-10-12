using GildedRose.Models;

namespace GildedRose.Core.Strategies
{
    /// <summary>
    /// Conjured items decay twice as fast as regular items
    /// </summary>
    public class UpdateConjuredItemStrategy : UpdateItemStrategy
    {
        public override int DecreasingFactor { get; set; } = 2;
    }
}

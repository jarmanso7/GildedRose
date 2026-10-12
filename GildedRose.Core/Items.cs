namespace GildedRose.Models
{
    public class Item
    {
        public string Name { get; set; }

        // The item must be sold within the number of days indicated in this property.
        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}

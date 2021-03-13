using BestOil.Abstracts;

namespace BestOil.Entities
{
    public class FoodItem : Item
    {
        public Food Food { get; set; }
        public int Count { get; set; }
    }
}
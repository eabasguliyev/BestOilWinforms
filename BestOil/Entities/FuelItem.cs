using BestOil.Abstracts;

namespace BestOil.Entities
{
    public class FuelItem:Item
    {
        public Fuel Fuel { get; set; }
        public double Liter { get; set; }
    }
}
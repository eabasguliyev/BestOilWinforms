using System.Collections.Generic;
using BestOil.Abstracts;

namespace BestOil.Entities
{
    public class Bill:Id
    {
        public FuelItem FuelItem { get; set; }
        public List<FoodItem> FoodItems{ get; set; }

        public double TotalCost { get; set; }
        public Bill()
        {
            FoodItems = new List<FoodItem>();
        }
    }
}
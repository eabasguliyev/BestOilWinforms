using System.Collections.Generic;
using System.Linq;
using BestOil.Entities;

namespace BestOil
{
    public class BestOil
    {
        public List<Fuel> Fuels { get; set; }
        public List<Food> Foods { get; set; }
        public Dictionary<string, int> FoodsCount { get; set; }

        public List<Bill> Bills { get; set; }

        public BestOil()
        {
            Fuels = new List<Fuel>();
            Foods = new List<Food>();
            FoodsCount = new Dictionary<string, int>();
            Bills = new List<Bill>();
        }

        public double this[string name]
        {
            get => Foods.Single(f => f.Name == name).Price;
        }
    }
}
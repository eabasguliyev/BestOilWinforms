using System.Collections.Generic;
using BestOil.Abstracts;
using BestOil.Entities;

namespace BestOil.Helpers
{
    public static class BestOilHelper
    {
        public static List<Fuel> GetFuels()
        {
            var fuels = new List<Fuel>()
            {
                new Fuel()
                {
                    Name = "AI92",
                    Price = 0.95,
                },
                new Fuel()
                {
                    Name = "AI95",
                    Price = 1.15,
                },
                new Fuel()
                {
                    Name = "Diesel",
                    Price = 0.6,
                },
            };
            return fuels;
        }

        public static List<Food> GetFoods()
        {
            var foods = new List<Food>()
            {
                new Food()
                {
                    Name = "Hot-Dog",
                    Price = 4,
                },
                new Food()
                {
                    Name = "Hamburger",
                    Price = 5.40,
                },
                new Food()
                {
                    Name = "Fries",
                    Price = 7.20,
                },
                new Food()
                {
                    Name = "Coca-Cola",
                    Price = 4.40,
                },
            };

            return foods;
        }
    }
}
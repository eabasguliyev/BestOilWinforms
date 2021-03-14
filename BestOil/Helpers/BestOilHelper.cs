using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
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

        public static string GetBillText(Bill bill)
        {
            var sb = new StringBuilder();

            sb.Append('*', 30);

            sb.Append("\nFuelling name: Best Oil\n");

            if(bill.FuelItem != null)
            {
                sb.Append($"Fuel name: {bill.FuelItem.Fuel.Name}\n");
                sb.Append($"Price: {bill.FuelItem.Fuel.Price} usd\n");
                sb.Append($"Liter: {bill.FuelItem.Liter} liter\n");
                sb.Append($"Fuel cost: {bill.FuelItem.Cost} usd\n");
            }

            sb.Append("\n");

            if (bill.FoodItems.Count > 0)
            {
                foreach (var foodItem in bill.FoodItems)
                {
                    sb.Append($"Food name: {foodItem.Food.Name}\n");
                    sb.Append($"Price: {foodItem.Food.Price} usd\n");
                    sb.Append($"Amount: {foodItem.Count}\n\n");
                    sb.Append($"Cost: {foodItem.Cost} usd\n\n");
                }

                sb.Append($"Foods cost: {bill.FoodItems.Sum(f => f.Cost)}\n\n");
            }

            sb.Append($"Total cost: {bill.TotalCost}\n");
            sb.Append('*', 30);

            return sb.ToString();
        }
    }
}
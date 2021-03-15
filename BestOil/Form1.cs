using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BestOil.Entities;
using BestOil.Helpers;

namespace BestOil
{
    public partial class Form1 : Form
    {
        private BestOil _bestOil;
        public Form1()
        {
            InitializeComponent();

            _bestOil = new BestOil()
            {
                Fuels = BestOilHelper.GetFuels(),
                Foods = BestOilHelper.GetFoods(),
            };
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            DraggableForm.MouseDown(Cursor.Position, this.Location);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var newLocation = DraggableForm.MouseMove();

            if (newLocation != Point.Empty)
                this.Location = newLocation;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            DraggableForm.MouseUp();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LiterRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            LiterMsBx.Enabled = LiterRdBtn.Checked;

            if(!LiterRdBtn.Checked)
                LiterMsBx.Text = String.Empty;
        }

        private void PriceRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            PriceMsBx.Enabled = PriceRdBtn.Checked;

            LiterMsBx.Visible = !LiterMsBx.Visible;
            LiterTxtBx.Visible = !LiterTxtBx.Visible;

            if (!PriceRdBtn.Checked)
            {
                PriceMsBx.Text = String.Empty;
                LiterTxtBx.Text = String.Empty;
            }
        }

        private void HotDogChBx_CheckedChanged(object sender, EventArgs e)
        {
            HotDogCountMsBx.Enabled = HotDogChBx.Checked;

            if (!HotDogChBx.Checked)
            {
                HotDogCountMsBx.Text = String.Empty;
            }
        }

        private void HamburgerChBx_CheckedChanged(object sender, EventArgs e)
        {
            HamburgerCountMsBx.Enabled = HamburgerChBx.Checked;

            if (!HamburgerChBx.Checked)
                HamburgerCountMsBx.Text = String.Empty;
        }

        private void FriesChBx_CheckedChanged(object sender, EventArgs e)
        {
            FriesCountMsBx.Enabled = FriesChBx.Checked;

            if(!FriesChBx.Checked)
                FriesCountMsBx.Text = String.Empty;
        }

        private void CocaColaChBx_CheckedChanged(object sender, EventArgs e)
        {
            CocaColaCountMsBx.Enabled = CocaColaChBx.Checked;

            if(!CocaColaChBx.Checked)
                CocaColaCountMsBx.Text = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FuelsList.ValueMember = "Name";
           
            FuelsList.DataSource = _bestOil.Fuels;

            HotDogPriceTxtBx.Text = _bestOil["Hot-Dog"].ToString();
            HamburgerPriceTxtBx.Text = _bestOil["Hamburger"].ToString();
            FriesPriceTxtBx.Text = _bestOil["Fries"].ToString();
            CocaColaPriceTxtBx.Text = _bestOil["Coca-Cola"].ToString();
        }

        private void FuelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fuel = FuelsList.SelectedItem as Fuel;

            FuelPriceTxtBx.Text = fuel?.Price.ToString();
        }

        private void LiterMsBx_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LiterMsBx.Text))
            {
                FuelCostTxtBx.Text = "0.00";
            }
            else
            {
                var cost = Convert.ToDouble(LiterMsBx.Text) * Convert.ToDouble(FuelPriceTxtBx.Text);

                FuelCostTxtBx.Text = cost.ToString();
            }
        }

        private void PriceMsBx_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PriceMsBx.Text))
            {
                FuelCostTxtBx.Text = "0.00";
                LiterTxtBx.Text = String.Empty;
            }
            else
            {
                var liter = Convert.ToDouble(PriceMsBx.Text) / Convert.ToDouble(FuelPriceTxtBx.Text);

                LiterTxtBx.Text = liter.ToString("F");
                FuelCostTxtBx.Text = PriceMsBx.Text;
            }
        }

        private void HotDogCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(HotDogCountMsBx, "Hot-Dog");
        }

        private void HamburgerCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(HamburgerCountMsBx, "Hamburger");
        }

        private void FriesCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(FriesCountMsBx, "Fries");
        }

        private void CocaColaCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(CocaColaCountMsBx, "Coca-Cola");
        }

        private void CalculateNewPrice(MaskedTextBox foodCountMsBx, string foodName)
        {
            var totalCost = Convert.ToDouble(CafeCostTxtBx.Text);

            if (String.IsNullOrWhiteSpace(foodCountMsBx.Text))
            {
                totalCost -= _bestOil[foodName] * _bestOil.FoodsCount[foodName];
                CafeCostTxtBx.Text = totalCost.ToString("F2");
                _bestOil.FoodsCount[foodName] = 0;
            }
            else
            {
                var lastCount = 0;

                try
                {
                    lastCount = _bestOil.FoodsCount[foodName];
                }
                catch
                {
                    lastCount = 0;
                }

                var newCount = Convert.ToInt32(foodCountMsBx.Text);

                totalCost -= _bestOil[foodName] * lastCount;


                var foodCost = newCount * _bestOil[foodName];

                CafeCostTxtBx.Text = (totalCost + foodCost).ToString("F2");

                _bestOil.FoodsCount[foodName] = newCount;
            }
        }

        private bool CheckOrder()
        {
            var emptyCost = "0.00";

            return !(FuelCostTxtBx.Text == emptyCost && CafeCostTxtBx.Text == emptyCost);
        }
        private Bill CreateNewBill()
        {
            var newBill = new Bill();
            var emptyCost = "0.00";

            if (FuelCostTxtBx.Text != emptyCost)
            {
                var fuelItem = new FuelItem();

                fuelItem.Fuel = FuelsList.SelectedItem as Fuel;

                fuelItem.Liter = (LiterRdBtn.Checked)
                    ? Convert.ToDouble(LiterMsBx.Text)
                    : Convert.ToDouble(LiterTxtBx.Text);

                fuelItem.Cost = Convert.ToDouble(FuelCostTxtBx.Text);

                newBill.FuelItem = fuelItem;
            }

            if (CafeCostTxtBx.Text != emptyCost)
            {
                var foodItems = new List<FoodItem>();
                
                if (HotDogChBx.Checked)
                {
                    foodItems.Add(CreateNewFoodItem(HotDogCountMsBx, "Hot-Dog"));
                }

                if (HamburgerChBx.Checked)
                {
                    foodItems.Add(CreateNewFoodItem(HamburgerCountMsBx, "Hamburger"));
                }

                if (FriesChBx.Checked)
                {
                    foodItems.Add(CreateNewFoodItem(FriesCountMsBx, "Fries"));
                }

                if (CocaColaChBx.Checked)
                {
                    foodItems.Add(CreateNewFoodItem(CocaColaCountMsBx, "Coca-Cola"));
                }

                newBill.FoodItems = foodItems;
            }

            newBill.TotalCost = newBill.FuelItem.Cost + newBill.FoodItems.Sum(f => f.Cost);
            return newBill;
        }
        private FoodItem CreateNewFoodItem(MaskedTextBox foodCountMsBox, string foodName)
        {
            var foodItem = new FoodItem();

            foodItem.Food = new Food()
            {
                Name = foodName,
                Price = _bestOil[foodName],
            };

            foodItem.Count = Convert.ToInt32(foodCountMsBox.Text);

            foodItem.Cost = foodItem.Food.Price * foodItem.Count;

            return foodItem;
        }

        private void PayBtn_Click(object sender, EventArgs e)
        {
            if (!CheckOrder())
            {
                MessageBox.Show("There is nothing to calculate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newBill = CreateNewBill();
            TotalCostTxtBx.Text = newBill.TotalCost.ToString("F2");
            _bestOil.Bills.Add(newBill);
            SaveToFiles(newBill);

            MessageBox.Show("Calculated. Bill saved to file.", "Info", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ChangeButtonsVisibility();
        }

        private void ChangeButtonsVisibility()
        {
            PayBtn.Visible = !PayBtn.Visible;
            ClearBtn.Visible = !ClearBtn.Visible;
        }

        private void SaveToFiles(Bill bill)
        {
            var directoryName = @"Bills\";

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var fileName = FileHelper.CreateNewFileName(bill.Guid);

                
            //save to json file

            FileHelper.WriteToJsonFile(bill, $"{directoryName}{fileName}.json");

            var desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            //save to pdf file


            FileHelper.WriteToPdf(bill, $@"{desktopFolderPath}\{fileName}.pdf");
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearUserInputs();
            TotalCostTxtBx.Text = "0.00";
            ChangeButtonsVisibility();
        }

        private void ClearUserInputs()
        {
            if (LiterRdBtn.Checked)
                LiterRdBtn.Checked = !LiterRdBtn.Checked;
            else
                PriceRdBtn.Checked = !PriceRdBtn.Checked;

            if (HotDogChBx.Checked)
                HotDogChBx.Checked = !HotDogChBx.Checked;

            if (HamburgerChBx.Checked)
                HamburgerChBx.Checked = !HamburgerChBx.Checked;

            if (FriesChBx.Checked)
                FriesChBx.Checked = !FriesChBx.Checked;

            if (CocaColaChBx.Checked)
                CocaColaChBx.Checked = !CocaColaChBx.Checked;
        }
    }
}

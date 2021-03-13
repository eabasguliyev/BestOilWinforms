using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

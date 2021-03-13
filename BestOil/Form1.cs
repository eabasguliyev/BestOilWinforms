using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestOil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            if(!PriceRdBtn.Checked)
                PriceMsBx.Text = String.Empty;
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
    }
}

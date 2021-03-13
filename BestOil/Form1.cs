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
        }

        private void PriceRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            PriceMsBx.Enabled = PriceRdBtn.Checked;
        }

        private void HotDogChBx_CheckedChanged(object sender, EventArgs e)
        {
            HotDogCountMsBx.Enabled = HotDogChBx.Checked;
        }

        private void HamburgerChBx_CheckedChanged(object sender, EventArgs e)
        {
            HamburgerCountMsBx.Enabled = HamburgerChBx.Checked;
        }

        private void FriesChBx_CheckedChanged(object sender, EventArgs e)
        {
            FriesCountMsBx.Enabled = FriesChBx.Checked;
        }

        private void CocaColaChBx_CheckedChanged(object sender, EventArgs e)
        {
            CocaColaCountMsBx.Enabled = CocaColaChBx.Checked;
        }
    }
}

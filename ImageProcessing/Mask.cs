using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Mask : Form
    {
        public double[,] mask;
        public double factors = 0; 
        public Mask()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            mask = new double[3,3]{
            {Double.Parse(tb0.Text),Double.Parse(tb1.Text),Double.Parse(tb2.Text)},
            {Double.Parse(tb3.Text),Double.Parse(tb4.Text),Double.Parse(tb5.Text)},
            {Double.Parse(tb6.Text),Double.Parse(tb7.Text),Double.Parse(tb8.Text)},
            };
            factors = Double.Parse(tb0.Text) + Double.Parse(tb1.Text) + Double.Parse(tb2.Text) +
                      Double.Parse(tb3.Text) + Double.Parse(tb4.Text) + Double.Parse(tb5.Text) +
                      Double.Parse(tb6.Text) + Double.Parse(tb7.Text) + Double.Parse(tb8.Text);
            this.Close();
        }

    }
}

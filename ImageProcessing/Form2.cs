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
    public partial class Form2 : Form
    {
        Bitmap img;
        public Form2(Bitmap img)
        {
            InitializeComponent();
            this.img = img.Clone() as Bitmap;            
        }

        
        public void drawHistogram()
        {
            try
            {
                pictureBox1.Image = img;
                //       generateHistogram(this.img);
                histoPanel.Refresh();
                HIistogram histogram = new HIistogram(histoPanel, histoPanel.Height, histoPanel.Width, img);
                histogram.DrawHistogram();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        
     }
}

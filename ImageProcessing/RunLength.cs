using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageProcessing
{
    public partial class RunLength : Form
    {
        private Bitmap bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7,img;
        public RunLength(Bitmap bit0, Bitmap bit1, Bitmap bit2, Bitmap bit3, Bitmap bit4, Bitmap bit5, Bitmap bit6, Bitmap bit7)
        {
            InitializeComponent();
            this.bit0 = bit0.Clone() as Bitmap;
            this.bit1 = bit1.Clone() as Bitmap;
            this.bit2 = bit2.Clone() as Bitmap;
            this.bit3 = bit3.Clone() as Bitmap;
            this.bit4 = bit4.Clone() as Bitmap;
            this.bit5 = bit5.Clone() as Bitmap;
            this.bit6 = bit6.Clone() as Bitmap;
            this.bit7 = bit7.Clone() as Bitmap;
        }
        public void runLenth()
        {
            try
            {
                for (int b = 0; b < 8; b++)
                {
                    if (b == 0)
                    {
                        img = bit0.Clone() as Bitmap;
                    }
                    else if (b == 1)
                    {
                        img = bit1.Clone() as Bitmap;
                    }
                    else if (b == 2)
                    {
                        img = bit2.Clone() as Bitmap;
                    }
                    else if (b == 3)
                    {
                        img = bit3.Clone() as Bitmap;
                    }
                    else if (b == 4)
                    {
                        img = bit4.Clone() as Bitmap;
                    }
                    else if (b == 5)
                    {
                        img = bit5.Clone() as Bitmap;
                    }
                    else if (b == 6)
                    {
                        img = bit6.Clone() as Bitmap;
                    }
                    else
                    {
                        img = bit7.Clone() as Bitmap;
                    }
                    Console.Write("picture bit - " + b);
                    rnlnth.AppendText("\nPlane " + b + " - ");
                    int count = 0;
                    for (int i = 0; i < img.Height; i++)
                    {
                        for (int j = 0; j < img.Width; j++)
                        {
                            if (j == 0)
                            {
                                if (img.GetPixel(j, i).R == 255)
                                {
                                    rnlnth.AppendText("1, ");
                                }
                            }
                            else
                            {
                                if (img.GetPixel(j, i).R == img.GetPixel(j - 1, i).R)
                                {
                                    count++;
                                }
                                else
                                {
                                    rnlnth.AppendText(count + ", ");
                                    count = 1;
                                }
                            }
                        }

                        rnlnth.AppendText(count + "\n");
                        count = 0;
                    }
                    rnlnth.AppendText("\n");
                }
            }
            catch (Exception e)
            {
                Console.Write("crashed");
            }
        }

        private void RunLength_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsMethod();
        }

        private void SaveAsMethod()
        {
            saveFileDialog1.Filter = "Text|*.txt";
            saveFileDialog1.OverwritePrompt = true;
            if (rnlnth.Text != null)
            {
                saveFileDialog1.ShowDialog();
                String path = saveFileDialog1.FileName;
                //String format = saveFileDialog1.
                File.WriteAllText(path, rnlnth.Text);
            }
            else
                MessageBox.Show("No image available");
        }
    }
}

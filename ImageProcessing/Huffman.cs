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
    public partial class Huffman : Form
    {
        Bitmap imgGS = null;
        public Huffman(Bitmap imgGS)
        {
            InitializeComponent();
            this.imgGS = imgGS;
        }

        public void huffmanCoding()
        {
            int[] myHuff= new int[256];
            int[] index = new int[256];
            int max,min,swap1,swap2;
            String[] hufcode = new String[256];
            String prew = "0";
            max=min=0;
            for (int i = 0; i < 256; i++)
            {
                myHuff[i] = 0;
                index[i] = i;
                hufcode[i] = null;
            }

            for (int i = 0; i < imgGS.Size.Width; i++)
            {
                for (int j = 0; j < imgGS.Size.Height; j++)
                {
                    System.Drawing.Color c = imgGS.GetPixel(i, j);

                    int Temp = 0;
                    Temp += c.R;
                    myHuff[Temp]++;
                }
            }

            try
            {
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 1; j < 256; j++)
                    {
                        if (myHuff[j] > myHuff[j - 1])
                        {
                            swap1 = myHuff[j];
                            swap2 = index[j];
                            myHuff[j] = myHuff[j - 1];
                            index[j] = index[j - 1];
                            myHuff[j - 1] = swap1;
                            index[j - 1] = swap2;
                        }
                    }
                }
            }
            catch
            {

            }
            
            for (int i = 0; i < 256; i++)
            {
                if (prew.Equals("0"))
                {
                    hufcode[i] += "1";
                    prew = "1";
                }
                else
                {
                    hufcode[i] += "0";
                    prew = "0";
                }
                for (int j = i+1; j < 256; j++)
                {
                    if (prew.Equals("0"))
                    {
                        hufcode[j] += "1";
                        prew = "1";
                    }
                    else
                    {
                        hufcode[j] += "0";
                        prew = "0";
                    }
                }
            }
            for (int i = 0; i < 256; i++)
            {
                Console.Write(" " + index[i] + " ");
            }
            hufMan.AppendText("Huffman Code\n");
            for (int i = 0; i < 256; i++)
            {
                if(myHuff[i]!=0)
                hufMan.AppendText(index[i].ToString()+" - "+ hufcode[i]+"\n");
            }

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsMethod();
        }

        private void SaveAsMethod()
        {
            saveFileDialog1.Filter = "Text|*.txt";
            saveFileDialog1.OverwritePrompt = true;
            if (hufMan.Text != null)
            {
                saveFileDialog1.ShowDialog();
                String path = saveFileDialog1.FileName;
                //String format = saveFileDialog1.
                File.WriteAllText(path, hufMan.Text);
            }
            else
                MessageBox.Show("No text available");
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;


namespace ImageProcessing
{
    public partial class Form1 : Form
    {

        private String path = null;
        private Bitmap img = null;
        private Bitmap img2 = null;
        private Bitmap imgRed = null;
        private Bitmap imgGreen = null;
        private Bitmap imgBlue = null;
        private Bitmap imgAlpha = null;
        private Bitmap imgbw = null;
        private Bitmap imgNeg = null;
        private Bitmap imgedge = null;

        private Bitmap bit0 = null;
        private Bitmap bit1 = null;
        private Bitmap bit2 = null;
        private Bitmap bit3 = null;
        private Bitmap bit4 = null;
        private Bitmap bit5 = null;
        private Bitmap bit6 = null;
        private Bitmap bit7 = null;

        private float contrast = 1.0f;
        private float gamma = 1.0f;
        private float brightness = 1.0f;

        private OpenFileDialog path2;

        private int count = 0;

        public Form1()
        {
            InitializeComponent();

        }

        public void setPath(String path)
        {
            this.path = path;
        }

        public String getPath()
        {
            return path;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox pictureBox1 = new PictureBox();
            PictureBox pictureBox2 = new PictureBox();
            pictureBox1.Location = new System.Drawing.Point(79, 40);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(100, 50);
            pictureBox1.BackColor = Color.Black;

            pictureBox2.Location = new System.Drawing.Point(79, 40);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(100, 50);
            pictureBox2.BackColor = Color.LightBlue;

            panel1.Controls.Add(pictureBox1);
            panel2.Controls.Add(pictureBox2);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        }
    

        private void Open_Click(object sender, EventArgs e)
        {
            OpenMethod();
            redImage();
            greenImage();
            blueImage();
            alphaImage();
            bAndwImage();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {

            SaveAsMethod();
        }



        private void Clone_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = img;
        }


        private void Save_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                pictureBox2.Image.Save(path);
            }
            else
                SaveAsMethod();
        }

        private void SaveAsMethod()
        {
            saveFileDialog1.Filter = "Bitmap|*.bmp|jpeps|*.jpg";
            saveFileDialog1.OverwritePrompt = true;
            if (pictureBox2.Image != null)
            {
                saveFileDialog1.ShowDialog();
                path = saveFileDialog1.FileName;
                //String format = saveFileDialog1.

                pictureBox2.Image.Save(path);
            }
            else
                MessageBox.Show("No image available");
        }
        private void OpenMethod()
        {
            //openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "Bitmap|*.bmp|jpeps|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = img;
                alphaImage();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMethod();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsMethod();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                pictureBox2.Image.Save(path);
            }
            else
                SaveAsMethod();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form2 = new Form1();
            form2.InitializeComponent();
        }

        

        private void blackWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
 //           int k=0;
            try
            {
                
                    bAndwImage();
                    pictureBox2.Image = imgbw.Clone() as Bitmap;

                //}
            }
            catch
            {
                MessageBox.Show("Error");
            }
            }


        private void redOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                redImage();
                pictureBox2.Image = imgRed.Clone() as Bitmap;
            }
            catch 
            {
                MessageBox.Show("No image available");
            }
        }

        private void blueOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                blueImage();
                pictureBox2.Image = imgBlue.Clone() as Bitmap;
                
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void greenOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                greenImage();
                pictureBox2.Image = imgGreen.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                alphaImage();
                redImage();
                blueImage();
                greenImage();
                img2 = imgAlpha.Clone() as Bitmap;
                pictureBox2.Image = imgAlpha.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c_alpha = imgAlpha.GetPixel(j, i);
                        Color c_red = imgRed.GetPixel(j,i);
                        Color c_green = imgGreen.GetPixel(j, i);
                        Color c_blue = imgBlue.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c_alpha.A, c_red.R, c_green.G, c_blue.B));
                    }
                }
                pictureBox2.Image = img2.Clone() as Bitmap;
            }
            catch
            {
                if (imgRed == null)
                    MessageBox.Show("Red Channel is not available");
                else if (imgBlue == null)
                    MessageBox.Show("Blue Channel is not available");
                else if (imgGreen == null)
                    MessageBox.Show("Green Channel is not available");
                else
                    MessageBox.Show("Error");
            }
        }
        private void alphaImage()
        {
            try
            {
                
                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c.A, 0, 0, 0));
                    }
                }
                imgAlpha = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void redImage()
        {
            try
            {

                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c.A, c.R, 0, 0));
                    }
                }
                imgRed = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void greenImage()
        {
            try
            {

                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c.A, 0, c.G, 0));
                    }
                }
                imgGreen = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void blueImage()
        {
            try
            {

                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c.A, 0, 0, c.B));
                    }
                }
                imgBlue = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        
        }

        private void bAndwImage()
        {
            try
            {

                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        //img2.SetPixel(j, i, Color.FromArgb((c.R + c.G + c.B) / 3, (c.R + c.G + c.B) / 3, (c.R + c.G + c.B) / 3));
                        img2.SetPixel(j, i, Color.FromArgb((int)(0.2989 * c.R + 0.5870 * c.G + 0.1140 * c.B), (int)(0.2989 * c.R + 0.5870 * c.G + 0.1140 * c.B), (int)(0.2989 * c.R + 0.5870 * c.G + 0.1140 * c.B)));
                    }
                }
                imgbw = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }

        }

        private void x2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resample(2.0f);
        }
        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resample(4.0f);
        }

        private void x12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resample(.5f);
        }

        private void Resample(float size)
        {
            try
            {
                img2 = img.Clone() as Bitmap;

                int originalWidth = img2.Width;
                int originalHeight = img2.Height;

                int destWidth = (int)(originalWidth*size);
                int destHeight = (int)(originalHeight * size); 

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img2, 0, 0, destWidth, destHeight);
                g.Dispose();

                pictureBox2.Image = b.Clone() as Bitmap;
            }
            catch
            {

            }
        }
        private void gammaContrast(float contrast, float gamma)
        {
            img2 = img.Clone() as Bitmap;
            Bitmap adjustedImage = img2.Clone() as Bitmap;
            

            
            // create matrix that will brighten and contrast the image
            float[][] colorMatrix ={
                new float[] {contrast, 0, 0, 0, 0}, // scale red
                new float[] {0, contrast, 0, 0, 0}, // scale green
                new float[] {0, 0, contrast, 0, 0}, // scale blue
                new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
                new float[] {0, 0, 0, 0, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(colorMatrix), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(adjustedImage);
            g.DrawImage(img2, new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height)
                , 0, 0, img2.Width, img2.Height,
                GraphicsUnit.Pixel, imageAttributes);
            pictureBox2.Image = adjustedImage.Clone() as Bitmap;
        }

        public void SetBrightness(int brightness)
        {
            img2 = img.Clone() as Bitmap;
            Color c;
            for (int i = 0; i < img2.Width; i++)
            {
                for (int j = 0; j < img2.Height; j++)
                {
                    c = img2.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    img2.SetPixel(i, j, System.Drawing.Color.FromArgb(255, cR, (byte)cG, (byte)cB));
                }
            }
            pictureBox2.Image = img2.Clone() as Bitmap;
        }

        private void getNegative()
        {
            try
            {

                img2 = img.Clone() as Bitmap;
                for (int i = 0; i < img2.Height; i++)
                {
                    for (int j = 0; j < img2.Width; j++)
                    {
                        Color c = img2.GetPixel(j, i);
                        img2.SetPixel(j, i, Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B));
                    }
                }
                imgNeg = img2.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                this.gamma = (float)trackBar1.Value+ 1.0f;
                gammaContrast(contrast, gamma);
            }
            catch
            {

            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            try
            {
                this.contrast = (float)trackBar2.Value + 1.0f;
                gammaContrast(contrast, gamma);
            }
            catch
            {

            }
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                getNegative();
                pictureBox2.Image = imgNeg.Clone() as Bitmap;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 form = new Form2(img);
                form.Visible = true;
                form.drawHistogram();
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != null)
            {
                Information infoButton = new Information((Bitmap)img.Clone(), openFileDialog1.FileName);
                infoButton.Show();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll_2(object sender, EventArgs e)
        {
            this.brightness = ((float)trackBar3.Value + 1.0f)*23;
            SetBrightness((int)brightness);
        }


        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img2 = img.Clone() as Bitmap;
            finalphase fp = new finalphase();
            pictureBox2.Image = fp.EdgeDetect(img2,5).Clone() as Bitmap;
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img2 = img.Clone() as Bitmap;
            finalphase fp = new finalphase();
            pictureBox2.Image = fp.EdgeDetect(img2,7).Clone() as Bitmap;
        }

        private void runLengthCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bAndwImage();
                img2 = imgbw.Clone() as Bitmap;
                finalphase fp = new finalphase();
                if (bit0 == null)
                {
                    bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                    bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                    bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                    bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                    bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                    bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                    bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                    bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
                }
                RunLength rl = new RunLength(bit0, bit1, bit2,bit3,bit4,bit5,bit6,bit7);
                rl.Visible = true;
                rl.runLenth();
                
            }
            catch
            {
                MessageBox.Show("No image available");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;                
            }
            pictureBox2.Image = bit7.Clone() as Bitmap;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit6.Clone() as Bitmap;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit5.Clone() as Bitmap;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit4.Clone() as Bitmap;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit3.Clone() as Bitmap;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit2.Clone() as Bitmap;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit1.Clone() as Bitmap;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            bAndwImage();
            finalphase fp = new finalphase();
            if (bit0 == null)
            {
                bit7 = fp.Bitplane(imgbw, 0).Clone() as Bitmap;
                bit6 = fp.Bitplane(imgbw, 1).Clone() as Bitmap;
                bit5 = fp.Bitplane(imgbw, 2).Clone() as Bitmap;
                bit4 = fp.Bitplane(imgbw, 3).Clone() as Bitmap;
                bit3 = fp.Bitplane(imgbw, 4).Clone() as Bitmap;
                bit2 = fp.Bitplane(imgbw, 5).Clone() as Bitmap;
                bit1 = fp.Bitplane(imgbw, 6).Clone() as Bitmap;
                bit0 = fp.Bitplane(imgbw, 7).Clone() as Bitmap;
            }
            pictureBox2.Image = bit0.Clone() as Bitmap;
        }

        private void huffmanCodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bAndwImage();
                img2 = imgbw.Clone() as Bitmap;
                Huffman huff = new Huffman(img2);
                huff.Visible = true;
                huff.huffmanCoding();
            }
            catch 
            {
                MessageBox.Show("Error");
            }
        }

        private void filter3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img2 = img.Clone() as Bitmap;
            finalphase fp = new finalphase();
            Mask mask = new Mask();
            double[,] MasK = new double[3, 3];
            double factors;
            mask.ShowDialog();
            MasK = mask.mask;
            factors = mask.factors;
            if (fp.Conv3x3(img2, MasK,factors))
            {
                this.Invalidate();
                pictureBox2.Image = img2.Clone() as Bitmap;
            }
        }


    }
}

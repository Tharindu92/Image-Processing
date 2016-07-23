using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessing
{
    class HIistogram
    {
        private Bitmap image = null;
        private Panel e;
        private long[] myValues;
        private long myMaxValue;
        private Boolean myIsDrawing;
        private float myXUnit, myYUnit;
        private int height, width, myOffset = 10;

        public HIistogram(Panel e, int height, int width, Bitmap image)
        {
            this.e = e;
            this.height = height;
            this.width = width;
            this.image = image;
        }

        public void DrawHistogram()
        {
            myValues = findValues();
            foreach (long l in myValues)
            {
                Console.Write(l + " ");
            }

            myMaxValue = myValues.Max();
            Console.WriteLine(myMaxValue);
            myIsDrawing = true;

            ComputeXYUnitValues();
            Draw();
            // e.Refresh();
        }

        public long[] findValues()
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < image.Size.Width; i++)
                for (int j = 0; j < image.Size.Height; j++)
                {
                    System.Drawing.Color c = image.GetPixel(i, j);

                    long Temp = 0;
                    Temp += c.R;
                    Temp += c.G;
                    Temp += c.B;

                    Temp = (int)Temp / 3;
                    myHistogram[Temp]++;
                }

            return myHistogram;
        }

        private void ComputeXYUnitValues()
        {
            myYUnit = (float)(this.height - (2 * myOffset)) / myMaxValue;
            myXUnit = (float)(this.width - (2 * myOffset)) / (myValues.Length - 1);
        }

        private void Draw()
        {
            Color myColor = Color.Black;
            Font myFont = new Font("Times New Roman", 8);
            if (myIsDrawing)
            {
                Graphics g = e.CreateGraphics();
                Pen myPen = new Pen(new SolidBrush(myColor), myXUnit);
                //The width of the pen is given by the XUnit for the control.
                for (int i = 0; i < myValues.Length; i++)
                {
                    //We draw each line
                    g.DrawLine(myPen, new PointF(myOffset + (i * myXUnit),
                       this.height - myOffset), new PointF(myOffset + (i * myXUnit),
                       this.height - myOffset - myValues[i] * myYUnit));
                    //We plot the coresponding index for the maximum value.
                    if (myValues[i] == myMaxValue)
                    {
                        SizeF mySize = g.MeasureString(i.ToString(), myFont);
                        g.DrawString(i.ToString(), myFont,
                           new SolidBrush(myColor),
                           new PointF(myOffset + (i * myXUnit) - (mySize.Width / 2),
                           this.height - myFont.Height),
                           System.Drawing.StringFormat.GenericDefault);
                    }
                }
                //We draw the indexes for 0 and for 
                //the length of the array beeing plotted
                g.DrawString("0", myFont, new SolidBrush(myColor),
                   new PointF(myOffset, this.height - myFont.Height),
                   System.Drawing.StringFormat.GenericDefault);
                g.DrawString((myValues.Length - 1).ToString(), myFont,
                   new SolidBrush(myColor),
                   new PointF(myOffset + (myValues.Length * myXUnit)
                     - g.MeasureString((myValues.Length - 1).ToString(), myFont).Width,
                   this.height - myFont.Height),
                   System.Drawing.StringFormat.GenericDefault);
                //We draw a rectangle surrounding the control. 
                g.DrawRectangle(new System.Drawing.Pen(new SolidBrush(Color.Black), 1), 0, 0,
                this.width - myOffset, this.height - myOffset);
            }
        }
    }
}

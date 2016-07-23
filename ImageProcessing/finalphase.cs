using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace ImageProcessing
{
    class finalphase
    {
        public Bitmap EdgeDetect(Bitmap src, int size)
        {
            double[,] MASK;
            if (size == 7)
            {
                MASK = new double[7, 7] {
                                {1,4,6,7,6,4,1},        
                                {4,16,24,28,24,16,4},   
                                {6,24,36,42,36,24,6},   
                                {7,28,42,-792,42,28,7},  
                                {6,24,36,42,36,24,6},
                                {4,16,24,28,24,16,4},
                                {1,4,6,7,6,4,1},
                            };
            }
            else
            {
                MASK = new double[5, 5] {
                                {1,4,6,4,1},    
                                {4,16,24,16,4}, 
                                {6,24,-220,24,6},   
                                {4,16,24,16,4},
                                {1,4,6,4,1},

                            };
            }
            double nTemp = 0.0,color = 0,min=0.0, max=0.0;
            double sum = 0.0, mean, threshold = 0.0, s = 0.0;

            int median, n=0,y,x;
            
            median = size / 2;

            Bitmap btmp = new Bitmap(src.Width + median, src.Height + median);

            BitmapData bitmapData = btmp.LockBits(new Rectangle(0, 0, btmp.Width, btmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                int offset = 3;

                for (int colm = 0; colm < srcData.Height - size; colm++)
                {
                    byte* srcptr = (byte*)srcData.Scan0 + (colm * srcData.Stride);
                    byte* bitmapptr = (byte*)bitmapData.Scan0 + (colm * bitmapData.Stride);

                    for (int row = 0; row < srcData.Width - size; row++)
                    {
                        nTemp = 0.0;

                        min = double.MaxValue;
                        max = double.MinValue;

                        for (x = 0; x < size; x++)
                        {
                            for (y = 0; y < size; y++)
                            {
                                byte* tempPtr = (byte*)srcData.Scan0 + ((colm + y) * srcData.Stride);
                                color = (tempPtr[((row + x) * offset)] + tempPtr[((row + x) * offset) + 1] + tempPtr[((row + x) * offset) + 2]) / 3;

                                nTemp += (double)color * MASK[x, y];

                            }
                        }

                        sum += nTemp;
                        n++;
                    }
                }
                mean = ((double)sum / n);
                threshold = 0.0;

                for (int i = 0; i < srcData.Height - size; i++)
                {
                    byte* ptr = (byte*)srcData.Scan0 + (i * srcData.Stride);
                    byte* tptr = (byte*)bitmapData.Scan0 + (i * bitmapData.Stride);

                    for (int j = 0; j < srcData.Width - size; j++)
                    {
                        nTemp = 0.0;

                        min = double.MaxValue;
                        max = double.MinValue;

                        for (x = 0; x < size; x++)
                        {
                            for (y = 0; y < size; y++)
                            {
                                byte* tempptr = (byte*)srcData.Scan0 + ((i + y) * srcData.Stride);
                                color = (tempptr[((j + x) * offset)] + tempptr[((j + x) * offset) + 1] + tempptr[((j + x) * offset) + 2]) / 3;

                                nTemp += (double)color * MASK[x, y];

                            }
                        }

                        s = (mean - nTemp);
                        threshold += (s * s);
                    }
                }


                threshold = threshold / (n - 1);
                threshold = Math.Sqrt(threshold);
                threshold = threshold/2;

                for (int colm = median; colm < srcData.Height - median; colm++)
                {
                    byte* ptr = (byte*)srcData.Scan0 + (colm * srcData.Stride);
                    byte* btmpptr = (byte*)bitmapData.Scan0 + (colm * bitmapData.Stride);

                    for (int row = median; row < srcData.Width - median; row++)
                    {
                        nTemp = 0.0;

                        min = double.MaxValue;
                        max = double.MinValue;

                        for (x = (median * -1); x < median; x++)
                        {
                            for (y = (median * -1); y < median; y++)
                            {
                                byte* tmpptr = (byte*)srcData.Scan0 + ((colm + y) * srcData.Stride);
                                color = (tmpptr[((row + x) * offset)] + tmpptr[((row + x) * offset) + 1] + tmpptr[((row + x) * offset) + 2]) / 3;

                                nTemp += (double)color * MASK[median + x, median + y];

                            }
                        }

                        if (nTemp > threshold)
                        {
                            btmpptr[row * offset] = btmpptr[row * offset + 1] = btmpptr[row * offset + 2] = 255;
                        }
                        else
                            btmpptr[row * offset] = btmpptr[row * offset + 1] = btmpptr[row * offset + 2] = 0;

                    }
                }
            }

            btmp.UnlockBits(bitmapData);
            src.UnlockBits(srcData);

            return btmp;
        }

        public Bitmap Bitplane(Bitmap img, int plane)
        {
            int intensity;
            char[] bit = new char[8];
            Bitmap img2 = img.Clone() as Bitmap;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    intensity = img.GetPixel(j, i).R;
                    String binary = Convert.ToString(intensity, 2).PadLeft(8, '0');
                    bit = binary.ToCharArray();
                    if (bit[plane].Equals('1'))
                    {
                        img2.SetPixel(j,i,Color.FromArgb(255,255,255,255));
                    }
                    else
                    {
                        img2.SetPixel(j, i, Color.FromArgb(255,0,0,0));
                    }
                }
            }
            
            return img2;
        }

        public bool Conv3x3(Bitmap img, double[,] mask,double factors)
        {
            int offset = 0;
            if (factors == 0)
            {
                factors = 1;
            }
            Bitmap img2 = img.Clone() as Bitmap;
            BitmapData databtmp = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData srcbtmp = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);
            int stride = databtmp.Stride;
            int stride2 = stride * 2;

            System.IntPtr dataScan = databtmp.Scan0;
            System.IntPtr SrcScan = srcbtmp.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)dataScan;
                byte* ptrSrc = (byte*)(void*)SrcScan;
                int nOffset = stride - img.Width * 3;
                int nWidth = img.Width - 2;
                int nHeight = img.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = (int)((((ptrSrc[2] * mask[0, 0]) +
                            (ptrSrc[5] * mask[0, 1]) +
                            (ptrSrc[8] * mask[0, 2]) +
                            (ptrSrc[2 + stride] * mask[1, 0]) +
                            (ptrSrc[5 + stride] * mask[1, 1]) +
                            (ptrSrc[8 + stride] * mask[1, 2]) +
                            (ptrSrc[2 + stride2] * mask[2, 0]) +
                            (ptrSrc[5 + stride2] * mask[2, 1]) +
                            (ptrSrc[8 + stride2] * mask[2, 2])) / factors) + offset);

                        if (nPixel < 0)
                        {
                            nPixel = 0;
                        }
                        if (nPixel > 255)
                        {
                            nPixel = 255;
                        }
                        ptr[5 + stride] = (byte)nPixel;

                        nPixel = (int)((((ptrSrc[1] * mask[0, 0]) +
                            (ptrSrc[4] * mask[0, 1]) +
                            (ptrSrc[7] * mask[0, 2]) +
                            (ptrSrc[1 + stride] * mask[1, 0]) +
                            (ptrSrc[4 + stride] * mask[1, 1]) +
                            (ptrSrc[7 + stride] * mask[1, 2]) +
                            (ptrSrc[1 + stride2] * mask[2, 0]) +
                            (ptrSrc[4 + stride2] * mask[2, 0]) +
                            (ptrSrc[7 + stride2] * mask[2, 2])) / factors) + offset);

                        if (nPixel < 0) 
                        { 
                            nPixel = 0; 
                        }
                        if (nPixel > 255)
                        {
                            nPixel = 255;
                        }
                        ptr[4 + stride] = (byte)nPixel;

                        nPixel = (int)((((ptrSrc[0] * mask[0, 0]) + (ptrSrc[3] * mask[0, 1]) + (ptrSrc[6] * mask[0, 2]) +
                                       (ptrSrc[0 + stride] * mask[1, 0]) +
                                       (ptrSrc[3 + stride] * mask[1, 1]) +
                                       (ptrSrc[6 + stride] * mask[1, 2]) +
                                       (ptrSrc[0 + stride2] * mask[2, 0]) +
                                       (ptrSrc[3 + stride2] * mask[2, 1]) +
                                       (ptrSrc[6 + stride2] * mask[2, 2])) / factors) + offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        ptr[3 + stride] = (byte)nPixel;

                        ptr += 3;
                        ptrSrc += 3;
                    }

                    ptr += nOffset;
                    ptrSrc += nOffset;
                }
            }

            img.UnlockBits(databtmp);
            img2.UnlockBits(srcbtmp);
            return true;
        }
    }
}
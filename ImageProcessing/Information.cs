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
    public partial class Information : Form
    {
        String BitmapPath;
        Bitmap CurrentBitmap;
        public Information(Bitmap image, String path)
        {
            this.BitmapPath = path;
            this.CurrentBitmap = image;
            InitializeComponent();
        }
        private void ImageInfo_Load(object sender, EventArgs e)
        {
            if (this.BitmapPath != null)
            {
                FileInfo fileInfo = new FileInfo(BitmapPath);
                lblImageName.Text = fileInfo.Name.Replace(fileInfo.Extension, "");
                lblImageExtension.Text = fileInfo.Extension;
                string loc = fileInfo.DirectoryName;
                if (loc.Length > 50)
                    loc = loc.Substring(0, 15) + "..." + loc.Substring(loc.LastIndexOf("\\"));
                lblImageLocation.Text = loc;
                lblImageDimension.Text = CurrentBitmap.Width + " x " + CurrentBitmap.Height;
                lblImageSize.Text = (fileInfo.Length / 1024.0).ToString("0.0") + " KB";
                lblImageCreatedOn.Text = fileInfo.CreationTime.ToString("dddd MMMM dd, yyyy");
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

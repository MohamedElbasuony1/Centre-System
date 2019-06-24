using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Center
{
    public partial class Report : Form
    {
        Bitmap memoryImage;
        Bitmap h;
        public Report(DataSet ds, string id, string name, string class1, string group)
        {
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables["x"];
            lblname1.Text = name;
            lblclass1.Text = class1;
            lblid1.Text = id;
            lblgroup1.Text = group;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(h, 30, 0);
        }

        private void Report_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printPreviewDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            try
            {
                printDocument1.Print();
            }
            catch (Win32Exception)
            {
                MessageBox.Show("لا يوجد طابعه");
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            pageSetupDialog1.ShowDialog();
        }

        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(380, 40, 0,20, s);
            Image b = memoryImage;
            h = ResizeImage(b, 850, 1170);
        }

        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private void Report_Move(object sender, EventArgs e)
        {
            this.Location = new Point(367, 10);
        }
    }
}

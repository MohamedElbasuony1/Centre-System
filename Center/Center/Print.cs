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
    public partial class Print : Form
    {
        Bitmap memoryImage;
        Bitmap h;
        List<student_info> list;

        public Print(List<student_info> list)
        {
            InitializeComponent();
            this.list = new List<student_info>();
            this.list = list;
            lbltit1.Parent = lblname1.Parent = lblclass1.Parent = lblgroup1.Parent = lblid1.Parent = pic1.Parent = lbl1.Parent = lbll1.Parent = pictureBox1;
            lbltit2.Parent = lblname2.Parent = lblclass2.Parent = lblgroup2.Parent = lblid2.Parent = pic2.Parent = lbl2.Parent = lbll2.Parent = pictureBox14;
            lbltit3.Parent = lblname3.Parent = lblclass3.Parent = lblgroup3.Parent = lblid3.Parent = pic3.Parent = lbl3.Parent = lbll3.Parent = pictureBox4;
            lbltit4.Parent = lblname4.Parent = lblclass4.Parent = lblgroup4.Parent = lblid4.Parent = pic4.Parent = lbl4.Parent = lbll4.Parent = pictureBox18;
            lbltit5.Parent = lblname5.Parent = lblclass5.Parent = lblgroup5.Parent = lblid5.Parent = pic5.Parent = lbl5.Parent = lbll5.Parent = pictureBox8;
            lbltit6.Parent = lblname6.Parent = lblclass6.Parent = lblgroup6.Parent = lblid6.Parent = pic6.Parent = lbl6.Parent = lbll6.Parent = pictureBox20;
            lbltit7.Parent = lblname7.Parent = lblclass7.Parent = lblgroup7.Parent = lblid7.Parent = pic7.Parent = lbl7.Parent = lbll7.Parent = pictureBox6;
            lbltit8.Parent = lblname8.Parent = lblclass8.Parent = lblgroup8.Parent = lblid8.Parent = pic8.Parent = lbl8.Parent = lbll8.Parent = pictureBox16;
            lbltit9.Parent = lblname9.Parent = lblclass9.Parent = lblgroup9.Parent = lblid9.Parent = pic9.Parent = lbl9.Parent = lbll9.Parent = pictureBox10;
            lbltit10.Parent = lblname10.Parent = lblclass10.Parent = lblgroup10.Parent = lblid10.Parent = pic10.Parent = lbl10.Parent = lbll10.Parent = pictureBox12;
            panel10.Visible = panel11.Visible = panel2.Visible = panel3.Visible = panel4.Visible = panel5.Visible = panel6.Visible = panel7.Visible = panel8.Visible = panel9.Visible = false;
        }

        private void Print_Load(object sender, EventArgs e)
        {
            //skinEngine1.SkinFile = "vista1.ssk";
            for (int i = 0; i < this.list.Count; i++)
            {
                if (i == 0)
                {
                    panel6.Visible = true;
                    lblname1.Text = list[i].name;
                    lblclass1.Text = list[i].Class;
                    lblgroup1.Text = list[i].group;
                    lblid1.Text = list[i].id;
                    pic1.Image = list[i].b;
                }
                else if (i == 1)
                {
                    panel8.Visible = true;
                    lblname2.Text = list[i].name;
                    lblclass2.Text = list[i].Class;
                    lblgroup2.Text = list[i].group;
                    lblid2.Text = list[i].id;
                    pic2.Image = list[i].b;
                }
                else if (i == 2)
                {
                    panel2.Visible = true;
                    lblname3.Text = list[i].name;
                    lblclass3.Text = list[i].Class;
                    lblgroup3.Text = list[i].group;
                    lblid3.Text = list[i].id;
                    pic3.Image = list[i].b;
                }
                else if (i == 3)
                {
                    panel10.Visible = true;
                    lblname4.Text = list[i].name;
                    lblclass4.Text = list[i].Class;
                    lblgroup4.Text = list[i].group;
                    lblid4.Text = list[i].id;
                    pic4.Image = list[i].b;
                }
                else if (i == 4)
                {
                    panel4.Visible = true;
                    lblname5.Text = list[i].name;
                    lblclass5.Text = list[i].Class;
                    lblgroup5.Text = list[i].group;
                    lblid5.Text = list[i].id;
                    pic5.Image = list[i].b;
                }
                else if (i == 5)
                {
                    panel11.Visible = true;
                    lblname6.Text = list[i].name;
                    lblclass6.Text = list[i].Class;
                    lblgroup6.Text = list[i].group;
                    lblid6.Text = list[i].id;
                    pic6.Image = list[i].b;
                }
                else if (i == 6)
                {
                    panel3.Visible = true;
                    lblname7.Text = list[i].name;
                    lblclass7.Text = list[i].Class;
                    lblgroup7.Text = list[i].group;
                    lblid7.Text = list[i].id;
                    pic7.Image = list[i].b;
                }
                else if (i == 7)
                {
                    panel9.Visible = true;
                    lblname8.Text = list[i].name;
                    lblclass8.Text = list[i].Class;
                    lblgroup8.Text = list[i].group;
                    lblid8.Text = list[i].id;
                    pic8.Image = list[i].b;
                }
                else if (i == 8)
                {
                    panel5.Visible = true;
                    lblname9.Text = list[i].name;
                    lblclass9.Text = list[i].Class;
                    lblgroup9.Text = list[i].group;
                    lblid9.Text = list[i].id;
                    pic9.Image = list[i].b;
                }
                else if (i == 9)
                {
                    panel7.Visible = true;
                    lblname10.Text = list[i].name;
                    lblclass10.Text = list[i].Class;
                    lblgroup10.Text = list[i].group;
                    lblid10.Text = list[i].id;
                    pic10.Image = list[i].b;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printPreviewDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            pageSetupDialog1.ShowDialog();
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
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(380, 40,-20,20, s);

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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(h, 30, 0);
        }

        private void Print_Move(object sender, EventArgs e)
        {
            this.Location = new Point(367, 10);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("هل انت متاكد من حذف جميع البيانات","Warning",MessageBoxButtons.YesNo)== System.Windows.Forms.DialogResult.Yes)
            {
                DataManager.ExecuteNonQuery("truncate_all");
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "WaveColor2.ssk";
        }
    }
}

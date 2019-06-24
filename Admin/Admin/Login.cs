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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Ahmed_Hassan" && textBox2.Text == "ahas22?9")
            {
                new Admin().ShowDialog();
                textBox1.Text = textBox2.Text = "";
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Information","Warning", MessageBoxButtons.OK);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "WaveColor2.ssk";
        }
    }
}

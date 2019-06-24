using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Center
{
    public partial class group : Form
    {
        SqlConnection con;
        public group()
        {
            InitializeComponent();
        }

        private void drb1_class_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt1_group.Text != "" || drb1_class.SelectedText != "")
            {
                if (!DataManger.GetDataReader("group_selectbyid", out con,
                         new SqlParameter("@group_id", txt1_group.Text)).HasRows)
                {
                    DataManger.ExecuteNonQuery("group_insert",
                                 new SqlParameter("@group_id", txt1_group.Text),
                                 new SqlParameter("@class", drb1_class.Text));
                    clear();
                }
                else
                {
                    MessageBox.Show("كودالمجموعة محجوز مسبقا", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("من فضلك املا الفراغات", "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
        private void clear()
        {
            txt1_group.Text = drb1_class.Text = "";
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt1_group.Text != "")
            {
                if (DataManger.ExecuteNonQuery("group_deletebyid", new SqlParameter("@group_id", txt1_group.Text)) == 0)
                {
                    MessageBox.Show("كودالمجموعة غير موجود", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataManger.ExecuteNonQuery("stud_deletebyid", new SqlParameter("@group_id", txt1_group.Text));
                    clear();
                    MessageBox.Show("تم حذف المجموعة بنجاح", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            else
            {
                MessageBox.Show("من فضلك املا الفراغات", "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

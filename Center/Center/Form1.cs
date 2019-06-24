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
using BarcodeLib.Barcode;

namespace Center
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        int c;
        string s;
        string barc;
        bool flag;
        bool flag2;
        List<string> id2;
        List<string> id;
        int count = 0;
        Bitmap b;
        string stud_id;
        string stud_bar;
        string stud_class;
        string stud_group;
        string stud_name;
        List<student_info> list;
        public Form1()
        {
            InitializeComponent();
        }

        private void drb3_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DataSet ds = DataManger.GetDataSet("group_selectbyclass", "x", new SqlParameter("@class", drb3_class.Text)))
            {
                cmb_group2.DataSource = ds.Tables["x"];
                cmb_group2.DisplayMember = "group_id";
            }
            cmb_group2.Text = "";
        }

        private void txtbar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    dr = DataManger.GetDataReader("stud_selectbygroupidstudbar", out con, new SqlParameter("@barcode", txtbar.Text));
                    if (!dr.HasRows)
                    {
                        lblresult.BackColor = Color.HotPink;
                        lblresult.ForeColor = Color.Red;
                        lblresult.Text = "تم حذف هذا الطالب؟؟";
                        con.Close();
                        return;
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (dr[5].ToString() == drb3_class.Text)
                            {
                                if (id.Remove(dr[0].ToString()))
                                {
                                    lblid.Text = dr[0].ToString();
                                    lblname.Text = dr[1].ToString();
                                    lblclass.Text = dr[5].ToString();
                                    lblgroup.Text = dr[2].ToString();
                                    id2.Add(dr[0].ToString());
                                    lblresult.BackColor = Color.LightGreen;
                                    lblresult.ForeColor = Color.Black;
                                    lblresult.Text = "تم حضور الطالب.......";
                                    lblpres.Text = id2.Count.ToString();
                                    cmb.Items.Add(dr[1].ToString());
                                    lblabse.Text = id.Count.ToString();
                                    con.Close();
                                    return;
                                }
                                else
                                {
                                    if (!id2.Remove(dr[0].ToString()))
                                    {
                                        if (MessageBox.Show("!!هل انت متاكد من اخذ حضور هذا الطالب", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                        {


                                            lblid.Text = dr[0].ToString();
                                            lblname.Text = dr[1].ToString();
                                            lblclass.Text = dr[5].ToString();
                                            lblgroup.Text = dr[2].ToString();
                                            id2.Add(dr[0].ToString());
                                            cmb.Items.Add(dr[1].ToString());
                                            lblpres.Text = id2.Count.ToString();
                                            if (DataManger.ExecuteNonQuery("absence_deletebyid", new SqlParameter("@stud_id", int.Parse(dr[0].ToString()))) == 0)
                                            {
                                                MessageBox.Show("يجب اخذ رقم الطالب لتثبيت حضوره في مجموعته");
                                                lblresult.BackColor = Color.LightGray;
                                                lblresult.ForeColor = Color.Black;
                                                lblresult.Text = "..........................";
                                                con.Close();
                                                return;
                                            }
                                            lblresult.BackColor = Color.MediumVioletRed;
                                            lblresult.ForeColor = Color.Black;
                                            lblresult.Text = "تم حضور الطالب.......";
                                            con.Close();
                                            return;
                                        }
                                        else
                                        {
                                            con.Close();
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        id2.Add(dr[0].ToString());
                                        lblid.Text = dr[0].ToString();
                                        lblname.Text = dr[1].ToString();
                                        lblclass.Text = dr[5].ToString();
                                        lblgroup.Text = dr[2].ToString();
                                        lblresult.BackColor = Color.LightBlue;
                                        lblresult.ForeColor = Color.Black;
                                        lblresult.Text = ".....تم اخذ حضور الطالب من قبل";
                                        con.Close();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(" الطالب في الصف : " + dr[5].ToString(), "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                con.Close();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!!هل انت متاكد من بيانات المجموعه", @"confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (drb3_class.Text != "" && cmb_group2.Text != "")
                {
                    id = new List<string>();
                    id2 = new List<string>();
                    using (DataSet ds = DataManger.GetDataSet("stud_selectbygroupid", "x", new SqlParameter("@group_id", cmb_group2.Text)))
                    {
                        foreach (DataRow item in ds.Tables["x"].Rows)
                        {
                            id.Add(item[0].ToString());
                        }
                    }
                    drb3_class.Enabled = false;
                    cmb_group2.Enabled = false;
                    txtbar.Enabled = true;
                    txtbar.Focus();
                    btnfinish.Enabled = true;
                    btnstart.Enabled = false;
                    c = id.Count;
                    lblabse.Text = id.Count.ToString();
                }
                else
                {
                    MessageBox.Show("من فضلك املا الفراغات", "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnfinish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!!هل انت متاكد من انهاء عملية الغياب", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (string item in id)
                {
                    DataManger.ExecuteNonQuery("absence_insert",
                                 new SqlParameter("@stud_id", int.Parse(item)),
                                 new SqlParameter("@Date", Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                 );
                }
                drb3_class.Text = "";
                cmb_group2.Text = "";
                txtbar.Text = "";
                drb3_class.Enabled = true;
                cmb_group2.Enabled = true;
                txtbar.Enabled = false;
                btnfinish.Enabled = false;
                btnstart.Enabled = true;
                lblname.Text = "";
                lblresult.Text = "";
                lblid.Text = "";
                lblgroup.Text = "";
                lblclass.Text = "";
                lblabse.Text = "";
                lblpres.Text = "0";
                cmb.Items.Clear();
                lblresult.BackColor = Color.Transparent;
            }
        }

        private void txt1_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void drb1_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DataSet ds = DataManger.GetDataSet("group_selectbyclass", "x", new SqlParameter("@class", drb1_class.Text)))
            {
                drb2_groub.DataSource = ds.Tables["x"];
                drb2_groub.DisplayMember = "group_id";
            }
            drb2_groub.Text = "";
        }

        private void drb2_groub_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txt2_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           new group().ShowDialog();
            drb1_class.Text = "";
            drb2_groub.Text = "";
            drb2_groub.DataSource = null;
            drb2_groub.DisplayMember = null;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (list.Count <= 10)
            {
                if (txt1_name.Text != "" && txt2_code.Text != "" && drb1_class.Text != "" && drb2_groub.Text != "")
                {
                    dr = DataManger.GetDataReader("stud_selectbyid", out con, new SqlParameter("@stud_id", int.Parse(txt2_code.Text)));
                    if (!dr.HasRows)
                    {
                        DataManger.ExecuteNonQuery("stud_insert",
                          new SqlParameter("@stud_id", int.Parse(txt2_code.Text)),
                          new SqlParameter("@stud_name", txt1_name.Text),
                          new SqlParameter("@group_id", drb2_groub.Text),
                          new SqlParameter("@barcode", s));
                        list.Add(new student_info(txt1_name.Text, drb1_class.Text, drb2_groub.Text, txt2_code.Text, b));
                        btn3_print.Text = string.Format("طباعة" + "({0})", ++count);
                        if (count == 10)
                        {
                            btn3_print.ForeColor = Color.Red;
                        }
                        btn_add.Enabled = false;
                        clear();
                        con.Close();
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("كودالطالب محجوز مسبقا", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك املا الفراغات", "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("تم الوصول للحد الاقصي للطباعة", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn2_barcode_Click(object sender, EventArgs e)
        {
            do
            {
                s = (new Random().Next(111111111, 999999999)).ToString();
                dr = DataManger.GetDataReader("stud_selectbybarcode", out con, new SqlParameter("@barcode", s));
            } while (dr.HasRows);
            con.Close();
            b = creat_barcode(s);
            pictureBox1.Image = b;
            btn_add.Enabled = true;
        }

        private void btn3_print_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                skinEngine1.Active = false;
                new Print(list).ShowDialog();
                list.Clear();
                count = 0;
                btn3_print.Text = "طباعه";
                skinEngine1.Active = true;
            }
            else
            {
                MessageBox.Show("لم يتم اضافة كرنيه للطباعة", "warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void cmb1_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear2();
            flag = false;
            using (DataSet ds = DataManger.GetDataSet("group_selectbyclass", "x", new SqlParameter("@class", cmb1_class.Text)))
            {
                cmb_group3.DataSource = ds.Tables["x"];
                cmb_group3.DisplayMember = "group_id";
            }
            flag = true;
            cmb_group3.Text = "";
        }

        private void cmb_group3_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear2();
            if (flag)
            {
                using (DataSet ds = DataManger.GetDataSet("stud_selectbygroupid", "x", new SqlParameter("@group_id", cmb_group3.Text)))
                {
                    cmb_name.DataSource = ds.Tables["x"];
                    cmb_name.DisplayMember = "stud_name";
                    cmb_name.ValueMember = "stud_id";
                }
            }
            flag2 = true;
            cmb_name.Text = "";
        }

        private void cmb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag2)
            {

                using (DataSet ds = DataManger.GetDataSet("stud_selectbyid", "x", new SqlParameter("@stud_id", (int)cmb_name.SelectedValue)))
                {
                    foreach (DataRow item in ds.Tables["x"].Rows)
                    {
                        txt_studname.Text = item[1].ToString();
                        txt_studid.Text = item[0].ToString();
                        cmb_studclass.Text = cmb1_class.Text;
                        stud_bar = item[3].ToString();
                        stud_id = item[0].ToString();
                        stud_class = cmb1_class.Text;
                        stud_group = item[2].ToString();
                        stud_name = item[1].ToString();
                        using (DataSet dw = DataManger.GetDataSet("group_selectbyclass", "x", new SqlParameter("@class", cmb_studclass.Text)))
                        {
                            cmb_studgroup.DataSource = dw.Tables["x"];
                            cmb_studgroup.DisplayMember = "group_id";
                        }
                        cmb_studgroup.Text = "";
                        cmb_studgroup.Text = item[2].ToString();
                        barc = item[3].ToString();
                    }
                }
            }          
        }

        private void cmb_studclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DataSet ds = DataManger.GetDataSet("group_selectbyclass", "x", new SqlParameter("@class", cmb_studclass.Text)))
            {
                cmb_studgroup.DataSource = ds.Tables["x"];
                cmb_studgroup.DisplayMember = "group_id";
            }
            cmb_studgroup.Text = "";
        }

        private void btn_report_Click(object sender, EventArgs e)
        {

            if (clear3())
            {
                skinEngine1.Active = false;
                DataSet ds = DataManger.GetDataSet("absence_selectbymonth", "x",
                                       new SqlParameter("@mon", int.Parse(DateTime.Now.Month.ToString())),
                                       new SqlParameter("@stud_id", int.Parse(stud_id)));
                new Report(ds, stud_id, stud_name, stud_class, stud_group).ShowDialog();
                skinEngine1.Active = true;
            }
            else
            {
                MessageBox.Show("!!من فضلك حدد الطالب", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_printcard_Click(object sender, EventArgs e)
        {
            if (clear3())
            {
                skinEngine1.Active = false;
                List<student_info> li = new List<student_info>();
                Bitmap g = creat_barcode(barc);
                li.Add(new student_info(txt_studname.Text, cmb_studclass.Text, cmb_studgroup.Text, txt_studid.Text, g));
                skinEngine1.Dispose();
                new Print(li).ShowDialog();
                skinEngine1.Active = true;
                li.Clear();
                label21.BackColor = Color.LightGreen;
                label21.ForeColor = Color.Black;
                label21.Text = "تمت عملية الطباعة بنجاح.......";
                txt_studid.Text = "";
                txt_studname.Text = "";
                cmb_studclass.Text = "";
                clear4();
            }
            else
            {
                MessageBox.Show("!!بيانات الطالب غير كامله", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (!btnfinish.Enabled)
            {
                if (clear3())
                {
                    DataManger.ExecuteNonQuery("stud_updatebystudid",
                         new SqlParameter("@stud_name", txt_studname.Text),
                         new SqlParameter("@group_id", cmb_studgroup.Text),
                         new SqlParameter("@stud_id", int.Parse(txt_studid.Text)),
                         new SqlParameter("@barcode", stud_bar)
                             );

                    if (DataManger.GetDataSet("stud_selectbyid", "x", new SqlParameter("@stud_id", int.Parse(txt_studid.Text))).Tables["x"].Rows.Count > 1)
                    {
                        DataManger.ExecuteNonQuery("stud_updatebystudid",
                        new SqlParameter("@stud_name", stud_name),
                        new SqlParameter("@group_id", stud_group),
                        new SqlParameter("@stud_id", int.Parse(stud_id)),
                        new SqlParameter("@barcode", stud_bar)
                            );
                        MessageBox.Show("!!كود الطالب محجوز", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clear4();
                    label21.BackColor = Color.LightGreen;
                    label21.ForeColor = Color.Black;
                    label21.Text = "تم تعديل الطالب بنجاح.......";
                }
                else
                {
                    MessageBox.Show("!!بيانات الطالب غير كامله", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("!!يجب اتمام عملية الغياب قبل هذه العملية", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (!btnfinish.Enabled)
            {
                if (MessageBox.Show("متاكدانك تريد حذف هذا الطالب", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (clear3())
                    {
                        DataManger.ExecuteNonQuery("stud_deletestudid",
                                new SqlParameter("@stud_id", int.Parse(stud_id)));
                        label21.BackColor = Color.LightGreen;
                        label21.ForeColor = Color.Black;
                        label21.Text = "تم حذف الطالب بنجاح.......";
                        clear4();
                    }
                    else
                    {
                        MessageBox.Show("!!من فضلك حدد الطالب", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("!!يجب اتمام عملية الغياب قبل هذه العملية", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = new List<student_info>();
            skinEngine1.SkinFile = "WaveColor2.ssk";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (count > 0)
            {
                if (MessageBox.Show("متاكدانك قمت بالطباعة", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            if (btnfinish.Enabled == true)
            {
                if (MessageBox.Show("!!متاكدانك قمت بضغط علي زر انهاء", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void clear()
        {
            txt1_name.Text = txt2_code.Text = "";
            drb1_class.Text = drb2_groub.Text = "";
            pictureBox1.Image = null;
        }

        private Bitmap creat_barcode(string barcode)
        {
            Linear bar = new Linear();
            bar.Type = BarcodeType.CODE39;
            bar.Data = barcode;
            bar.UOM = UnitOfMeasure.PIXEL;
            bar.BarWidth = 1;
            bar.BarHeight = 80;
            bar.LeftMargin = 5;
            bar.RightMargin = 5;
            bar.TopMargin = 5;
            bar.BottomMargin = 5;
            Bitmap b = bar.drawBarcode();
            return b;
        }
        void clear2()
        {
            txt_studid.Text = "";
            txt_studname.Text = "";
            cmb_studclass.Text = "";
            cmb_studgroup.Text = "";
            flag2 = false;
            cmb_name.DataSource = null;
            cmb_name.DisplayMember = null;
            cmb_studgroup.DataSource = null;
            cmb_studgroup.DisplayMember = null;
        }
        void clear4()
        {
            clear2();
            using (DataSet ds = DataManger.GetDataSet("stud_selectbygroupid", "x", new SqlParameter("@group_id", cmb_group3.Text)))
            {
                cmb_name.DataSource = ds.Tables["x"];
                cmb_name.DisplayMember = "stud_name";
                cmb_name.ValueMember = "stud_id";
            }
            flag2 = true;
            cmb_name.Text = "";
        }
        bool clear3()
        {
            return (cmb_name.Text != "" && txt_studname.Text != "" && txt_studid.Text != "");
        }

    }
}

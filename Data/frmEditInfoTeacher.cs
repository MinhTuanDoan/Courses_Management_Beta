using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Data
{
    public partial class frmEditInfoTeacher : Office2007RibbonForm
    {
        TEACHER t = null;
        public frmEditInfoTeacher()
        {
            InitializeComponent();
        }
        public frmEditInfoTeacher(TEACHER pass)
        {
            InitializeComponent();
            t = pass;
        }

        private void frmEditInfoTeacher_Load(object sender, EventArgs e)
        {
            txtHoTen.Text = t.teacherFullname;
            txtEmail.Text = t.teacherEmail;
            dtBirthday.Value = Convert.ToDateTime(t.teacherBirthday);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //kiểm tra 
            if (string.IsNullOrEmpty(txtHoTen.Text) | string.IsNullOrEmpty(txtEmail.Text) | dtBirthday.Value == null)
            {
                MessageBox.Show("Không được để trống các trường");
            }
            else
            {
                if (TracNghiem.SuaThongTinGiaoVien(t.ID, txtHoTen.Text, txtEmail.Text, dtBirthday.Value))
                {
                    //xuất thông báo
                    MessageBox.Show("Sửa thành công");
                }
                else
                {
                    //xuất thông báo lỗi
                    MessageBox.Show("Sửa thất bại");
                }
            }
        }
    }
}

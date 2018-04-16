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
    public partial class frmEditInfoStudent : Office2007Form
    {
        STUDENT temp = null;
        public frmEditInfoStudent()
        {
            InitializeComponent();
        }
        public frmEditInfoStudent(STUDENT pass)
        {
            InitializeComponent();
            temp = pass;
        }

        private void frmEditInfoStudent_Load(object sender, EventArgs e)
        {
            txtHoTen.Text = temp.studentFullname;
            txtEmail.Text = temp.studentEmail;
            dtBirthday.Value = temp.studentBirthday.Value;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //kiểm tra 
            if(string.IsNullOrEmpty(txtHoTen.Text) | string.IsNullOrEmpty(txtEmail.Text) | dtBirthday.Value == null)
            {
                MessageBox.Show("Không được để trống các trường");
            }
            else
            {
                if(TracNghiem.SuaThongTinHocSinh(temp.id, txtHoTen.Text, txtEmail.Text, dtBirthday.Value))
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

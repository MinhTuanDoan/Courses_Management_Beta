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
    public partial class frmEditPassword : Office2007Form
    {
        TEACHER t = null;
        public frmEditPassword()
        {
            InitializeComponent();
        }
        public frmEditPassword(TEACHER pass)
        {
            InitializeComponent();
            t = pass;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //kiểm tra 2 text box có trùng nhau không
            if (string.IsNullOrEmpty(txtPasswordNew.Text) | string.IsNullOrEmpty(txtPasswordNew2.Text))
            {
                MessageBox.Show("Không được để trống các trường");
            }
            else
            {
                if (string.Compare(txtPasswordNew.Text, txtPasswordNew2.Text) == 0)
                {
                    TracNghiem.DoiMatKhauGiaoVien(t.ID, txtPasswordNew.Text);
                }
                else
                {
                    MessageBox.Show("Mật khẩu chưa khớp");
                }

            }
        }
    }
}

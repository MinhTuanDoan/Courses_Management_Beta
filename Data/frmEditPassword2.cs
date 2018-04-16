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
    public partial class frmEditPassword2 : Office2007Form
    {
        STUDENT temp = null;
        public frmEditPassword2()
        {
            InitializeComponent();
        }
        public frmEditPassword2(STUDENT pass)
        {
            InitializeComponent();
            temp = pass;
        }

        private void frmEditPassword2_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //kiểm tra 2 text box có trùng nhau không
            if(string.IsNullOrEmpty(txtPasswordNew.Text) | string.IsNullOrEmpty(txtPasswordNew2.Text))
            {
                MessageBox.Show("Không được để trống các trường");
            }
            else
            {
                if(string.Compare(txtPasswordNew.Text, txtPasswordNew2.Text) == 0)
                {
                    TracNghiem.DoiMatKhauHocSinh(temp.id, txtPasswordNew.Text);
                }
                else
                {
                    MessageBox.Show("Mật khẩu chưa khớp");
                }
                
            }
        }
    }
}

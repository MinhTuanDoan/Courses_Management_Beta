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
    public partial class frmSignUpGuest : Office2007Form
    {
        public frmSignUpGuest()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtID.Text) | string.IsNullOrEmpty(txtPass.Text)| string.IsNullOrEmpty(txtPass2.Text)| string.IsNullOrEmpty(txtFullname.Text))
            {
                MessageBox.Show("Không được để trống các trường");
            }
            else
            {
                if(string.Compare(txtPass.Text, txtPass2.Text) == 0)
                {
                    GUEST gNew = new GUEST()
                    {
                        guestid = txtID.Text,
                        guestpassword = txtPass.Text,
                        guestName = txtFullname.Text
                    };
                    if (TracNghiem.ThemGuest(gNew))
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu nhập lại chưa đúng");
                }
            }    
        }
    }
}

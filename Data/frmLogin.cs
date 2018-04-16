using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Mail;
using DevComponents.DotNetBar;
using System.Collections;

namespace Data
{
    public partial class frmLogin :Office2007Form
    {
        public frmLogin()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            clockMain.Value = System.DateTime.Now;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Show();
            //load loại tài khoản vào combobox
            IList<TYPE> rs = TracNghiem.LayDanhSachLoaiTaiKhoan();
            cbType.DataSource = rs;
            cbType.DisplayMember = "name";
            cbType.ValueMember = "id";
            txtID.Clear();
            txtPass.Clear();
            timer.Enabled = true;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //gọi form đăng kí tài khoản guest
            frmSignUpGuest fsu = new frmSignUpGuest();
            fsu.StartPosition = FormStartPosition.CenterParent;
            fsu.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //lấy giá trị của combobox
            int gt = Convert.ToInt32(cbType.SelectedValue);
            if (string.IsNullOrEmpty(txtID.Text) | string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Không được để trống tên đăng nhập và mật khẩu.");
            }
            else
            {
                if (gt == 1)
                {
                    //nếu phân hệ đăng nhập là học sinh
                    //kiểm tra thông tin đăng nhập
                    var ds = TracNghiem.LayDanhSachHocSinh();
                    var rs = ds.Where(s => (s.studentID.Trim() == txtID.Text) & (s.studentPassword.Trim() == txtPass.Text)).SingleOrDefault();
                    if (rs != null)
                    {
                        //thông báo đăng nhập thành công

                        //gọi form học sinh
                        frmStudent ftc = new frmStudent(rs);
                        ftc.StartPosition = FormStartPosition.CenterScreen;
                        Hide();
                        ftc.ShowDialog();
                        //ẩn form main
                        OnLoad(e);

                    }
                    else
                    {
                        //xử lí ngoại lệ: thông báo lỗi tương ứng
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
                else if (gt == 2)
                {
                    /*nếu phân hệ đăng nhập là giáo viên*/
                    /*kiểm tra thông tin đăng nhập*/
                    var ds = TracNghiem.LayDanhSachGiaoVien();
                    var rs = ds.Where(s => (s.teacherID.Trim() == txtID.Text) & (s.teacherPassword.Trim() == txtPass.Text)).SingleOrDefault();
                    if (rs != null)
                    {
                        //thông báo đăng nhập thành công

                        //gọi form giáo viên
                        frmTeacher ftc = new frmTeacher(rs);
                        Hide();
                        ftc.ShowDialog();
                        //ẩn form main
                        OnLoad(e);

                    }
                    else
                    {
                        //xử lí ngoại lệ: thông báo lỗi tương ứng
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
                else if (gt == 3)
                {
                    //nếu phân hệ đăng nhập là guest
                    //kiểm tra thông tin đăng nhập
                    var ds = TracNghiem.LayDanhSachGuest();
                    var rs = ds.Where(s => (s.guestid.Trim() == txtID.Text & s.guestpassword.Trim() == txtPass.Text)).SingleOrDefault();

                    if (rs != null)
                    {
                        //thông báo đăng nhập thành công
                        MessageBox.Show("Đăng nhập thành công");
                        //gọi form guest
                        //ẩn form main


                    }
                    else
                    {
                        //xử lí ngoại lệ: thông báo lỗi tương ứng
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
                else if (gt == 4)
                {
                    //nếu phân hệ đăng nhập là admin
                    //kiểm tra thông tin đăng nhập
                    if (txtID.Text == "admin" && txtPass.Text == "admin")
                    {
                        //thông báo đăng nhập thành công
                        //gọi form admin
                        frmAdmin fad = new frmAdmin();
                        fad.StartPosition = FormStartPosition.CenterScreen;
                        Hide();
                        fad.ShowDialog();
                        OnLoad(e);
                        //ẩn form main
                    }
                    else
                    {

                    }
                }
            }
        }

        private void Ftc_Deactivate(object sender, EventArgs e)
        {
            OnLoad(e);
        }
        public SmtpClient client = new SmtpClient();
        public MailMessage msg = new MailMessage();
        public System.Net.NetworkCredential smtpCreds = new System.Net.NetworkCredential("minhtuanus2015@gmail.com", "QQwwEE112233");
        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
           
        }
    }
}

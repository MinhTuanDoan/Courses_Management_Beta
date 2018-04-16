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
    public partial class frmStudent : Office2007Form
    {
        STUDENT temp = null;
        COMPETION cNew = null;
        string mh = null;
        int mde = 0;
        public frmStudent()
        {
            InitializeComponent();
        }
        public frmStudent(STUDENT pass)
        {
            InitializeComponent();
            temp = pass;
            dgvCompetition.SelectionChanged += DgvCompetition_SelectionChanged;
            cbSubDemo.SelectedIndexChanged += CbSubDemo_SelectedIndexChanged;
        }

        private void CbSubDemo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lấy value
            string t = cbSubDemo.SelectedValue.ToString();
            var rs5 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID.Trim() == t.Trim()).Select(s => s).ToList();
            if (rs5 != null)
            {
                dgvShowDemo.DataSource = rs5;
                cbDe.DataSource = rs5;
                cbDe.DisplayMember = "id";
                cbDe.ValueMember = "subtractID";
            }
        }

        private void DgvCompetition_SelectionChanged(object sender, EventArgs e)
        {
            int ri = dgvCompetition.CurrentRow.Index;
            lbMH.Text = dgvCompetition[1, ri].Value.ToString();
            cNew = new COMPETION()
            {
                id = Convert.ToInt32(dgvCompetition[0, ri].Value),
                subtractID = dgvCompetition[1, ri].Value.ToString(),
                testDateBegin = Convert.ToDateTime(dgvCompetition[2, ri].Value),
                testTimeToLive = Convert.ToInt32(dgvCompetition[3, ri].Value),
                gradeid = Convert.ToInt32(dgvCompetition[4, ri].Value),
                idtest = Convert.ToInt32(dgvCompetition[5, ri].Value)
            };
            var rs = TracNghiem.LayDanhSachKetQua();
            var rt = rs.Where(s => s.idCom == Convert.ToInt32(dgvCompetition[0, ri].Value) && s.idStudent == temp.id).Select(s => s).SingleOrDefault();
            if (rt != null)
            {
                lbScore.Text = string.Format("{0}", Math.Round((double)rt.score,2));
            }
            else
            {
                lbScore.Text = "Chưa có";
            }
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            /*Load thông tin học sinh đăng nhập*/
            var rs = TracNghiem.LayDanhSachHocSinh();
            var r = rs.Where(s => s.id == temp.id).Select(s => s).SingleOrDefault();
            lbstudentID.Text = r.id.ToString();
            lbFullnameStudent.Text = r.studentFullname.ToString();
            lbBirthdayStudent.Text = string.Format("{0}-{1}-{2}", r.studentBirthday.Value.Day, r.studentBirthday.Value.Month, r.studentBirthday.Value.Year);
            lbClassStudent.Text = r.studentClass.ToString();
            lbEmailStudent.Text = r.studentEmail.ToString();
            temp = r;

            /*load lịch thi cho học sinh này*/
            var rs1 = TracNghiem.LayDanhSachKyThi().Where(s => Convert.ToInt32(s.gradeid) == Convert.ToInt32(temp.studentClass)).Select(s => s).ToList();
            dgvCompetition.DataSource = rs1;
            //dgvCompetition.Columns["SUBTRACT"].Visible = false;
            //dgvCompetition.Columns["clidCompetition"].Visible = false;

            //load luyện thi
            var rs2 = TracNghiem.LayDanhSachMonHoc();
            cbSubDemo.DataSource = rs2;
            cbSubDemo.DisplayMember = "name";
            cbSubDemo.ValueMember = "id";

            string idmh = cbSubDemo.SelectedValue.ToString();
            var rs3 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID == idmh).ToList();
            cbDe.DataSource = rs3;
            cbDe.DisplayMember = "id";
            cbDe.ValueMember = "id";
            dgvShowDemo.DataSource = rs3;
        }


        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            frmEditInfoStudent fes = new frmEditInfoStudent(temp);
            fes.StartPosition = FormStartPosition.CenterParent;
            fes.ShowDialog();
            OnLoad(e);
        }

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            frmEditPassword2 fep = new frmEditPassword2(temp);
            fep.StartPosition = FormStartPosition.CenterParent;
            fep.ShowDialog();
            OnLoad(e);
        }

        private void btnLogOutStudent_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //kiểm tra xem thời hạn thi với giờ hiện tại đã hết hạn thi chưa
            string s = string.Format("{0}:{1}:{2}", Convert.ToDateTime(cNew.testDateBegin.Value).Hour, Convert.ToDateTime(cNew.testDateBegin.Value).Minute, Convert.ToDateTime(cNew.testDateBegin.Value).Second);
            TimeSpan t = cNew.testDateBegin.Value.AddMinutes(cNew.testTimeToLive.Value).TimeOfDay - System.DateTime.Now.TimeOfDay;
            if(cNew.testDateBegin.Value > System.DateTime.Now)
            {
                MessageBox.Show("Chưa tới giờ thi");
            }
            else if (cNew.testDateBegin.Value < System.DateTime.Now)
            {
                if (t.TotalSeconds < 0)
                {
                    MessageBox.Show("Đã hết hạn thi môn này");
                }
                else
                {
                    if (lbScore.Text != "Chưa có")
                    {
                        MessageBox.Show("Đã thi mất rồi");
                    }
                    else
                    {
                        frmThiThat ftt = new frmThiThat(cNew, temp);
                        ftt.StartPosition = FormStartPosition.CenterScreen;
                        ftt.ShowDialog();
                        OnLoad(e);
                    }
                }
            }
        }

        private void btnBeginDemo_Click(object sender, EventArgs e)
        {
            //lấy thông tin từ bảng luyện thi gửi cho form thi thử
            string mh = cbSubDemo.SelectedValue.ToString();
            int de = Convert.ToInt32(cbDe.SelectedValue.ToString());

            frmThiThu ftt = new frmThiThu(mh, de);
            ftt.ShowDialog();
        }
    }
}

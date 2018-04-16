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
    public partial class frmAddQuiz : Office2007Form
    {
        public frmAddQuiz()
        {
            InitializeComponent();
        }

        private void frmAddQuiz_Load(object sender, EventArgs e)
        {
            //load cb môn học
            var rs = TracNghiem.LayDanhSachMonHoc();
            cbSubtract.DataSource = rs;
            cbSubtract.ValueMember = "id";
            cbSubtract.DisplayMember = "name";
            //load cb đáp án đúng
            List<string> ans = new List<string>() { "a", "b", "c", "d" };
            cbRight.DataSource = ans;
            //load cb độ khó
            var rs1 = TracNghiem.LayDanhSachDoKho();
            cbRateHard.DataSource = rs1;
            cbRateHard.DisplayMember = "levelQuizz";
            cbRateHard.ValueMember = "id";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var rs = TracNghiem.LayDanhSachCauHoi();
            var rs2 = TracNghiem.LayDanhSachMonHoc();
            
            string codeSubstract = rs2.Where(s => s.name == cbSubtract.Text).Select(s => s.id).SingleOrDefault().ToString();
            int r = (from i in rs where i.idSubtract.ToString().Trim() == codeSubstract.Trim() select i).Count();
            //thứ nhất là khởi tạo 1 câu hỏi 
            QUIZ qNew = new QUIZ() {
                id = r + 1,
                idSubtract = codeSubstract,
                quizz = txtQuiz.Text,
                a = txtA.Text,
                b = txtB.Text,
                c = txtC.Text,
                d = txtD.Text,
                answer = char.Parse(cbRight.SelectedItem.ToString()),
                rateHard = int.Parse(cbRateHard.SelectedValue.ToString())
            };
            if (TracNghiem.ThemCauHoi(qNew))
            {
                MessageBox.Show("Thêm câu hỏi thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            //thêm xong để đó để có thêm tiếp thì bấm tiếp
            //đã xong chức năng thêm câu hỏi
        }
    }
}

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
    public partial class frmThiThu : Office2007Form
    {
        string mh = null;
        int de = 0;
        int idxQuizCurrent = 0;
        IList<QUIZ> rs3;
        QUIZ temp;//lưu tạm thời câu hỏi đang dùng
        public frmThiThu()
        {
            InitializeComponent();
        }
        public frmThiThu(string m, int d)
        {
            InitializeComponent();
            mh = m;
            de = d;
            txtCurrentQuiz.TextChanged += TxtCurrentQuiz_TextChanged;
            btnSubmit.Click += BtnSubmit_Click;
            btnSubmit.Click += TxtCurrentQuiz_TextChanged;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            //ghi xuống bảng kết quả tạm thời 
            DEMO_RESULT_TEMP t = new DEMO_RESULT_TEMP();
            t.stt = idxQuizCurrent;
            t.idQuiz = temp.id;
            if (rdA.Checked == true)
            {
                t.answer = 'a';
            }
            if (rdB.Checked == true)
            {
                t.answer = 'b';
            }
            if (rdC.Checked == true)
            {
                t.answer = 'c';
            }
            if (rdD.Checked == true)
            {
                t.answer = 'd';
            }
            if (t.answer == rs3[idxQuizCurrent].answer)
            {
                t.truefalse = true;
            }
            else
            {
                t.truefalse = false;
            }
            TracNghiem.GhiBangTamThiThu(t);
            idxQuizCurrent++;
            if (idxQuizCurrent > 59)
            {
                MessageBox.Show("Hết rồi");
            }
            else
            {
                txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
            }
        }

        private void TxtCurrentQuiz_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCurrentQuiz.Text) == 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
            }
            else if (Convert.ToInt32(txtCurrentQuiz.Text) > 1 && Convert.ToInt32(txtCurrentQuiz.Text) < 60)
            {
                btnPrev.Enabled = btnNext.Enabled = true;
            }
            else if (Convert.ToInt32(txtCurrentQuiz.Text) == 60)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
            }
            lbShowQuiz.Text = rs3[idxQuizCurrent].quizz;
            rdA.Checked = rdB.Checked = rdC.Checked = rdD.Checked = false;
            rdA.Text = "A. " + rs3[idxQuizCurrent].a;
            rdB.Text = "B. " + rs3[idxQuizCurrent].b;
            rdC.Text = "C. " + rs3[idxQuizCurrent].c;
            rdD.Text = "D. " + rs3[idxQuizCurrent].d;
            txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
            temp = rs3[idxQuizCurrent];
        }

        private void frmThiThu_Load(object sender, EventArgs e)
        {
            //load danh sách câu hỏi từ đề thi
            var rs = TracNghiem.LayDeThi().Where(s=>s.testID == de && s.subtractID == mh);
            var rs2 = TracNghiem.LayDanhSachCauHoi().Where(s => s.idSubtract.Trim() == mh.Trim()).ToList();
            rs3 = (from i in rs
                   join j in rs2 on i.quizID equals j.id
                   select j).ToList();
            lbShowQuiz.Text = rs3[idxQuizCurrent].quizz;
            rdA.Text = "A. " + rs3[idxQuizCurrent].a;
            rdB.Text = "B. " + rs3[idxQuizCurrent].b;
            rdC.Text = "C. " + rs3[idxQuizCurrent].c;
            rdD.Text = "D. " + rs3[idxQuizCurrent].d;
            txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
            temp = rs3[idxQuizCurrent];
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            idxQuizCurrent--;
            txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            idxQuizCurrent++;
            txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
        }

        private void btnEndCom_Click(object sender, EventArgs e)
        {
            //tổng hợp các điểm số từ bảng tạm
            int qt = TracNghiem.SoCauTraLoiDungThiThu();
            string tb = string.Format("Điểm là {0}, số câu đúng {1}", qt * 10.0 / 60, qt);
            MessageBox.Show(tb);
            //lưu vào bảng kết quả
            //xóa tất cả các record từ bảng tạm
            TracNghiem.XoaBangTamThiThu();
            Close();
        }

        private void btnTip_Click(object sender, EventArgs e)
        {
            string s = string.Format("Đáp án của câu này là {0}, có đáp án rồi thì tự suy nghĩ vì sao nó như thế nhé", temp.answer);
            MessageBox.Show(s);
        }
    }
}

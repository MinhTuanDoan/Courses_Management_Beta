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
using System.Collections;
namespace Data
{
    public partial class frmThiThat : Office2007Form
    {
        public frmThiThat()
        {
            InitializeComponent();
        }
        COMPETION pemp = null;
        STUDENT temp = null;
        int idxQuizCurrent = 0;
        ArrayList l = new ArrayList();
        IList<QUIZ> rs3;
        QUIZ tempQuiz;
        public frmThiThat(COMPETION p, STUDENT t)
        {
            InitializeComponent();
            pemp = p;
            temp = t;
            timerTest.Tick += TimerTest_Tick;
            txtCurrentQuiz.TextChanged += TxtCurrentQuiz_TextChanged;
        }


        private void TxtCurrentQuiz_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCurrentQuiz.Text) == 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
            }
            else if(Convert.ToInt32(txtCurrentQuiz.Text)>1 && Convert.ToInt32(txtCurrentQuiz.Text) < 60)
            {
                btnPrev.Enabled = btnNext.Enabled = true;
            }
            else if(Convert.ToInt32(txtCurrentQuiz.Text) == 60)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
            }
            lbShowQuiz.Text = rs3[idxQuizCurrent].quizz;
            //rdA.Checked = rdB.Checked = rdC.Checked = rdD.Checked = false;
            rdA.Text = "A. " + rs3[idxQuizCurrent].a;
            rdB.Text = "B. " + rs3[idxQuizCurrent].b;
            rdC.Text = "C. " + rs3[idxQuizCurrent].c;
            rdD.Text = "D. " + rs3[idxQuizCurrent].d;
            txtCurrentQuiz.Text = (idxQuizCurrent+1).ToString();
            tempQuiz = rs3[idxQuizCurrent];
        }

        private void TimerTest_Tick(object sender, EventArgs e)
        {
            clockTest.Value = System.DateTime.Now;
            string s = string.Format("{0}:{1}:{2}", Convert.ToDateTime(pemp.testDateBegin.Value).Hour, Convert.ToDateTime(pemp.testDateBegin.Value).Minute, Convert.ToDateTime(pemp.testDateBegin.Value).Second);
            TimeSpan m = pemp.testDateBegin.Value.AddMinutes(pemp.testTimeToLive.Value).TimeOfDay - System.DateTime.Now.TimeOfDay;
            int hours = (int)m.TotalSeconds / 3600;
            int minutes = ((int)m.TotalSeconds % 3600) / 60;
            int seconds = ((int)m.TotalSeconds % 3600 % 60);
            lbTimeToEnd.Text = string.Format("{0}:{1}",minutes, seconds);
            if(m.TotalSeconds < 0)
            {
                timerTest.Stop();
                gpQuiz.Enabled = false;
                //tự động kết thúc bài thi
                //tổng hợp các điểm số từ bảng tạm
                int qt = TracNghiem.SoCauTraLoiDung(temp.id, pemp.id);
                string tb = string.Format("Điểm là {0}, số câu đúng {1}", qt * 10.0 / 60, qt);
                //lưu vào bảng kết quả
                RESULT t = new RESULT() { idCom = pemp.id, idStudent = temp.id, score = qt * 10.0 / 60};
                TracNghiem.GhiKetQuaLai(t);
                //xóa tất cả các record từ bảng tạm
            }
        }

        private void frmThiThat_Load(object sender, EventArgs e)
        {
            timerTest.Enabled = true;
            //load danh sách câu hỏi từ đề thi
            var rs = TracNghiem.LayDeThi().Where(s=>s.testID == pemp.id).Select(s=> s);
            var rs2 = TracNghiem.LayDanhSachCauHoi().Where(s => s.idSubtract.Trim() == pemp.subtractID.Trim()).ToList();
            rs3 = (from i in rs
                      join j in rs2 on i.quizID equals j.id
                      select j).ToList();

            lbSubtract.Text = pemp.subtractID;
            lbGrade.Text = pemp.gradeid.ToString();
            lbDateBegin.Text = pemp.testDateBegin.Value.ToString();
            lbTTL.Text = pemp.testTimeToLive.ToString();
            lbEnd.Text = pemp.testDateBegin.Value.AddHours(1).ToString();
            lbName.Text = temp.studentFullname;
            lbBirth.Text = temp.studentBirthday.Value.Date.ToString();
            lbShowQuiz.Text = rs3[idxQuizCurrent].quizz;
            rdA.Text = "A. " + rs3[idxQuizCurrent].a;
            rdB.Text = "B. " + rs3[idxQuizCurrent].b;
            rdC.Text = "C. " + rs3[idxQuizCurrent].c;
            rdD.Text = "D. " + rs3[idxQuizCurrent].d;
            txtCurrentQuiz.Text = (idxQuizCurrent+1).ToString();
            tempQuiz = rs3[idxQuizCurrent];

            //var rs4 = (from i in rs
            //       join j in rs2 on i.quizID equals j.id
            //       select new {i.stt, i.quizID }).ToList();
            //cbQuiz.DataSource = rs4;
            //cbQuiz.DisplayMember = "stt";
            //cbQuiz.ValueMember = "quizID";
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            idxQuizCurrent--;
            txtCurrentQuiz.Text = (idxQuizCurrent+1).ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            idxQuizCurrent++;
            txtCurrentQuiz.Text = (idxQuizCurrent+1).ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //ghi xuống bảng kết quả tạm thời 
            RESULT_TEMP t = new RESULT_TEMP();
            t.idCompe = pemp.id;
            t.idStudent = temp.id;
            t.idQuiz = tempQuiz.id;
            t.idSubtract = pemp.subtractID;
            if(rdA.Checked == true)
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
            if(t.answer == rs3[idxQuizCurrent].answer)
            {
                t.truefalse = true;
            }
            else
            {
                t.truefalse = false;
            }

            TracNghiem.GhiBangTam(t);
            idxQuizCurrent++;
            if(idxQuizCurrent > 59)
            {
                MessageBox.Show("Hết rồi");
            }
            else
            {
                txtCurrentQuiz.Text = (idxQuizCurrent + 1).ToString();
            }
        }

        private void btnEndCom_Click(object sender, EventArgs e)
        {
            //tổng hợp các điểm số từ bảng tạm
            int qt = TracNghiem.SoCauTraLoiDung(temp.id, pemp.id);
            string tb = string.Format("Điểm là {0}, số câu đúng {1}", qt * 10.0 / 60, qt);
            MessageBox.Show(tb);
            //lưu vào bảng kết quả
            //xóa tất cả các record từ bảng tạm
            RESULT t = new RESULT() { idCom = pemp.id, idStudent = temp.id,idSubtract = pemp.subtractID, score = qt * 10.0 / 60 };
            TracNghiem.GhiKetQuaLai(t);
            TracNghiem.XoaBangTam(pemp.id, temp.id, pemp.subtractID);
            gpQuiz.Enabled = false;
            Close();
        }
    }
}

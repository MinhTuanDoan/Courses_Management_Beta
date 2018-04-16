using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DevComponents.DotNetBar;

namespace Data
{
    public partial class frmTeacher : Office2007Form
    {
        TEACHER temp = null;
        public frmTeacher()
        {
            InitializeComponent();
        }
        public frmTeacher(TEACHER pass)
        {
            InitializeComponent();
            temp = pass;
            dgvQuiz.SelectionChanged += DgvQuiz_SelectionChanged;
            dgvCompetition.SelectionChanged += DgvCompetition_SelectionChanged;
            cbMH.SelectedValueChanged += CbMH_SelectedValueChanged;
            cbSubtractCom.SelectedValueChanged += CbSubtractCom_SelectedValueChanged;
            cbMHDemo.SelectedValueChanged += CbMHDemo_SelectedValueChanged;
            dgvDemoCompetition.SelectionChanged += DgvDemoCompetition_SelectionChanged;
        }

        private void DgvDemoCompetition_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dgvDemoCompetition.CurrentRow.Index;
            txtDemoTestID.Text = dgvDemoCompetition[0, rowindex].Value.ToString();
            cbMHDemo.SelectedValue = dgvDemoCompetition[1, rowindex].Value;
            dtExp.Value = Convert.ToDateTime(dgvDemoCompetition[2, rowindex].Value);
            cbDemoKhoi.SelectedValue = Convert.ToInt32(dgvDemoCompetition[3, rowindex].Value);
            cbDemoDe.SelectedValue = Convert.ToInt32(dgvDemoCompetition[4, rowindex].Value);
            
        }

        private void CbMHDemo_SelectedValueChanged(object sender, EventArgs e)
        {
            string idmh3 = cbMHDemo.SelectedValue.ToString();
            var rs8 = TracNghiem.LayDanhSachDeThiThu().Where(s => s.subtractID == idmh3).ToList();
            cbDemoDe.DataSource = rs8;
            cbDemoDe.DisplayMember = "demoTestID";
            cbDemoDe.ValueMember = "demoTestID";
        }

        private void CbSubtractCom_SelectedValueChanged(object sender, EventArgs e)
        {
            string idmh2 = cbSubtractCom.SelectedValue.ToString();
            var rs5 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID == idmh2).ToList();
            cbDe.DataSource = rs5;
            cbDe.DisplayMember = "id";
            cbDe.ValueMember = "id";
        }

        private void CbMH_SelectedValueChanged(object sender, EventArgs e)
        {
            string idmh = cbMH.SelectedValue.ToString();
            var rs3 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID == idmh).ToList();
            cbTest.DataSource = rs3;
            cbTest.DisplayMember = "id";
            cbTest.ValueMember = "id";
            dgvTest.DataSource = rs3;
        }

        private void DgvCompetition_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dgvCompetition.CurrentRow.Index;
            txtidtest.Text = dgvCompetition[0, rowindex].Value.ToString();
            cbSubtractCom.SelectedValue = dgvCompetition[1, rowindex].Value;
            dtDateBegin.Value = Convert.ToDateTime(dgvCompetition[2, rowindex].Value);
            inputHour.Value = Convert.ToDateTime(dgvCompetition[2, rowindex].Value).Hour;
            inputMin.Value = Convert.ToDateTime(dgvCompetition[2, rowindex].Value).Minute;
            intTTL.Value = Convert.ToInt32(dgvCompetition[3, rowindex].Value);
            cbDe.SelectedValue = Convert.ToInt32(dgvCompetition[5, rowindex].Value);
            cbgradeid.SelectedValue = Convert.ToInt32(dgvCompetition[4, rowindex].Value);
        }

        private void DgvQuiz_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dgvQuiz.CurrentRow.Index;
            txtQuiz.Text = dgvQuiz[2, rowindex].Value.ToString();
            txtA.Text = dgvQuiz[3, rowindex].Value.ToString();
            txtB.Text = dgvQuiz[4, rowindex].Value.ToString();
            txtC.Text = dgvQuiz[5, rowindex].Value.ToString();
            txtD.Text = dgvQuiz[6, rowindex].Value.ToString();
            var rs = TracNghiem.LayDanhSachMonHoc();
            lbSubtract.Text = rs.Where(s => dgvQuiz[1, rowindex].Value.ToString().Trim() == s.id.Trim()).Select(s => s.name).SingleOrDefault().ToString();
            cbRight.SelectedItem = dgvQuiz[7, rowindex].Value.ToString();
        }

        private void frmTeacher_Load(object sender, EventArgs e)
        {
            
            //load thông tin giáo viên
            lbCodeTeacher.Text = temp.teacherID;
            lbEmailTeacher.Text = temp.teacherEmail;
            lbFacultyTeacher.Text = "?????????";
            lbNameTeacher.Text = temp.teacherFullname;
            lbBirthTeacher.Text = string.Format("{0}-{1}-{2}", temp.teacherBirthday.Value.Day, temp.teacherBirthday.Value.Month, temp.teacherBirthday.Value.Year);
            //load tiêu đề form
            this.TitleText = string.Format("<b>Phân hệ giáo viên - ID: {0}</b>", temp.teacherID);
            /*load tab quản lý câu hỏi*/
            //load danh sách câu hỏi
            var rs = TracNghiem.LayDanhSachCauHoi();
            dgvQuiz.DataSource = rs;
            //dgvQuiz.Columns["LEVELQUIZ"].Visible = false;
            //dgvQuiz.Columns["SUBTRACT"].Visible = false;
            //load combobox
            List<string> ans = new List<string>() { "a", "b", "c", "d"};
            cbRight.DataSource = ans;
            var r = TracNghiem.LayDanhSachDoKho();
            cbLevelQuiz.DataSource = r;
            cbLevelQuiz.DisplayMember = "levelQuizz";
            cbLevelQuiz.ValueMember = "id";

            /*load tab quản lí đề thi*/
            //load danh sách đề thi thật vào gridview
            var rs2 = TracNghiem.LayDanhSachMonHoc();
            cbMH.DataSource = rs2;
            cbMH.DisplayMember = "name";
            cbMH.ValueMember = "id";

            string idmh = cbMH.SelectedValue.ToString();
            var rs3 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID == idmh).ToList();
            cbTest.DataSource = rs3;
            cbTest.DisplayMember = "id";
            cbTest.ValueMember = "id";
            dgvTest.DataSource = rs3;
            /*load tab quan li kỳ thi thật*/
            var rs4 = TracNghiem.LayDanhSachKyThi();
            dgvCompetition.DataSource = rs4;
            cbSubtractCom.DataSource = rs2;
            cbSubtractCom.DisplayMember = "name";
            cbSubtractCom.ValueMember = "id";
            string idmh2 = cbSubtractCom.SelectedValue.ToString();
            var rs5 = TracNghiem.LayDanhSachDeThi().Where(s => s.subtractID == idmh2).ToList();
            cbDe.DataSource = rs5;
            cbDe.DisplayMember = "id";
            cbDe.ValueMember = "id";
            var rs6 = TracNghiem.LayDanhSachKhoi();
            cbgradeid.DataSource = rs6;
            cbgradeid.DisplayMember = "id";
            cbgradeid.ValueMember = "id";
            //load tab kỳ thi thử
            var rs7 = TracNghiem.LayDanhSachKyThiThu();
            dgvDemoCompetition.DataSource = rs7;
            cbMHDemo.DataSource = rs2;
            cbMHDemo.DisplayMember = "name";
            cbMHDemo.ValueMember = "id";
            string idmh3 = cbMHDemo.SelectedValue.ToString();
            var rs8 = TracNghiem.LayDanhSachDeThiThu().Where(s => s.subtractID == idmh3).ToList();
            cbDemoDe.DataSource = rs8;
            cbDemoDe.DisplayMember = "demoTestID";
            cbDemoDe.ValueMember = "demoTestID";
            cbDemoKhoi.DataSource = rs6;
            cbDemoKhoi.DisplayMember = "id";
            cbDemoKhoi.ValueMember = "id";

        }

        private void btnLogOutTeacher_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditTeacher_Click(object sender, EventArgs e)
        {
            frmEditInfoTeacher fedit = new frmEditInfoTeacher(temp);
            fedit.StartPosition = FormStartPosition.CenterParent;
            fedit.ShowDialog();
        }

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            //gọi form đổi mật khẩu
            frmEditPassword fedit = new frmEditPassword(temp);
            fedit.StartPosition = FormStartPosition.CenterParent;
            fedit.ShowDialog();

        }

        private void btnAddQuiz_Click(object sender, EventArgs e)
        {
            //gọi form thêm câu hỏi
            frmAddQuiz faq = new frmAddQuiz();
            faq.StartPosition = FormStartPosition.CenterParent;
            faq.ShowDialog();
            OnLoad(e);
        }

        private void btnImportQuiz_Click(object sender, EventArgs e)
        {
            //gọi form import
            frmImportQuiz fiq = new frmImportQuiz();
            fiq.StartPosition = FormStartPosition.CenterParent;
            fiq.ShowDialog();
            OnLoad(e);
        }

        private void btnEditQuiz_Click(object sender, EventArgs e)
        {
            //sửa thông tin câu hỏi theo STT và Mã môn học của câu hỏi được chọn
            //lấy STT và mã môn học của câu hỏi được chọn
            int rowindex = dgvQuiz.CurrentRow.Index;
            int stt = Convert.ToInt32(dgvQuiz[0, rowindex].Value.ToString());
            string codeSubtract = dgvQuiz[1, rowindex].Value.ToString().Trim();
            //khởi tạo Câu hỏi
            QUIZ qEdit = new QUIZ()
            {
                id = stt,
                idSubtract = codeSubtract,
                quizz = txtQuiz.Text,
                a = txtA.Text,
                b = txtB.Text,
                c = txtC.Text,
                d = txtD.Text,
                answer = Convert.ToChar(cbRight.SelectedItem.ToString()),
                rateHard = Convert.ToInt32(cbLevelQuiz.SelectedValue)
            };

            //gọi hàm sửa
            TracNghiem.SuaCauHoi(qEdit);
            //đã xong chức năng
            OnLoad(e);

        }

        private void btnGenerateTest_Click(object sender, EventArgs e)
        {
            frmCreateTest fct = new frmCreateTest();
            fct.StartPosition = FormStartPosition.CenterParent;
            fct.ShowDialog();
            OnLoad(e);
        }

        private void btnEditTest_Click(object sender, EventArgs e)
        {
            int rowindex = dgvTest.CurrentRow.Index;
            int codeTest = Convert.ToInt32(dgvTest[0, rowindex].Value);

        }

        private void btnDeleteQuiz_Click(object sender, EventArgs e)
        {
            //Xóa thông tin câu hỏi theo STT và Mã môn học của câu hỏi được chọn
            //lấy STT và mã môn học của câu hỏi được chọn
            int rowindex = dgvQuiz.CurrentRow.Index;
            int stt = Convert.ToInt32(dgvQuiz[0, rowindex].Value.ToString());
            string codeSubtract = dgvQuiz[1, rowindex].Value.ToString().Trim();
            //gọi hàm xóa
            TracNghiem.XoaCauHoi(stt, codeSubtract);
            //xong chức năng
            OnLoad(e);
        }

        private void btnTestDetail_Click(object sender, EventArgs e)
        {
            //lấy mã đề thi tra cứu trong chi tiết đề thi xuất ra dialog các câu hỏi của đề thi
            int ri = dgvTest.CurrentRow.Index;
            string mh = dgvTest[1, ri].Value.ToString();
            int t = Convert.ToInt32(dgvTest[0, ri].Value);
            frmTestDetail ftd = new frmTestDetail(t, mh);
            ftd.ShowDialog();
        }

        private void btnCreateCompetition_Click(object sender, EventArgs e)
        {
            //lấy thông tin từ các textbox đề tạo 1 kỳ thi mới
            //kiểm tra text box giờ thi
            //tạo 1 cái date time mới.
            DateTime dt = new DateTime(dtDateBegin.Value.Year, dtDateBegin.Value.Month, dtDateBegin.Value.Day, inputHour.Value, inputMin.Value, 0);
            COMPETION cNew = new COMPETION();
            cNew.id = Convert.ToInt32(txtidtest.Text);
            cNew.subtractID = cbSubtractCom.SelectedValue.ToString();
            cNew.testDateBegin = dt;
            cNew.testTimeToLive = intTTL.Value;
            cNew.gradeid = Convert.ToInt32(cbgradeid.SelectedValue);
            cNew.idtest = Convert.ToInt32(cbDe.SelectedValue);
            //gọi hàm thêm kỳ thi
            TracNghiem.ThemKyThiThat(cNew);
            OnLoad(e);
        }

        private void btnDeleteCompetion_Click(object sender, EventArgs e)
        {
            //lấy mã kỳ thi để xóa
            int rowindex = dgvCompetition.CurrentRow.Index;
            int code = Convert.ToInt32(dgvCompetition[0, rowindex].Value);
            string subtract = dgvCompetition[1, rowindex].Value.ToString();
            TracNghiem.XoaKyThiThat(code, subtract);
            OnLoad(e);
        }

        private void btnEditCompetion_Click(object sender, EventArgs e)
        {
            int rowindex = dgvCompetition.CurrentRow.Index;
            int code = Convert.ToInt32(dgvCompetition[0, rowindex].Value);
            //tạo một cái time mới
            DateTime dt = new DateTime(dtDateBegin.Value.Year, dtDateBegin.Value.Month, dtDateBegin.Value.Day, inputHour.Value, inputMin.Value, 0);
            COMPETION cEdit = new COMPETION()
            {
                id = code,
                subtractID = cbSubtractCom.SelectedValue.ToString(),
                testDateBegin = dt,
                testTimeToLive = intTTL.Value,
                gradeid = Convert.ToInt32(cbgradeid.SelectedValue),
                idtest = Convert.ToInt32(cbDe.SelectedValue)
            };

            TracNghiem.SuaKyThiThat(cEdit);
            OnLoad(e);
        }

        private void btnCreateDemoCompetition_Click(object sender, EventArgs e)
        {
            DEMO_COMPETION cNew = new DEMO_COMPETION();
            cNew.demoCompID = Convert.ToInt32(txtDemoTestID.Text);
            cNew.subtractID = cbMHDemo.SelectedValue.ToString();
            cNew.expDate = dtExp.Value;
            cNew.gradeid = Convert.ToInt32(cbDemoKhoi.SelectedValue);
            cNew.iddemotest = Convert.ToInt32(cbDemoDe.SelectedValue);

            TracNghiem.ThemKyThiThu(cNew);
            OnLoad(e);
        }

        private void btnDeleteDemoCompetition_Click(object sender, EventArgs e)
        {
            //lấy mã kỳ thi để xóa
            int rowindex = dgvDemoCompetition.CurrentRow.Index;
            int code = Convert.ToInt32(dgvDemoCompetition[0, rowindex].Value);
            string subtract = dgvDemoCompetition[1, rowindex].Value.ToString();
            TracNghiem.XoaKyThiThu(code, subtract);
            OnLoad(e);
        }

        private void btnEditDemoCompetition_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDemoCompetition.CurrentRow.Index;
            int code = Convert.ToInt32(dgvDemoCompetition[0, rowindex].Value);
            string subtract = dgvDemoCompetition[1, rowindex].Value.ToString();
            DEMO_COMPETION cEdit = new DEMO_COMPETION()
            {
                demoCompID = code,
                subtractID = cbSubtractCom.SelectedValue.ToString(),
                expDate = dtExp.Value,
                gradeid = Convert.ToInt32(cbDemoKhoi.SelectedValue),
                iddemotest = Convert.ToInt32(cbDemoDe.SelectedValue),
        };

            TracNghiem.SuaKyThiThu(cEdit);
            OnLoad(e);
        }

        private void btnDeleteTest_Click(object sender, EventArgs e)
        {
            TEST ts = new TEST()
            {
                id = Convert.ToInt32(cbTest.SelectedValue),
                subtractID = cbMH.SelectedValue.ToString()
            };


            TracNghiem.XoaBoDeThi(ts);
            TracNghiem.XoaDeThi(ts);
            OnLoad(e);
        }

        private void btnExportQuiz_Click(object sender, EventArgs e)
        {
            //Export excel

            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            try
            {
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Sheet1";
                // storing header part in Excel  
                for (int i = 1; i < dgvQuiz.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgvQuiz.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dgvQuiz.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvQuiz.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvQuiz.Rows[i].Cells[j].Value.ToString();
                    }
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FilterIndex = 1;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                app.Quit();
                workbook = null;
                app = null;
            }
        }

        private void cbMH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

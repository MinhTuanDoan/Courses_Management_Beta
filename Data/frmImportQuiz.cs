using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using System.Data.OleDb;
using ExcelDataReader;
namespace Data
{
    public partial class frmImportQuiz : Office2007Form
    {
        public frmImportQuiz()
        {
            InitializeComponent();
        }

        DataSet result;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofdQuiz.Title = "Import From A File....";//tên dialog
            ofdQuiz.Multiselect = false;//không cho chọn nhiều file
            ofdQuiz.InitialDirectory = @"D:\";//thư mục mặc định khi mở
            ofdQuiz.Filter = "Excel 2007(*.xlsx)|*.xlsx";// Lọc ra những file cần hiển thị
            ofdQuiz.FilterIndex = 1;
            ofdQuiz.RestoreDirectory = true;
            if (ofdQuiz.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofdQuiz.FileName;
                FileStream fs = File.Open(ofdQuiz.FileName, FileMode.Open, FileAccess.Read);

                IExcelDataReader reader;
                reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                reader.Close();

                dgvShow.DataSource = result.Tables[0];
            }
        }

        private void btnAddDatabase_Click(object sender, EventArgs e)
        {
            //thêm dữ liệu vào câu hỏi
            int flag = 0;
            DataTable dt = (DataTable)(dgvShow.DataSource);
            for (int i = 0; i < dt.Rows.Count; i++)
            { 
                var rs = TracNghiem.LayDanhSachCauHoi().Where(s => s.idSubtract.Trim() == dt.Rows[i][1].ToString().Trim());
                var t = rs.OrderByDescending(s => s.id).Take(1).Select(s => s).SingleOrDefault();
                // tạo 1 cái QUIZ
                QUIZ qNew = new QUIZ()
                {
                    id = t.id + 1,
                    idSubtract = dt.Rows[i][1].ToString(),
                    quizz = dt.Rows[i][2].ToString(),
                    a = dt.Rows[i][3].ToString(),
                    b = dt.Rows[i][4].ToString(),
                    c = dt.Rows[i][5].ToString(),
                    d = dt.Rows[i][6].ToString(),
                    answer = Convert.ToChar(dt.Rows[i][7].ToString().ToLower()),
                    rateHard = Convert.ToInt32(dt.Rows[i][8])
                };

                var t1 = rs.Where(s => s.quizz.Trim() == qNew.quizz.Trim()).Take(1).SingleOrDefault();
                if(t1 == null)
                {
                    //Thêm vào bảng câu hỏi
                    TracNghiem.ThemCauHoi(qNew);
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
    }
}

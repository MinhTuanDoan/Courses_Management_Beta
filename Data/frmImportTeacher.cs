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
using ExcelDataReader;
using System.IO;
namespace Data
{
    public partial class frmImportTeacher : Office2007Form
    {
        public frmImportTeacher()
        {
            InitializeComponent();
        }
        DataSet result;

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            ofdQuiz.Title = "Import From A File....";//tên dialog
            ofdQuiz.Multiselect = false;//không cho chọn nhiều file
            ofdQuiz.InitialDirectory = @"D:\";//thư mục mặc định khi mở
            ofdQuiz.Filter = "Excel 2007(*.xlsx)|*.xlsx";// Lọc ra những file cần hiển thị
            ofdQuiz.FilterIndex = 1;
            ofdQuiz.RestoreDirectory = true;
            if (ofdQuiz.ShowDialog() == DialogResult.OK)
            {
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

                dgv.DataSource = result.Tables[0];
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //thêm dữ liệu vào câu hỏi

            DataTable dt = (DataTable)(dgv.DataSource);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rs = TracNghiem.LayDanhSachGiaoVien();
                var t = rs.OrderByDescending(s => s.ID).Take(1).Select(s => s).SingleOrDefault();
                // tạo 1 cái Teacher
                TEACHER qNew = new TEACHER()
                {
                    ID = t.ID + 1,
                    teacherID = dt.Rows[i][1].ToString(),
                    teacherPassword = dt.Rows[i][2].ToString(),
                    teacherFullname = dt.Rows[i][3].ToString(),
                    teacherEmail = dt.Rows[i][4].ToString(),
                    teacherBirthday = Convert.ToDateTime(dt.Rows[i][5]),
                    teacherSchema = Convert.ToInt32(dt.Rows[i][6]),
                };

                var t1 = rs.Where(s =>(s.teacherID == qNew.teacherID)).Take(1).SingleOrDefault();
                if (t1 == null)
                {
                    //Thêm vào bảng câu hỏi
                    TracNghiem.ThemGiaoVien(qNew);
                }
            }
        }
    }
}

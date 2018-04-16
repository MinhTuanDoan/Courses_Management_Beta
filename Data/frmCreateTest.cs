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
    public partial class frmCreateTest : Office2007Form
    {
        public frmCreateTest()
        {
            InitializeComponent();
        }
        List<QUIZ> rancauhoi = new List<QUIZ>();
        private void frmCreateTest_Load(object sender, EventArgs e)
        {
            var rs = TracNghiem.LayDanhSachMonHoc();
            cbMonThi.DataSource = rs;
            cbMonThi.DisplayMember = "name";
            cbMonThi.ValueMember = "id";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //lấy ra đề thi có id lớn nhất của môn học được chọn
            var rs = TracNghiem.LayDanhSachDeThi();
            if (rs.Count == 0)
            {
                TEST ts = new TEST() { id = 1, subtractID = cbMonThi.SelectedValue.ToString() };
                TracNghiem.ThemDeThi(ts);
                Random rd = new Random();
                var rs1 = TracNghiem.LayDanhSachCauHoi().Where(s => s.idSubtract == cbMonThi.SelectedValue.ToString()).ToList();
                while (rancauhoi.Count < 60)
                {
                    int i = rd.Next(0, rs1.Count);
                    if (!rancauhoi.Contains(rs1[i]))
                        rancauhoi.Add(rs1[i]);
                }
                for (int i = 0; i < 60; i++)
                {
                    TEST_DETAIL tNew = new TEST_DETAIL()
                    {
                        testID = ts.id,
                        subtractID = rancauhoi[i].idSubtract,
                        stt = i + 1,
                        quizID = rancauhoi[i].id
                    };
                    TracNghiem.ThemChiTietDeThi(tNew);
                }
            }
            else
            {
                var t = rs.OrderByDescending(s => s.id).Where(s => s.subtractID == cbMonThi.SelectedValue.ToString()).Take(1).Select(s => s).SingleOrDefault();
                if (t != null)
                {
                    TEST ts = new TEST() { id = t.id + 1, subtractID = cbMonThi.SelectedValue.ToString() };
                    TracNghiem.ThemDeThi(ts);
                    Random rd = new Random();
                    var rs1 = TracNghiem.LayDanhSachCauHoi().Where(s => s.idSubtract == cbMonThi.SelectedValue.ToString()).ToList();
                    while (rancauhoi.Count < 60)
                    {
                        int i = rd.Next(0, rs1.Count);
                        if (!rancauhoi.Contains(rs1[i]))
                            rancauhoi.Add(rs1[i]);
                    }
                    for (int i = 0; i < 60; i++)
                    {
                        TEST_DETAIL tNew = new TEST_DETAIL()
                        {
                            testID = ts.id,
                            subtractID = rancauhoi[i].idSubtract,
                            stt = i + 1,
                            quizID = rancauhoi[i].id
                        };
                        TracNghiem.ThemChiTietDeThi(tNew);
                    }
                }
            }
            MessageBox.Show("Tạo đề thành công");
            Close();
        }
    }
}

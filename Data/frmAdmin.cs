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
    public partial class frmAdmin : Office2007Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            dgvShowFullTeacher.SelectionChanged += DgvShowFullTeacher_SelectionChanged;
        }

        private void DgvShowFullTeacher_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            //đọc danh sách giáo viên
            var rt = TracNghiem.LayDanhSachGiaoVien();
            dgvShowFullTeacher.DataSource = rt;
            var rt2 = TracNghiem.LayDanhSachHocSinh();
            dgvShowFullStudent.DataSource = rt2;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmImportTeacher fit = new frmImportTeacher();
            fit.ShowDialog();
            OnLoad(e);
        }

        private void btnImHS_Click(object sender, EventArgs e)
        {
            frmImportStudent fit = new frmImportStudent();
            fit.ShowDialog();
            OnLoad(e);
        }
    }
}

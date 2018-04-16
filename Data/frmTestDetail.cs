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
    public partial class frmTestDetail : Office2007Form
    {
        string m = null;
        int t = 0;
        public frmTestDetail()
        {
            InitializeComponent();
        }
        public frmTestDetail(int tid, string mh)
        {
            InitializeComponent();
            t = tid;
            m = mh;
        }

        private void frmTestDetail_Load(object sender, EventArgs e)
        {
            var rs = TracNghiem.LayDeThi().Where(s => (s.testID == t && s.subtractID == m));
            var rs2 = TracNghiem.LayDanhSachCauHoi();

            var rs3 = (from i in rs
                       join j in rs2 on i.quizID equals j.id
                       select new { j.quizz, j.a, j.b, j.c, j.d }).ToList();
            dgvShowFullTestDetail.DataSource = rs3;
        }
    }
}

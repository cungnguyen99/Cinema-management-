using Quản_lý_rạp_chiếu_phim.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_rạp_chiếu_phim
{
    public partial class fAdmin : Form
    {
        private int kt = 1;
        public fAdmin()
        {
            InitializeComponent();
            loadListRevenueOfFimls();
        }

        void loadListRevenueOfFimls()
        {
            dtgvRevenue.DataSource = fimlsDAO.Instance.getListRevenueOfFimls();
        }

        void loadListRevenueOfCinema(int month)
        {
            dtgvRevenue.DataSource=CinemaDAO.Instance.getListRevenueOfCinema(month);
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            kt++;
            if (kt%2==0)
            {
                btnView.Text = "Doanh thu các rạp";
                loadListRevenueOfCinema(dateTimePicker1.Value.Month);
            }else
            {
                btnView.Text = "Doanh thu phim";
                loadListRevenueOfFimls();
            }
        }
    }
}

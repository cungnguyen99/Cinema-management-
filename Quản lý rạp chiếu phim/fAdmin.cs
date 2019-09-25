using Quản_lý_rạp_chiếu_phim.DAO;
using Quản_lý_rạp_chiếu_phim.DTO;
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
            loadListCinema();
        }

        void loadListRevenueOfFimls()
        {
            dtgvRevenue.DataSource = fimlsDAO.Instance.getListRevenueOfFimls();
        }

        void loadListRevenueOfCinema()
        {
            dtgvRevenue.DataSource=CinemaDAO.Instance.getListRevenueOfCinema();
        }

        void loadListCinema()
        {
            List<Cinema> cinemas = CinemaDAO.Instance.getListCinemas();
            cbLoadCinema.DataSource = cinemas;
            cbLoadCinema.DisplayMember = "TenRap";
        }

        void loadListCinemaByName(string name)
        {
            dtgvRevenue.DataSource = CinemaDAO.Instance.getListCinemaByName(name);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            kt++;
            if (kt%2==0)
            {
                btnView.Text = "Doanh thu các rạp";
                loadListRevenueOfCinema();
            }else
            {
                btnView.Text = "Doanh thu phim";
                loadListRevenueOfFimls();
            }
        }

        private void cbLoadCinema_SelectedValueChanged(object sender, EventArgs e)
        {
            loadListCinemaByName(cbLoadCinema.Text.ToString());
        }
    }
}

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
            Load();
        }

        void Load()
        {
            loadListRevenueOfFimls();
            //Load danh sách rạp trong bảng doanh thu
            loadListCinema(cbLoadCinema,true);
            loadListShowTimes();
            //load danh mã rạp trong bảng lịch chiếu
            loadListCinema(cbIDCinema, false);
            addShowTimesBinding();
            loadRoomsInToCombobox(cbRooms);
        }

        void loadListRevenueOfFimls()
        {
            dtgvRevenue.DataSource = fimlsDAO.Instance.getListRevenueOfFimls();
        }

        void loadListRevenueOfCinema()
        {
            dtgvRevenue.DataSource=CinemaDAO.Instance.getListRevenueOfCinema();
        }

        void loadListCinemaByName(string name)
        {
            dtgvRevenue.DataSource = CinemaDAO.Instance.getListCinemaByName(name);
        }

        void loadListCinema(ComboBox comboBox, bool check)
        {
            List<Cinema> cinemas = CinemaDAO.Instance.getListCinemas();
            comboBox.DataSource = cinemas;
            if (check)
            {
                comboBox.DisplayMember = "TenRap";
            }
            else
            {
                comboBox.DisplayMember = "MaRap";
            }
        }

        void loadListShowTimes()
        {
            dtgvShows.DataSource = ShowtimesDAO.Instance.getListShowTimes();
        }

        void addShowTimesBinding()
        {
            txtID.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaShow"));
            txtTen.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaPhim"));
            txtNgayKC.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "NgayChieu"));
            txtNgayKT.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "SoVeDaBan"));
            txtSum.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "TongTien"));
        }

        void loadRoomsInToCombobox(ComboBox comboBox)
        {
            comboBox.DataSource = CinemaRoomDAO.Instance.getListCinemaRoom();
            comboBox.DisplayMember = "MaPhong";
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
            loadListCinemaByName(comboBox1.Text.ToString());
        }

        private void cbIDCinema_TextChanged(object sender, EventArgs e)
        {
            string id = cbIDCinema.Text;
            CinemaRoom room = CinemaRoomDAO.Instance.getRoomByID(id);
        }
    }
}

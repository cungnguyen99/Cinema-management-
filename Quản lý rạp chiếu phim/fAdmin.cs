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
        BindingSource showList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }

        void Load()
        {
            //Khi nhấn nút xem vẫn binding được 
            dtgvShows.DataSource = showList;
            loadListRevenueOfFimls();
            //Load danh sách rạp trong bảng doanh thu
            loadListCinema(cbLoadCinema,true);
            loadListShowTimes();
            //load danh mã rạp trong bảng lịch chiếu
            loadListCinema(cbIDCinema, false);
            addShowTimesBinding();
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
            //Thay dtgvShows bằng showList
            showList.DataSource = ShowtimesDAO.Instance.getListShowTimes();
        }

        void addShowTimesBinding()
        {
            txtID.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaShow",true, DataSourceUpdateMode.Never));
            txtTen.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaPhim", true, DataSourceUpdateMode.Never));
            cbIDCinema.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaRap", true, DataSourceUpdateMode.Never));
            txtNgayKC.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "NgayChieu", true, DataSourceUpdateMode.Never));
            txtNgayKT.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "SoVeDaBan", true, DataSourceUpdateMode.Never));
            cbRooms.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaPhong", true, DataSourceUpdateMode.Never));
            txtSum.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "TongTien", true, DataSourceUpdateMode.Never));
        }

        void loadroomintocombobox()
        {
            cbRooms.DataSource = CinemaRoomDAO.Instance.getListCinemaRoom();
            cbRooms.DisplayMember = "maphong";
        }

        void loadRoomsInToCombobox(string id)
        {
            List<CinemaRoom> rooms = CinemaRoomDAO.Instance.getRoomsByID(id);
            cbRooms.DataSource = rooms;
            cbRooms.DisplayMember = "MaPhong";
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

        private void cbIDCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id;
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem == null) return;
            Cinema selected = comboBox.SelectedItem as Cinema;
            id = selected.MaRap;
            loadRoomsInToCombobox(id);
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            loadListShowTimes();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maShow = txtID.Text;
            string maPhim = txtTen.Text;
            string maRap = cbIDCinema.Text;
            string maPhong = cbRooms.Text;
            string ngayChieu = txtNgayKC.Text;
            if (ShowtimesDAO.Instance.insertShowtimes(maShow, maPhim, maRap, maPhong, Convert.ToDateTime(ngayChieu)))
            {
                MessageBox.Show("Insert succeeded");
                loadListShowTimes();
            }
            else
            {
                MessageBox.Show("Insert unsuccessful");
            }
        }

        private List<Showtimes> searchShowTimes(string id)
        {
            List<Showtimes> showtimes = ShowtimesDAO.Instance.getListShowTimesByIdFimlsOrIdCinema(txtID.Text);
            return showtimes;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maShow = txtID.Text;
            string maPhim = txtTen.Text;
            string maRap = cbIDCinema.Text;
            string maPhong = cbRooms.Text;
            string ngayChieu = txtNgayKC.Text;
            if (ShowtimesDAO.Instance.updateShowtimes(maShow, maPhim, maRap, maPhong, Convert.ToDateTime(ngayChieu)))
            {
                MessageBox.Show("Update succeeded");
                loadListShowTimes();
            }
            else
            {
                MessageBox.Show("Update unsuccessful");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maShow = txtID.Text;
            if (ShowtimesDAO.Instance.deleteShowtimes(maShow))
            {
                MessageBox.Show("Delete succeeded");
                loadListShowTimes();
            }
            else
            {
                MessageBox.Show("Delete unsuccessful");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            showList.DataSource = searchShowTimes(txtID.Text);
        }

        private void cbLoadCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadListCinemaByName(cbLoadCinema.Text.ToString());
            if (dtgvShows.SelectedCells.Count > 0)
            {
                string id = dtgvShows.SelectedCells[3].OwningRow.Cells["MaPhong"].Value.ToString();
                CinemaRoom room = CinemaRoomDAO.Instance.getRoomByID(id);
                cbRooms.SelectedItem = room;
                int index = -1;
                int i = 0;
                foreach (CinemaRoom item in cbRooms.Items)
                {
                    if (item.MaPhong == room.MaPhong)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbRooms.SelectedIndex = index;
            }
        }

    }
}

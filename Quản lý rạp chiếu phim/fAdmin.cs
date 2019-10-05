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
    public partial class fAdmin : BaseForm
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
            loadListCinema(cbLoadCinema, true);
            loadListShowTimes();
            loadListTicket();
            //load danh mã rạp trong bảng lịch chiếu
            loadListCinema(cbIDCinema, false);
            addShowTimesBinding();
            addTicketBinding();
        }

        void loadListRevenueOfFimls()
        {
            dtgvRevenue.DataSource = fimlsDAO.Instance.getListRevenueOfFimls();
        }

        void loadListRevenueOfCinema()
        {
            dtgvRevenue.DataSource = CinemaDAO.Instance.getListRevenueOfCinema();
        }

        void loadListCinemaByName(string name, int month)
        {
            dtgvRevenue.DataSource = CinemaDAO.Instance.getListCinemaByName(name, month);
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

        void loadListTicket()
        {
            dtgvTicket.DataSource = ticketDAO.Instance.getListTicket();
        }

        void addShowTimesBinding()
        {
            txtID.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaShow", true, DataSourceUpdateMode.Never));
            txtTen.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaPhim", true, DataSourceUpdateMode.Never));
            cbIDCinema.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaRap", true, DataSourceUpdateMode.Never));
            txtNgayKC.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "NgayChieu", true, DataSourceUpdateMode.Never));
            txtNgayKT.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "SoVeDaBan", true, DataSourceUpdateMode.Never));
            cbRooms.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "MaPhong", true, DataSourceUpdateMode.Never));
            txtSum.DataBindings.Add(new Binding("Text", dtgvShows.DataSource, "TongTien", true, DataSourceUpdateMode.Never));
        }

        void addTicketBinding()
        {
            txtmashow.DataBindings.Add(new Binding("Text", dtgvTicket.DataSource, "MaShow", true,
              DataSourceUpdateMode.Never));
            txtmaghe.DataBindings.Add(new Binding("Text", dtgvTicket.DataSource, "MaGhe", true,
              DataSourceUpdateMode.Never));
            txtgiochieu.DataBindings.Add(new Binding("Text", dtgvTicket.DataSource, "GioChieu", true,
              DataSourceUpdateMode.Never));
            txtgia.DataBindings.Add(new Binding("Text", dtgvTicket.DataSource, "GiaVe", true,
              DataSourceUpdateMode.Never));
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
            if (kt % 2 != 0)
            {
                btnView.Text = "Xem doanh thu rạp";
                loadListRevenueOfFimls();
            }
            else
            {
                btnView.Text = "Xem doanh thu phim";
                loadListRevenueOfCinema();
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
           if(this.Is_Shown)
            {
                loadListCinemaByName(((DTO.Cinema)cbLoadCinema.SelectedValue).TenRap, Convert.ToInt32(comboBox1.Text));
                if (dtgvRevenue.Rows.Count == 0||dtgvRevenue == null)
                {
                    MessageBox.Show("Rạp " + cbLoadCinema.Text + " tháng " + comboBox1.Text + " không có doanh thu");
                }
            }
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            menuTicket menu = new menuTicket();
            menu.ShowDialog();
        }

        private void btnEditTicket_Click(object sender, EventArgs e)
        {
            string maShow = txtmashow.Text;
            string maGhe = txtmaghe.Text;
            string gioChieu = txtgiochieu.Text;
            string giaVe = txtgia.Text;
            if (ticketDAO.Instance.updateTicket(maShow, maGhe, gioChieu, Convert.ToInt16(giaVe)))
            {
                MessageBox.Show("Update succeeded");
                loadListTicket();
            }
            else
            {
                MessageBox.Show("Update unsuccessful");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Is_Shown)
            {
                loadListCinemaByName(((DTO.Cinema)cbLoadCinema.SelectedValue).TenRap, Convert.ToInt32(comboBox1.Text));
                if (dtgvRevenue.Rows.Count == 0||dtgvRevenue==null)
                {
                    MessageBox.Show("Rạp " + cbLoadCinema.Text + " tháng " + comboBox1.Text + " không có doanh thu");
                }
            }
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
         
        }
    }
}

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

        public void Load()
        {
            //Khi nhấn nút xem vẫn binding được 
            dtgvShows.DataSource = showList;
            loadListRevenueOfFimls();
            //Load danh sách rạp trong bảng doanh thu
            loadListCinema(cbLoadCinema, true);
            loadListShowTimes();
            loadListTicket();
            loadListChair();
            loadListCinemaRooms();
            //load danh mã rạp trong bảng lịch chiếu
            loadListCinema(cbIDCinema, false);
            loadListCinema(comboBox3, false);
            addShowTimesBinding();
            addTicketBinding();
            addChairBinding();
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

        void loadListChair()
        {
            dtgvChair.DataSource = ChairDAO.Instance.getListChair();
        }

        void loadListCinemaRooms()
        {
            dtgvCinemaRoom.DataSource = CinemaRoomDAO.Instance.getListCinemaRoom();
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
            dtgvShows.DataBindings.Clear();
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

        void addChairBinding()
        {
            txtChair1.DataBindings.Add(new Binding("Text", dtgvChair.DataSource, "MaGhe", true,
              DataSourceUpdateMode.Never));
            comboBox3.DataBindings.Add(new Binding("Text", dtgvChair.DataSource, "MaRap", true,
              DataSourceUpdateMode.Never));
            comboBox2.DataBindings.Add(new Binding("Text", dtgvChair.DataSource, "MaPhong", true,
              DataSourceUpdateMode.Never));
        }

        void loadRoomsInToCombobox(ComboBox cbbox, string id)
        {
            List<CinemaRoom> rooms = CinemaRoomDAO.Instance.getRoomsByID(id);
            cbbox.DataSource = rooms;
            cbbox.DisplayMember = nameof(CinemaRoom.MaPhong);
            cbbox.ValueMember = nameof(CinemaRoom.MaPhong);
            foreach (Binding binding in cbbox.DataBindings)
            {
                binding.ReadValue();
            }
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
            loadRoomsInToCombobox(cbRooms, id);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id;
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem == null) return;
            Cinema selected = comboBox.SelectedItem as Cinema;
            id = selected.MaRap;
            loadRoomsInToCombobox(comboBox2, id);
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
            List<Showtimes> showtimes = ShowtimesDAO.Instance.getListShowTimesByIdFimlsOrIdCinema(id);
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
            dtgvShows.DataBindings.Clear();
            showList.DataSource = searchShowTimes(txtFimlsName.Text);
        }

        private void cbLoadCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Is_Shown)
            {
                loadListCinemaByName(((DTO.Cinema)cbLoadCinema.SelectedValue).TenRap, Convert.ToInt32(comboBox1.Text));
                if (dtgvRevenue.Rows.Count == 0 || dtgvRevenue == null)
                {
                    MessageBox.Show("Rạp " + cbLoadCinema.Text + " tháng " + comboBox1.Text + " không có doanh thu");
                }
            }
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            formAddTicket menu = new formAddTicket();
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
                loadListRevenueOfCinema();
                loadListRevenueOfFimls();
                loadListShowTimes();
            }
            else
            {
                MessageBox.Show("Update unsuccessful");
            }
        }

        private void btnRemoveTicket_Click(object sender, EventArgs e)
        {
            string maShow = txtmashow.Text;
            string maGhe = txtmaghe.Text;
            if (ticketDAO.Instance.deleteTicket(maShow,maGhe))
            {
                MessageBox.Show("Delete succeeded");
                loadListTicket();
                loadListRevenueOfCinema();
                loadListRevenueOfFimls();
                loadListShowTimes();
            }
            else
            {
                MessageBox.Show("Delete unsuccessful");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Is_Shown)
            {
                loadListCinemaByName(((DTO.Cinema)cbLoadCinema.SelectedValue).TenRap, Convert.ToInt32(comboBox1.Text));
                if (dtgvRevenue.Rows.Count == 0 || dtgvRevenue == null)
                {
                    MessageBox.Show("Rạp " + cbLoadCinema.Text + " tháng " + comboBox1.Text + " không có doanh thu");
                }
            }
        }

        private void btnAddChair_Click(object sender, EventArgs e)
        {
            string maShow = txtChair1.Text;
            string maPhim = comboBox2.Text;
            string maRap = comboBox3.Text;
            if (ChairDAO.Instance.insertChair(maShow, maRap, maPhim))
            {
                MessageBox.Show("Insert succeeded");
                loadListChair();
                loadListCinemaRooms();
                loadListRevenueOfCinema();
            }
            else
            {
                MessageBox.Show("Insert unsuccessful");
            }
        }

        private void btnEditChair_Click(object sender, EventArgs e)
        {

            string maShow = txtChair1.Text;
            string maPhim = comboBox3.Text;
            string maRap = comboBox2.Text;
            if (MessageBox.Show("Bạn sẽ cập nhật luôn dữ liệu trong bảng vé", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (ticketDAO.Instance.updateTickets(maShow))
                {
                    if (ChairDAO.Instance.updateChair(maShow, maPhim, maRap))
                    {
                        MessageBox.Show("Update succeeded");
                        loadListChair();
                        loadListCinemaRooms();
                        loadListShowTimes();
                    }
                }
                else
                {
                    MessageBox.Show("Update unsuccessful");
                }
            }
        }

        private void btnRemoveChair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn sẽ xóa luôn dữ liệu trong bảng vé", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                string maGhe = txtChair1.Text;
                if (ChairDAO.Instance.deleteChair(maGhe))
                {
                    loadListTicket();
                    loadListRevenueOfCinema();
                    loadListRevenueOfFimls();
                    loadListShowTimes();
                    loadListChair();
                    loadListCinemaRooms();
                    MessageBox.Show("Delete successful");
                }
                else
                {
                    MessageBox.Show("Delete unsuccessful");
                }
            }
        }

        private void btnthemphong_Click(object sender, EventArgs e)
        {
            string maPhong = txtmaphong2.Text;
            string maRap = txtmarap2.Text;
            string tenPhong = txttenphong.Text;
            if (CinemaRoomDAO.Instance.insertCinemaRoom(maPhong, maRap, tenPhong))
            {
                MessageBox.Show("Insert succeeded");
                loadListCinemaRooms();
            }
            else
            {
                MessageBox.Show("Insert unsuccessful");
            }
        }

        private void btnsuaphong_Click(object sender, EventArgs e)
        {
            string maPhong = txtmaphong2.Text;
            string maRap = txtmarap2.Text;
            string tenPhong = txttenphong.Text;
            if (CinemaRoomDAO.Instance.updateCinemaRoom(maRap, tenPhong, maPhong))
            {
                MessageBox.Show("Update succeeded");
                loadListCinemaRooms();
            }
            else
            {
                MessageBox.Show("Update unsuccessful");
            }
        }

        private void btnxoaphong_Click(object sender, EventArgs e)
        {
            string maPhong = txtmaphong2.Text;
            if (ChairDAO.Instance.deleteChair(maPhong))
            {
                MessageBox.Show("Delete succeeded");
                loadListCinemaRooms();
            }
            else
            {
                MessageBox.Show("Delete unsuccessful");
            }
        }

        private void btnXemVe_Click(object sender, EventArgs e)
        {
            loadListTicket();
            loadListShowTimes();
        }
    }
}

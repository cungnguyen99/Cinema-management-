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
    public partial class menuTicket : Form
    {
        private int id;
        public menuTicket()
        {
            InitializeComponent();
            loadCinemaRooms1();

        }

        void loadCinemaRooms1()
        {
            id = 1;
            List<CinemaRoom> fimlsList = CinemaRoomDAO.Instance.getRoomsByID(id.ToString());
            foreach (CinemaRoom item in fimlsList)
            {
                Button btn = new Button() { Width = 100, Height = 120 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.Text = "Phòng " + item.MaPhong;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            if (((sender as Button).Tag as CinemaRoom).MaPhong != null)
            {
                string fimlsID = ((sender as Button).Tag as CinemaRoom).MaPhong.ToString();
                showRooms(fimlsID);
            }
        }

        private void showRooms(string id)
        {
            listView1.Items.Clear();
            List<CinemaRoom> listFimls = CinemaRoomDAO.Instance.getRoomsByID(id);
            //foreach (CinemaRoom item in fimlsList)
            //{
            //    Button btn = new Button() { Width = 100, Height = 120 };
            //    btn.Tag = item;
            //    btn.Click += btn_click;
            //    btn.Text = "Phòng " + item.MaPhong;
            //    flowLayoutPanel1.Controls.Add(btn);
            //}
        }
    }
}

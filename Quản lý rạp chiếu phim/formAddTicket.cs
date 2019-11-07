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
    public partial class formAddTicket : Form
    {
        string fimlsID="MS001";

        public formAddTicket()
        {
            InitializeComponent();
            loadListShow();
            loadListChairEmpty(fimlsID);
            showChairInCinemaRoom("MS001");
            loadTimeInToCombobox();
        }

        List<Chair> listChairs = ChairDAO.Instance.getListChair();
        List<Showtimes> showtimes = ShowtimesDAO.Instance.loadListShowtimes();

        void loadListChairEmpty(string id)
        {
            List<Chair> chairs = ChairDAO.Instance.getListChairEmpty(id);
            comboBox2.DataSource = chairs;
            comboBox2.DisplayMember = "MaGhe";
        }

        void loadListShow()
        {
            List<Showtimes> fimlsList = ShowtimesDAO.Instance.loadListShowtimes();
            foreach (Showtimes item in fimlsList)
            {
                Button btn = new Button() { Width = 50, Height = 50 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.Text = item.MaShow;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        void loadTimeInToCombobox()
        {
            List<Ticket> fimlsList = ticketDAO.Instance.loadTime();
            comboBox1.DataSource = fimlsList;
            comboBox1.DisplayMember = "GioChieu";
        }


        bool IsValidShowTime(string lichChieu, string maShow)
        {
            using (DataTable dttmp = DataProvider.Instance.ExecuteReturnDataTable(CommonConst.SPNAME_IsValidTicket, "@MaShow", maShow, "@ShowTime", lichChieu))
            {
                return dttmp != null && dttmp.Rows.Count > 0 && dttmp.Rows[0][0] is bool isValid && isValid;
            }
        }

        private void showChairInCinemaRoom(string id)
        {
            flowLayoutPanel2.Controls.Clear();
            List<Chair> chairs = ChairDAO.Instance.getListChairEmpty(id);
            foreach (Chair item in listChairs)
            {
                Button btn = new Button() { Width = 50, Height = 70 };
                foreach (Chair items in chairs)
                {
                    btn.Tag = item;
                    btn.Click += btn_click;
                    if (items.MaGhe == item.MaGhe)
                    {
                        btn.Text = item.MaGhe;
                        btn.ForeColor = Color.White;
                    }
                    btn.Margin = new Padding(0, 0, 15, 15);
                    btn.BackgroundImage= Image.FromFile(@"D:\My Easy Life\c1.jpg");
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
                flowLayoutPanel2.Controls.Add(btn);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {   
            if (((sender as Button).Tag as Showtimes).MaShow != null)
            {
                fimlsID = ((sender as Button).Tag as Showtimes).MaShow.ToString();
                loadListChairEmpty(fimlsID);
                showChairInCinemaRoom(fimlsID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool checkShowTime(string id)
        {
            List<Ticket> fimlsList = ticketDAO.Instance.loadListTicket();
            foreach (Ticket item in fimlsList)
            {
                if (comboBox1.Text == item.MaShow.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        bool checkTime(string id)
        {
            List<Ticket> fimlsList = ticketDAO.Instance.checkTimeTicket(id);
            foreach (Ticket item in fimlsList)
            {
                if (comboBox1.Text == item.GioChieu)
                {
                    return true;
                }
            }
            return false;
        }

        bool checkIdShow(string id)
        {
            List<Ticket> tickets = ticketDAO.Instance.loadListTicket ();
            foreach (Ticket item in tickets)
            {
                if (fimlsID != item.MaShow)
                {
                    return true;
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || textBox9.Text =="")
            {
                MessageBox.Show("Fill in the blanks");
            }
            else
            {
                string maPhim = comboBox2.Text;
                string gioChieu = comboBox1.Text;
                int maPhong = Convert.ToInt32(textBox9.Text);
                if (!IsValidShowTime(gioChieu, fimlsID))
                {
                    MessageBox.Show("Correct showtimes. Enter a different showtime");
                }
                else
                {
                    if (checkShowTime(fimlsID))
                    {
                        MessageBox.Show(checkShowTime(fimlsID)+fimlsID+"cccc");
                        //if (checkTime(fimlsID))
                        //{
                        //    if (ticketDAO.Instance.insertTicket(fimlsID, maPhim, gioChieu, maPhong))
                        //    {
                        //        MessageBox.Show("Insert succeeded");
                        //        showChairInCinemaRoom(fimlsID);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Insert unsuccessful");
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Show time do not match");
                        //}
                    }
                    else 
                    {
                        MessageBox.Show(checkShowTime(fimlsID) + fimlsID+"q4887874872");
                        //if (ticketDAO.Instance.insertTicket(fimlsID, maPhim, gioChieu, maPhong))
                        //{
                        //    MessageBox.Show("Insert succeeded");
                        //    showChairInCinemaRoom(fimlsID);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Insert unsuccessful");
                        //}
                    }
                }
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Ticket> tickets = ticketDAO.Instance.loadTimeByIdShowTime(fimlsID);
            foreach (Ticket item in tickets)
            {
                comboBox1.Text = item.GioChieu;
            }
        }
    }
}

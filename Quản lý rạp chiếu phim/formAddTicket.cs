using Quản_lý_rạp_chiếu_phim.DAO;
using Quản_lý_rạp_chiếu_phim.DTO;
using Quản_lý_rạp_chiếu_phim.Lib;
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
        fAdmin f1 = new fAdmin();


        public formAddTicket()
        {
            InitializeComponent();
            loadListShow();
            showChairInCinemaRoom("MS001");
            textBox1.Text = "MS001";
        }

        List<Chair> listChairs = ChairDAO.Instance.getListChair();
        List<Showtimes> showtimes = ShowtimesDAO.Instance.loadListShowtimes();
        List<Showtimes> listId;

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
                    btn.Text = item.MaGhe;
                    //btn.BackColor = Color.Tomato;
                    if (items.MaGhe == item.MaGhe)
                    {
                        btn.BackColor = Color.LightSkyBlue;
                    }
                }
                flowLayoutPanel2.Controls.Add(btn);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            if (((sender as Button).Tag as Showtimes).MaShow != null)
            {
                string fimlsID = ((sender as Button).Tag as Showtimes).MaShow.ToString();
                textBox1.Text = fimlsID;
                showChairInCinemaRoom(fimlsID);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //foreach (Showtimes item in showtimes)
            //{
            //    if (textBox1.Text != item.MaShow)
            //    {
            //        MessageBox.Show("Khong co ma sho do trong CSDL");
            //    }
            //}
            loadListChairEmpty(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maShow = textBox1.Text;
            string maPhim = comboBox2.Text;
            string gioChieu = textBox11.Text;
            int maPhong = Convert.ToInt32(textBox9.Text);
            if (!IsValidShowTime(gioChieu, maShow))
            {
                MessageBox.Show("Correct showtimes. Enter a different showtime");
            }
            else
            {
                MessageBox.Show("Insert unsuccessful");
                //if (ticketDAO.Instance.insertTicket(maShow, maPhim, maRap, maPhong))
                //{
                //    MessageBox.Show("Insert succeeded");
                //    showChairInCinemaRoom(textBox1.Text);
                //}
                //else
                //{
                //    MessageBox.Show("Insert unsuccessful");
                //}
            }
        }
    }
}

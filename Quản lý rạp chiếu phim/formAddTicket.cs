﻿using Quản_lý_rạp_chiếu_phim.DAO;
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
        public formAddTicket()
        {
            InitializeComponent();
            loadListShow();
            showChairInCinemaRoom("MS001");
        }

        List<Chair> listChairs = ChairDAO.Instance.getListChair();

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
                showChairInCinemaRoom(fimlsID);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
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
            string maRap = textBox11.Text;
            string maPhong = textBox9.Text;
            if (ChairDAO.Instance.insertChair(maShow, maPhim, maRap, maPhong))
            {
                MessageBox.Show("Insert succeeded");
            }
            else
            {
                MessageBox.Show("Insert unsuccessful");
            }
        }
    }
}

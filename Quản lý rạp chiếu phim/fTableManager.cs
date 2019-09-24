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
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            loadFimls();
        }

        void loadFimls()
        {
            List<Fimls> fimlsList = fimlsDAO.Instance.loadListTable();
            foreach (Fimls item in fimlsList)
            {
                Button btn = new Button() { Width = 110, Height = 140 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.BackgroundImage = Image.FromFile(@"D:\My Easy Life\My Film\"+item.Anh.ToString());
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                flbTable.Controls.Add(btn);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            pictureBox1.Visible = true;
            if (((sender as Button).Tag as Fimls).MaPhim != null)
            {
                string fimlsID = ((sender as Button).Tag as Fimls).MaPhim.ToString();
                showFimls(fimlsID);
            }
        }

        private void showFimls(string fimlsID)
        {
            listView1.Items.Clear();
            List<Fimls> listFimls = fimlsDAO.Instance.getListFimsByIdFiml(fimlsID);
            foreach (Fimls item in listFimls)
            {
                label1.Text = "Tên phim: " + item.TenPhim;
                label2.Text = "Nữ chính: "+item.NuChinh;
                label3.Text = "Nam chính: "+item.NamChinh;
                label4.Text = "Số tiền làm phim: "+ item.TongChi.ToString();
                label5.Text = "Ngày sản xuất: "+item.NgayKhoiChieu.ToString();
                pictureBox1.BackgroundImage = Image.FromFile(@"D:\My Easy Life\My Film\" + item.Anh.ToString());
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount account = new fAccount();
            account.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin admin = new fAdmin();
            admin.ShowDialog();
        }


        List<Fimls> searchFimlsByFimlsName(string name)
        {
            List<Fimls> fimls = fimlsDAO.Instance.searchFimlsByFimlsName(name);
            return fimls;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            flbTable.Controls.Clear();
            List<Fimls> fimls=searchFimlsByFimlsName(textBox1.Text);
            listView1.Items.Clear();
            foreach (Fimls item in fimls)
            {
                Button btn = new Button() { Width = 100, Height = 130 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.BackgroundImage = Image.FromFile(@"D:\My Easy Life\My Film\" + item.Anh.ToString());
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                flbTable.Controls.Add(btn);
            }
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            flbTable.Controls.Clear();
            loadFimls();
        }
    }
}

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
                Button btn = new Button() { Width = 100, Height = 80 };
                btn.Text = item.TenPhim;
                btn.Click += btn_click;
                btn.BackgroundImage = Image.FromFile(@"D:\My Easy Life\My Film\"+item.Anh.ToString());
                btn.ImageAlign = ContentAlignment.BottomCenter;
                flbTable.Controls.Add(btn);
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            string fimlsID = ((sender as Button).Tag as Fimls).MaPhim;
            showFimls(fimlsID);
        }

        private void showFimls(string fimlsID)
        {
            listView1.Items.Clear();
            List<Fimls> listFimls = fimlsDAO.Instance.getListFimsByIdFiml(fimlsID);
            foreach (Fimls item in listFimls)
            {
                ListViewItem lsvItem = new ListViewItem(item.TenPhim.ToString());
                lsvItem.SubItems.Add(item.NamChinh.ToString());
                listView1.Items.Add(lsvItem);
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
    }
}

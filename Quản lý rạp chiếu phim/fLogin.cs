using Quản_lý_rạp_chiếu_phim.DAO;
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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text;
            string pass = txtPassWord.Text;
            if (login(user, pass))
            {
                fTableManager fTable = new fTableManager();
                this.Hide();
                fTable.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Wrong pass word or name user");
            }
        }

        bool login(string user,string pass)
        {
            return AccountDAO.Instance.login(user, pass);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            this.Shown += BaseForm_Shown;
        }

        protected bool Is_Shown { get; private set; }

        private void BaseForm_Shown(object sender, EventArgs e)
        {
            this.Is_Shown = true;
        }
    }
}

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
using Excel = Microsoft.Office.Interop.Excel;

namespace Quản_lý_rạp_chiếu_phim
{
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            loadFimls();
            showFimls("P001");
        }

        void loadFimls()
        {
            List<Fimls> fimlsList = fimlsDAO.Instance.loadListFimls();
            foreach (Fimls item in fimlsList)
            {
                Button btn = new Button() { Width = 110, Height = 140 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.BackgroundImage = Image.FromFile(@"..\..\" + item.Anh.ToString());
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
                pictureBox1.BackgroundImage = Image.FromFile(@"..\..\" + item.Anh.ToString());
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
            List<Fimls> fimls=searchFimlsByFimlsName(txtSearchFimls.Text);
            if (fimls.Count > 0)
            {
                listView1.Items.Clear();
                foreach (Fimls item in fimls)
                {
                    Button btn = new Button() { Width = 100, Height = 130 };
                    btn.Tag = item;
                    btn.Click += btn_click;
                    btn.BackgroundImage = Image.FromFile(@"..\..\" + item.Anh.ToString());
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    flbTable.Controls.Add(btn);
                }
            }
            else
            {
                MessageBox.Show("There are no movies in the list");
                loadFimls();
            }
            txtSearchFimls.Text = "";
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            flbTable.Controls.Clear();
            loadFimls();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flbTable.Controls.Clear();
            List<Fimls> fimlsList = fimlsDAO.Instance.getListMoviesShowing();
            foreach (Fimls item in fimlsList)
            {
                Button btn = new Button() { Width = 110, Height = 140 };
                btn.Tag = item;
                btn.Click += btn_click;
                btn.BackgroundImage = Image.FromFile(@"..\..\" + item.Anh.ToString());
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                flbTable.Controls.Add(btn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

            //Định dạng chung
            Excel.Range tenCuaHang = (Excel.Range)exSheet.Cells[1, 1];
            tenCuaHang.Font.Size = 14;
            tenCuaHang.Font.Bold = true;
            tenCuaHang.Font.Color = Color.Blue;
            tenCuaHang.Value = "BẢNG DANH SÁCH DOANH THU PHIM";

            Excel.Range dcCuaHang = (Excel.Range)exSheet.Cells[2, 1];
            dcCuaHang.Font.Size = 13;
            dcCuaHang.Font.Bold = true;
            dcCuaHang.Font.Color = Color.Blue;
            dcCuaHang.Value = "Copyright: Nguyễn Văn Cung";

            Excel.Range dtCuaHang = (Excel.Range)exSheet.Cells[3, 1];
            dtCuaHang.Font.Size = 13;
            dtCuaHang.Font.Bold = true;
            dtCuaHang.Font.Color = Color.Blue;
            dtCuaHang.Value = "Điện thoại: 0399544543";


            Excel.Range header = (Excel.Range)exSheet.Cells[5, 2];
            exSheet.get_Range("B5:G5").Merge(true);
            header.Font.Size = 13;
            header.Font.Bold = true;
            header.Font.Color = Color.Red;
            header.Value = "DANH SÁCH DOANH THU PHIM";

            //Định dạng tiêu đề bảng

            exSheet.get_Range("B7:N7").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //exSheet.get_Range("A7").Value = "STT";
            exSheet.get_Range("B7").Value = "Mã Phim";
            exSheet.get_Range("B7").ColumnWidth = 20;
            exSheet.get_Range("C7").Value = "Tên Phim";
            exSheet.get_Range("C7").ColumnWidth = 20;
            exSheet.get_Range("D7").Value = "Mã nước sản xuất";
            exSheet.get_Range("E7").Value = "Mã hãng sản xuất";
            exSheet.get_Range("F7").Value = "Đạo diễn";
            exSheet.get_Range("G7").Value = "Mã thể loại";
            exSheet.get_Range("G7").ColumnWidth = 20;
            exSheet.get_Range("H7").Value = "Ngày khởi chiếu";
            exSheet.get_Range("H7").ColumnWidth = 20;
            exSheet.get_Range("I7").Value = "Ngày kết thúc";
            exSheet.get_Range("I7").ColumnWidth = 20;
            exSheet.get_Range("J7").Value = "Nữ chính";
            exSheet.get_Range("J7").ColumnWidth = 20;
            exSheet.get_Range("K7").Value = "Nam chính";
            exSheet.get_Range("K7").ColumnWidth = 20;
            exSheet.get_Range("L7").Value = "Tổng chi";
            exSheet.get_Range("M7").Value = "Tổng thu";

            DataTable dataTable = fimlsDAO.Instance.getListFimls();
            DataColumnCollection dataColumnCollection = dataTable.Columns;

            //In dữ liệu
            for (int i = 1; i <= dataTable.Rows.Count + 1; i++)
            {
                for (int j = 1; j <= dataTable.Columns.Count; j++)
                {
                    if (i == 1)
                        exApp.Cells[5+i, j] = dataColumnCollection[j - 1].ToString();
                    else
                        exApp.Cells[5+i, j] = dataTable.Rows[i - 2][j - 1].ToString();
                }
            }
            exSheet.Name = "Danh sách doanh thu phim";
            exBook.Activate();
            sdlSave.Filter = "Excel Document(*.xls)|*.xls  |Word Document(*.doc) |*.doc|All files(*.*)|*.*";
            sdlSave.FilterIndex = 1;
            sdlSave.AddExtension = true;
            sdlSave.DefaultExt = ".xls";
            if (sdlSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                exBook.SaveAs(sdlSave.FileName.ToString());
            exApp.Quit();
        }
    }
}

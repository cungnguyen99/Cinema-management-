using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    class fimlsDAO
    {
        private static fimlsDAO instance;

        public static fimlsDAO Instance
        {
            get
            {
                if (instance == null) instance = new fimlsDAO();
                return fimlsDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private fimlsDAO() { }

        public List<Fimls> loadListTable()
        {
            List<Fimls> fimlsList = new List<Fimls>();
            DataTable dataTable = DataProvider.Instance.ExecuteQuery("USP_fimls");
            foreach (DataRow item in dataTable.Rows)
            {
                Fimls fimls = new Fimls(item);
                fimlsList.Add(fimls);
            }
            return fimlsList;
        }

        public List<Fimls> getListFimsByIdFiml(string id)
        {
            List<Fimls> fimls = new List<Fimls>();
            string query = "SELECT TenPhim, NuChinh, NamChinh, NgayKhoiChieu, Anh  FROM PHIM, THELOAI WHERE MaPhim="+id.ToString();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Fimls fiml = new Fimls(item);
                fimls.Add(fiml);
            }
            return fimls;
        }
    }
}

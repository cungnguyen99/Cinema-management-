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

        public List<Fimls> loadListFimls()
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
            // dau @ dat trc dau " de minh co the xuong dong
            // dau $ dat trc dau " de minh gan {id} vao trong string ma ko can + string
            string query = $@"SELECT p.*,t.TenTheLoai
                            FROM PHIM p 
                            left join THELOAI t on p.MaTheLoai = t.MaTheLoai
                            WHERE p.MaPhim='{id}' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Fimls fiml = new Fimls(item);
                fimls.Add(fiml);
            }
            return fimls;
        }

        public List<Fimls> searchFimlsByFimlsName(string name)
        {
            List<Fimls> fimls = new List<Fimls>();
            string query = string.Format("SELECT * FROM PHIM JOIN THELOAI ON PHIM.MaTheLoai=THELOAI.MaTheLoai JOIN HANGSANXUAT ON PHIM.MaHangSX=HANGSANXUAT.MaHangSX WHERE dbo.fuConvertToUnsign1(TenPhim) LIKE N'%'+dbo.fuConvertToUnsign1(N'{0}')+N'%' or dbo.fuConvertToUnsign1(TenTheLoai) LIKE N'%'+dbo.fuConvertToUnsign1(N'{0}')+N'%' or dbo.fuConvertToUnsign1(TenHangSX) LIKE N'%'+dbo.fuConvertToUnsign1(N'{0}')+N'%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Fimls fiml = new Fimls(item);
                fimls.Add(fiml);
            }
            return fimls;
        }

        public DataTable getListFimls()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM PHIM WHERE EXISTS (SELECT LICHCHIEU.MaPhim FROM LICHCHIEU WHERE PHIM.MaPhim=LICHCHIEU.MaPhim)");
        }

        public DataTable getListRevenueOfFimls()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListRevenueOfFimls", new object[] { });
        }

        public List<Fimls> getListMoviesShowing()
        {
            List<Fimls> fimls = new List<Fimls>();
            string query = string.Format("SELECT * FROM PHIM WHERE EXISTS (SELECT LICHCHIEU.MaPhim FROM LICHCHIEU WHERE PHIM.MaPhim=LICHCHIEU.MaPhim)");
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

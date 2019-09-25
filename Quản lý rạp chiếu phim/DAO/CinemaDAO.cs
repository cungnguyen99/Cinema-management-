using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    public class CinemaDAO
    {
        private static CinemaDAO instance;

        public static CinemaDAO Instance
        {
            get
            {
                if (instance == null) instance = new CinemaDAO();
                return CinemaDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private CinemaDAO() { }

        public List<Cinema> getListCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>();
            string query = "SELECT * FROM RAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Cinema cinema = new Cinema(item);
                cinemas.Add(cinema);
            }
            return cinemas;
        }

        public DataTable getListRevenueOfCinema()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT RAP.TenRap AS [Rạp], MAX(RAP.DiaChi) AS [Địa chỉ], MAX(RAP.SDT) AS [Số điện thoại], MAX(RAP.SoPhong) AS [Số phòng], SUM(LICHCHIEU.TongTien) AS [Doanh thu] FROM RAP LEFT JOIN LICHCHIEU ON RAP.MaRap=LICHCHIEU.MaRap GROUP BY RAP.TenRap");
        }

        public DataTable getListCinemaByName(string name)
        {
            return DataProvider.Instance.ExecuteQuery("SELECT RAP.TenRap AS [Rạp], MAX(RAP.DiaChi) AS [Địa chỉ], MAX(RAP.SDT) AS [Số điện thoại], MAX(RAP.SoPhong) AS [Số phòng], SUM(LICHCHIEU.TongTien) AS [Doanh thu] FROM RAP, LICHCHIEU WHERE RAP.MaRap=LICHCHIEU.MaRap AND RAP.TenRap= N'"+name+"'GROUP BY RAP.TenRap");
        }
    }
}

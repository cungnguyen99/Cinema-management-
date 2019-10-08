using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    public class ShowtimesDAO
    {
        private static ShowtimesDAO instance;

        public static ShowtimesDAO Instance
        {
            get
            {
                if (instance == null) instance = new ShowtimesDAO();
                return ShowtimesDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        public ShowtimesDAO() { }

        public DataTable getListShowTimes()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM LICHCHIEU");
        }

        public List<Showtimes> loadListShowtimes()
        {
            List<Showtimes> fimlsList = new List<Showtimes>();
            DataTable dataTable = DataProvider.Instance.ExecuteQuery("SELECT * FROM LICHCHIEU");
            foreach (DataRow item in dataTable.Rows)
            {
                Showtimes fimls = new Showtimes(item);
                fimlsList.Add(fimls);
            }
            return fimlsList;
        }

        public bool insertShowtimes(string maShow, string maPhim, string maRap, string maPhong, DateTime ngayChieu)
        {
            string query = string.Format("Insert Into LICHCHIEU values(N'{0}', N'{1}', N'{2}', N'{3}',{4}, N'{5}',{6})", maShow, maPhim, maRap, maPhong, 0, ngayChieu, 0);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool updateShowtimes(string maShow, string maPhim, string maRap, string maPhong, DateTime ngayChieu)
        {
            string query = string.Format("UPDATE LICHCHIEU SET MaPhim=N'{0}', MaRap=N'{1}', MaPhong=N'{2}',NgayChieu=N'{3}' WHERE MaShow=N'{4}'", maPhim, maRap, maPhong, ngayChieu, maShow);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool deleteShowtimes(string maShow)
        {
            string query = string.Format("DELETE LICHCHIEU WHERE MaShow=N'{0}'", maShow);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public List<Showtimes> getListShowTimesByIdFimlsOrIdCinema(string id)
        {
            List<Showtimes> fimls = new List<Showtimes>();
            string query = string.Format("SELECT * FROM LICHCHIEU WHERE MaPhim = N'{0}' or MaRap = N'{0}'", id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Showtimes fiml = new Showtimes(item);
                fimls.Add(fiml);
            }
            return fimls;
        }
    }
}

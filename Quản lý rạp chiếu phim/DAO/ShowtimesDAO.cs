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

        //public bool insertShowtimes(string maShow, string maPhim, string maRap, 
        //    string maPhong, int soVeDaBan=0, DateTime ngayChieu, int tongTien=0)
        //{
        //    string query = string.Format("Insert Into LICHCHIEU values(N'{0}', N'{1}', N'{2}', N'{3}',{4}, N'{5}',{6})", maShow, maPhim, maRap, maPhong, soVeDaBan, ngayChieu, tongTien);
        //    int result = DataProvider.Instance.ExecuteNonQuery(query);
        //    return result > 0;
        //}
    }
}

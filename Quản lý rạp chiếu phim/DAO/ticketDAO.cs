using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    public class ticketDAO
    {
        private static ticketDAO instance;

        public static ticketDAO Instance
        {
            get
            {
                if (instance == null) instance = new ticketDAO();
                return ticketDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        public ticketDAO() { }

        public DataTable getListTicket()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM VE");
        }

        public bool updateTicket(string maShow, string maGhe, string gioChieu, int giaVe)
        {
            string query = string.Format("UPDATE VE SET MaShow=N'{0}', MaGhe=N'{1}', GioChieu=N'{2}',GiaVe={3} WHERE MaShow=N'{4}'", maShow, maGhe, gioChieu, giaVe, maShow);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool insertTicket(string maShow, string maGhe, string gioChieu, int giaVe)
        {
            string query = string.Format("Insert Into VE values(N'{0}', N'{1}', N'{2}', N'{3}',{4})", maShow, maGhe, gioChieu, giaVe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool deleteTicket(string maShow, string maGhe)
        {
            string query = string.Format("DELETE VE WHERE MaShow=N'{0}' AND MaGhe=N'{1}}'", maShow, maGhe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

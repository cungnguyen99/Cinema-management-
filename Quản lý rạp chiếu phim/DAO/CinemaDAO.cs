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

        public DataTable getListRevenueOfCinema(int month)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListRevenueOfCinema @month", new object[] {month});
        }
    }
}

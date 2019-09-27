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
    }
}

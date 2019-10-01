using System;
using System.Collections.Generic;
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

    }
}

using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    public class ChairDAO
    {
        private static ChairDAO instance;

        public static ChairDAO Instance
        {
            get
            {
                if (instance == null) instance = new ChairDAO();
                return ChairDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private ChairDAO() { }

        public List<Chair> getListChair(string id)
        {

        }
    }
}

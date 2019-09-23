using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    class CinemaRoomDAO
    {
        private static CinemaRoomDAO instance;

        public static CinemaRoomDAO Instance
        {
            get
            {
                if (instance == null) instance = new CinemaRoomDAO();
                return CinemaRoomDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private CinemaRoomDAO(){}

        //public List<CinemaRoom> getListCinemaRoom()
        //{
        //    return 
        //}
    }
}

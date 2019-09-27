using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<CinemaRoom> getListCinemaRoom()
        {
            List<CinemaRoom> rooms = new List<CinemaRoom>();
            string query = "SELECT * FROM PHONGCHIEU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CinemaRoom room = new CinemaRoom(item);
                rooms.Add(room);
            }
            return rooms;
        }

        public CinemaRoom getRoomByID(string id)
        {
            CinemaRoom room = null;
            string query = "SELECT * FROM PHONGCHIEU WHERE MaPhong = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                room = new CinemaRoom(item);
                return room;
            }
            return room;
        }

        public List<CinemaRoom> getRoomsByID(string id)
        {
            List<CinemaRoom> rooms = new List<CinemaRoom>();
            string query = "SELECT * FROM PHONGCHIEU WHERE MaRap = "+id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CinemaRoom room = new CinemaRoom(item);
                rooms.Add(room);
            }
            return rooms;
        }
    }
}

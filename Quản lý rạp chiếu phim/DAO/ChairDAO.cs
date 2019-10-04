using Quản_lý_rạp_chiếu_phim.DTO;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Chair> getListChair(string id=null)
        {
            List<Chair> fimlsList = new List<Chair>();
            DataTable dataTable = DataProvider.Instance.ExecuteQuery("SELECT * FROM GHE  WHERE MaPhong = N'"+id+"'");
            foreach (DataRow item in dataTable.Rows)
            {
                Chair fimls = new Chair(item);
                fimlsList.Add(fimls);
            }
            return fimlsList;
        }

        public List<Chair> getListChairEmpty(string id)
        {
            List<Chair> fimlsList = new List<Chair>();
            string query = string.Format("SELECT * FROM GHE WHERE NOT EXISTS (SELECT VE.MaGhe FROM VE WHERE VE.MaGhe=GHE.MaGhe AND VE.MaShow=N'{0}')", id);
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dataTable.Rows)
            {
                Chair fimls = new Chair(item);
                fimlsList.Add(fimls);
            }
            return fimlsList;
        }

        public bool insertChair(string maShow, string maGhe, string gioChieu, string giaVe)
        {
            string query = string.Format("Insert Into VE values(N'{0}', N'{1}', N'{2}', N'{3}')", maShow, maGhe, gioChieu, giaVe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

    }
}

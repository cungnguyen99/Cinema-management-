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

        public List<Chair> getListChair()
        {
            List<Chair> fimlsList = new List<Chair>();
            DataTable dataTable = DataProvider.Instance.ExecuteQuery("SELECT * FROM GHE");
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

        public bool insertChair(string maGhe, string maRap, string maPhong)
        {
            string query = string.Format("Insert Into GHE values(N'{0}', N'{1}', N'{2}')", maGhe, maRap, maPhong);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool deleteChair(string maGhe)
        {
            string query = string.Format("DELETE GHE WHERE MaGhe=N'{0}'", maGhe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool updateChair(string maGhe, string maRap, string maPhong, string maGheSau)
        {
            string query = string.Format("UPDATE GHE SET MaGhe=N'{0}', MaRap=N'{1}', MaPhong=N'{2}' WHERE MaGhe=N'{3}'", maGheSau, maRap, maPhong, maGhe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null) instance = new AccountDAO();
                return AccountDAO.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private AccountDAO() { }

        public bool login(string user, string pass)
        {
            string query = "SELECT * FROM ACCOUNT WHERE UserName=N'" + user + "'AND PASSWORD=N'" + pass + "'";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            return dataTable.Rows.Count > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DAO
{
    public class DataProvider
    {
        string connectionSTR = "Data Source=.\\sqlexpress;Initial Catalog=QLGiaoHang;Integrated Security=True";
        public DataTable ExecuteQuery(string query, object [] paramater=null)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string [] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            com.Parameters.AddWithValue(item,listPara[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.Fill(table);
                connection.Close();
            }
            return table;
        }

        public int ExecuteNonQuery(string query, object[] paramater = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            com.Parameters.AddWithValue(item, listPara[i]);
                            i++;
                        }
                    }
                }
                data = com.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] paramater = null)
        {
            object table = 0;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            com.Parameters.AddWithValue(item, listPara[i]);
                            i++;
                        }
                    }
                }
                table = com.ExecuteScalar();
                connection.Close();
            }
            return table;
        }
    }
}

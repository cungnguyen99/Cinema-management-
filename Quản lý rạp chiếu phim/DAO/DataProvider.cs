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
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider();
                return DataProvider.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private DataProvider() { }

        string connectionSTR = "Data Source=.\\sqlexpress;Initial Catalog=Cinema;Integrated Security=True";

        //Trả về số dòng kết quả

        public DataTable ExecuteQuery(string query, object[] paramater = null)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
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
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(com);
                sqlDataAdapter.Fill(table);
                connection.Close();
            }
            return table;
        }

        //Trả về số trường dữ liệu thỏa mãn được thực thi(insert, delete, update)
        public int ExecuteNonQuery(string query, object[] paramater = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
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

        public DataTable ExecuteReturnDataTable(string procedureName, params object[] args)// params object[] args : nguyene cụm này có nghĩa là từ bên ngoài gọi vào sẽ truyền dc vô hạn phần tử, nhưng sau khi vào đây sẽ tạo thành 1 mảng object trong biến args. e xem bene hamf IsValidShowTime nguyene cumj nayf: "@MaShow", maShow, "@ShowTime", lichChieu sẽ dc truyền vào args.
            // Quy ước của a là : phần tử chẵn là tên biến, phần tử lẻ kế tiếp là value của biến.
        {
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    cmd.Connection = conn;

                    var prs = args?.Select((val, ind) => new { ind, val })// ?. có nghĩa là nếu khác null thì xxx
                        // Select((val,ind)) ~> cái này là lambda syntax. khi có 1 mảng mà muốn lấy value và index của từng phần từ thì dùng syntax này.
                        // => new {val, ind} : lên mạng tìm từ khóa C# anonymous type
                        .Where(c => c.ind % 2 == 0) // lấy các phần từ có index %2 =0 tức alf tên biến 
                        
                        .Select(c => new SqlParameter(args[c.ind].ToString(), args[c.ind + 1]))
                        .ToArray();
                    if (prs?.Count() > 0)
                    {
                        cmd.Parameters.AddRange(prs);
                    }
                    using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                    {
                        DataTable dtRes = new DataTable();
                        adap.Fill(dtRes);
                        return dtRes;
                    }
                }
            }
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

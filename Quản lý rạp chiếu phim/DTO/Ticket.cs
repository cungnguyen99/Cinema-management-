using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    public class Ticket
    {
        private string maShow;
        private string maGhe;
        private string gioChieu;
        private int giaVe;

        public Ticket(string maShow, string maGhe, string gioChieu, int giaVe)
        {
            this.maShow = maShow;
            this.maGhe = maGhe;
            this.gioChieu = gioChieu;
            this.giaVe = giaVe;
        }

        public string MaShow { get => maShow; set => maShow = value; }
        public string MaGhe { get => maGhe; set => maGhe = value; }
        public string GioChieu { get => gioChieu; set => gioChieu = value; }
        public int GiaVe { get => giaVe; set => giaVe = value; }

        public Ticket(DataRow row)
        {
            this.maShow = row["maShow"].ToString();
            this.maGhe = row["maGhe"].ToString();
            this.gioChieu = row["gioChieu"].ToString();
            this.giaVe = (int)row["giaVe"];
        }
    }
}

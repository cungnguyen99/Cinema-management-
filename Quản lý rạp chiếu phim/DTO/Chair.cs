using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    public class Chair
    {
        private string maGhe;
        private string maRap;
        private string maPhong;

        public Chair(string maGhe, string maRap, string maPhong)
        {
            this.maGhe = maGhe;
            this.maRap = maRap;
            this.maPhong = maPhong;
        }

        public Chair(DataRow row)
        {
            this.maGhe = row["MaGhe"].ToString();
            this.maRap = row["MaRap"].ToString();
            this.maPhong = row["MaPhong"].ToString();
        }

        public string MaGhe { get => maGhe; set => maGhe = value; }
        public string MaRap { get => maRap; set => maRap = value; }
        public string MaPhong { get => maPhong; set => maPhong = value; }
    }
}

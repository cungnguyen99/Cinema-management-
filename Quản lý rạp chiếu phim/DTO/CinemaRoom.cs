using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    class CinemaRoom
    {
        private string maPhong;
        private string maRap;
        private string tenPhong;
        private int tongSoGhe;

        public CinemaRoom(string maPhong, string maRap, string tenPhong, int tongSoGhe)
        {
            this.maPhong = maPhong;
            this.maRap = maRap;
            this.tenPhong = tenPhong;
            this.tongSoGhe = tongSoGhe;
        }

        public CinemaRoom() { }

        public CinemaRoom(DataRow row)
        {
            this.maPhong = row["maPhong"].ToString();
            this.maRap = row["maRap"].ToString();
            this.tenPhong = row["tenPhong"].ToString();
            this.tongSoGhe = (int)row["tongSoGhe"];
        }

        public string MaPhong { get => maPhong; set => maPhong = value; }
        public string MaRap { get => maRap; set => maRap = value; }
        public string TenPhong { get => tenPhong; set => tenPhong = value; }
        public int TongSoGhe { get => tongSoGhe; set => tongSoGhe = value; }
    }
}

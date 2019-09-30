using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    public class Showtimes
    {
        private string maShow;
        private string maPhim;
        private string maRap;
        private string maPhong;
        private int soVeDaBan;
        private DateTime NgayChieu;
        private int TongTien;

        public Showtimes(string maShow, string maPhim, string maRap, string maPhong, int soVeDaBan, DateTime ngayChieu, int tongTien)
        {
            this.maShow = maShow;
            this.maPhim = maPhim;
            this.maRap = maRap;
            this.maPhong = maPhong;
            this.soVeDaBan = soVeDaBan;
            NgayChieu = ngayChieu;
            TongTien = tongTien;
        }

        private Showtimes() { }

        public Showtimes(DataRow row)
        {
            this.maShow = row["Mashow"].ToString();
            this.maPhim = row["MaPhim"].ToString();
            this.maRap = row["maRap"].ToString();
            this.maPhong = row["maPhong"].ToString();
            this.soVeDaBan = (int)row["soVeDaBan"];
            this.NgayChieu = (DateTime)row["ngayChieu"];
            this.TongTien =(int)row["tongTien"];
        }

        public string MaShow { get => maShow; set => maShow = value; }
        public string MaPhim { get => maPhim; set => maPhim = value; }
        public string MaRap { get => maRap; set => maRap = value; }
        public string MaPhong { get => maPhong; set => maPhong = value; }
        public int SoVeDaBan { get => soVeDaBan; set => soVeDaBan = value; }
        public DateTime NgayChieu1 { get => NgayChieu; set => NgayChieu = value; }
        public int TongTien1 { get => TongTien; set => TongTien = value; }
    }
}

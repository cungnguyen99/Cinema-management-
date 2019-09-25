using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    public class Cinema
    {
        private string maRap;
        private string tenRap;
        private string diaChi;
        private string soDienThoai;
        private int soPhong;
        private int tongSoGhe;
        private int tongTien;
        private DateTime ngayChieu;

        public Cinema() { }

        public Cinema(string maRap, string tenRap, string diaChi, string soDienThoai, int soPhong, int tongSoGhe, int tongTien, DateTime ngayChieu)
        {
            this.maRap = maRap;
            this.tenRap = tenRap;
            this.diaChi = diaChi;
            this.soDienThoai = soDienThoai;
            this.soPhong = soPhong;
            this.tongSoGhe = tongSoGhe;
            this.tongTien = tongTien;
            this.ngayChieu = ngayChieu;
        }

        public string MaRap { get => maRap; set => maRap = value; }
        public string TenRap { get => tenRap; set => tenRap = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public int SoPhong { get => soPhong; set => soPhong = value; }
        public int TongSoGhe { get => tongSoGhe; set => tongSoGhe = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
        public DateTime NgayChieu { get => ngayChieu; set => ngayChieu = value; }

        public Cinema(DataRow row)
        {
            this.maRap = row["MaRap"].ToString();
            this.tenRap = row["TenRap"].ToString();
            this.diaChi = row["DiaChi"].ToString();
            this.soDienThoai = row["SoDienThoai"].ToString();
            this.soPhong = (int)row["SoPhong"];
            this.tongSoGhe = (int)row["TongSoGhe"];
            this.tongTien = (int)row["TongTien"];
            this.ngayChieu = (DateTime)row["NgayChieu"];
        }
    }
}

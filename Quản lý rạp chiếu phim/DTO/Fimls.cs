using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_rạp_chiếu_phim.DTO
{
    public class Fimls
    {
        private string maPhim;
        private string tenPhim;
        private string maNuocSX;
        private string maHangSX;
        private string daoDien;
        private string maTheLoai;
        private DateTime ngayKhoiChieu;
        private DateTime NgayKetThuc;
        private string nuChinh;
        private string namChinh;
        private int tongChi;
        private int tongThu;
        private string anh;

        public Fimls(string maPhim, string tenPhim, string maNuocSX, string maHangSX, string daoDien, string maTheLoai, DateTime ngayKhoiChieu, DateTime ngayKetThuc, string nuChinh, string namChinh, int tongChi, int tongThu, string anh)
        {
            this.maPhim = maPhim;
            this.tenPhim = tenPhim;
            this.maNuocSX = maNuocSX;
            this.maHangSX = maHangSX;
            this.daoDien = daoDien;
            this.maTheLoai = maTheLoai;
            this.ngayKhoiChieu = ngayKhoiChieu;
            NgayKetThuc = ngayKetThuc;
            this.nuChinh = nuChinh;
            this.namChinh = namChinh;
            this.tongChi = tongChi;
            this.tongThu = tongThu;
            this.anh = anh;
        }

        public string MaPhim { get => maPhim; set => maPhim = value; }
        public string TenPhim { get => tenPhim; set => tenPhim = value; }
        public string MaNuocSX { get => maNuocSX; set => maNuocSX = value; }
        public string MaHangSX { get => maHangSX; set => maHangSX = value; }
        public string DaoDien { get => daoDien; set => daoDien = value; }
        public string MaTheLoai { get => maTheLoai; set => maTheLoai = value; }
        public DateTime NgayKhoiChieu { get => ngayKhoiChieu; set => ngayKhoiChieu = value; }
        public DateTime NgayKetThuc1 { get => NgayKetThuc; set => NgayKetThuc = value; }
        public string NuChinh { get => nuChinh; set => nuChinh = value; }
        public string NamChinh { get => namChinh; set => namChinh = value; }
        public int TongChi { get => tongChi; set => tongChi = value; }
        public int TongThu { get => tongThu; set => tongThu = value; }
        public string Anh { get => anh; set => anh = value; }

        public Fimls(DataRow row)
        {
            this.maPhim = row["MaPhim"].ToString();
            this.tenPhim = row["TenPhim"].ToString();
            this.maNuocSX = row["MaNuocSX"].ToString();
            this.maHangSX = row["MaHangSX"].ToString();
            this.daoDien = row["DaoDien"].ToString();
            this.maTheLoai = row["MaTheLoai"].ToString();
            this.ngayKhoiChieu =(DateTime)row["NgayKhoiChieu"];
            this.NgayKetThuc = (DateTime)row["NgayKetThuc"];
            this.nuChinh = row["NuChinh"].ToString();
            this.namChinh = row["NamChinh"].ToString();
            this.tongChi = (int)row["TongChiPhi"];
            this.tongThu = (int)row["TongThu"];
            this.anh = row["Anh"].ToString();
        }
    }
}

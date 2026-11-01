using System;
using System.Collections.Generic;
using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL
{
    public class DocGiaBLL
    {
        private readonly IDocGiaDAL dal;

        public DocGiaBLL()
        {
            dal = new DocGiaDAL();
        }

        public DocGiaBLL(IDocGiaDAL dal)
        {
            this.dal = dal;
        }

        public List<DocGia> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public bool Them(DocGia dg)
        {
            if (string.IsNullOrWhiteSpace(dg.HoTen))
                throw new ArgumentException("Họ tên không được để trống!");

            return dal.Them(dg);
        }

        public bool Sua(DocGia dg)
        {
            if (dg.MaDocGia <= 0)
                throw new ArgumentException("Mã độc giả không hợp lệ!");

            if (string.IsNullOrWhiteSpace(dg.HoTen))
                throw new ArgumentException("Họ tên không được để trống!");

            return dal.Sua(dg);
        }

        public bool Xoa(int ma)
        {
            if (ma <= 0)
                throw new ArgumentException("Mã độc giả không hợp lệ!");

            return dal.Xoa(ma);
        }

        public List<DocGia> TimKiem(string tuKhoa)
        {
            return dal.TimKiem(tuKhoa.Trim());
        }
    }
}
using System.Collections.Generic;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL
{
    public interface IDocGiaDAL
    {
        List<DocGia> LayDanhSach();

        bool Them(DocGia dg);

        bool Sua(DocGia dg);

        bool Xoa(int maDocGia);

        List<DocGia> TimKiem(string tuKhoa);
    }
}
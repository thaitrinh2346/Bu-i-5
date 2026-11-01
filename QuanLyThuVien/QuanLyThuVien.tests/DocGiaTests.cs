using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Tests
{
    // DAL giả để kiểm thử
    public class FakeDocGiaDAL : IDocGiaDAL
    {
        public List<DocGia> DanhSach = new List<DocGia>();

        public List<DocGia> LayDanhSach()
        {
            return DanhSach;
        }

        public bool Them(DocGia dg)
        {
            dg.MaDocGia = DanhSach.Count + 1;
            DanhSach.Add(dg);
            return true;
        }

        public bool Sua(DocGia dg)
        {
            DocGia? cu = DanhSach.Find(x =>
                x.MaDocGia == dg.MaDocGia);

            if (cu == null) return false;

            cu.HoTen = dg.HoTen;
            cu.Email = dg.Email;

            return true;
        }

        public bool Xoa(int maDocGia)
        {
            DocGia? dg = DanhSach.Find(x =>
                x.MaDocGia == maDocGia);

            if (dg == null) return false;

            DanhSach.Remove(dg);
            return true;
        }

        public List<DocGia> TimKiem(string tuKhoa)
        {
            return DanhSach.FindAll(x =>
                x.HoTen.Contains(
                    tuKhoa,
                    StringComparison.OrdinalIgnoreCase));
        }
    }

    [TestClass]
    public class DocGiaTests
    {
        private FakeDocGiaDAL fake = null!;
        private DocGiaBLL bll = null!;

        [TestInitialize]
        public void KhoiTao()
        {
            fake = new FakeDocGiaDAL();
            bll = new DocGiaBLL(fake);
        }

        [TestMethod]
        public void ThemDocGia_HopLe_TraVeTrue()
        {
            DocGia dg = new DocGia
            {
                HoTen = "Nguyen Van A",
                Email = "a@gmail.com"
            };

            bool ketQua = bll.Them(dg);

            Assert.IsTrue(ketQua);
            Assert.AreEqual(1, fake.DanhSach.Count);
        }

        [TestMethod]
        public void ThemDocGia_ThieuHoTen_BaoLoi()
        {
            DocGia dg = new DocGia
            {
                HoTen = ""
            };

            Assert.ThrowsException<ArgumentException>(
                () => bll.Them(dg));
        }

        [TestMethod]
        public void LayDanhSach_TraVeDanhSach()
        {
            fake.DanhSach.Add(new DocGia
            {
                MaDocGia = 1,
                HoTen = "Nguyen Van A"
            });

            Assert.AreEqual(1, bll.LayDanhSach().Count);
        }

        [TestMethod]
        public void SuaDocGia_HopLe_TraVeTrue()
        {
            fake.DanhSach.Add(new DocGia
            {
                MaDocGia = 1,
                HoTen = "Nguyen Van A"
            });

            DocGia dg = new DocGia
            {
                MaDocGia = 1,
                HoTen = "Nguyen Van B",
                Email = "b@gmail.com"
            };

            Assert.IsTrue(bll.Sua(dg));
        }

        [TestMethod]
        public void XoaDocGia_HopLe_TraVeTrue()
        {
            fake.DanhSach.Add(new DocGia
            {
                MaDocGia = 1,
                HoTen = "Nguyen Van A"
            });

            Assert.IsTrue(bll.Xoa(1));
            Assert.AreEqual(0, fake.DanhSach.Count);
        }

        [TestMethod]
        public void TimKiemDocGia_TimThay()
        {
            fake.DanhSach.Add(new DocGia
            {
                MaDocGia = 1,
                HoTen = "Nguyen Van A"
            });

            Assert.AreEqual(1, bll.TimKiem("Nguyen").Count);
        }
    }
}

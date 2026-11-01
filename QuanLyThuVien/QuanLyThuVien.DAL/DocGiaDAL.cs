using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL
{
    public class DocGiaDAL : IDocGiaDAL
    {
        string chuoiKetNoi = @"Server=DESKTOP-CSQGMGV\MSSQLSERVER02;Database=QuanLyThuVien;Trusted_Connection=True;TrustServerCertificate=True";

        // Lấy danh sách độc giả
        public List<DocGia> LayDanhSach()
        {
            List<DocGia> danhSach = new List<DocGia>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                string sql = "SELECT * FROM DocGia";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DocGia dg = new DocGia
                            {
                                MaDocGia = Convert.ToInt32(reader["MaDocGia"]),
                                HoTen = reader["HoTen"]?.ToString() ?? string.Empty,

                                NgaySinh = reader["NgaySinh"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["NgaySinh"]),

                                GioiTinh = reader["GioiTinh"] == DBNull.Value ? null : reader["GioiTinh"].ToString(),
                                SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString()
                            };

                            danhSach.Add(dg);
                        }
                    }
                }
            }

            return danhSach;
        }

        // Thêm độc giả
        public bool Them(DocGia dg)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                string sql = @"
                    INSERT INTO DocGia
                    (HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, Email)
                    VALUES
                    (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @DiaChi, @Email)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    GanThamSo(cmd, dg);

                    conn.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Sửa độc giả
        public bool Sua(DocGia dg)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                string sql = @"
                    UPDATE DocGia SET
                    HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    SoDienThoai = @SoDienThoai,
                    DiaChi = @DiaChi,
                    Email = @Email
                    WHERE MaDocGia = @MaDocGia";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    GanThamSo(cmd, dg);

                    cmd.Parameters.AddWithValue("@MaDocGia", dg.MaDocGia);

                    conn.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Xóa độc giả
        public bool Xoa(int maDocGia)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                string sql = "DELETE FROM DocGia WHERE MaDocGia = @MaDocGia";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);

                    conn.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Tìm kiếm độc giả
        public List<DocGia> TimKiem(string tuKhoa)
        {
            List<DocGia> danhSach = new List<DocGia>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                string sql = @"
                    SELECT * FROM DocGia
                    WHERE HoTen LIKE @TuKhoa
                    OR SoDienThoai LIKE @TuKhoa
                    OR DiaChi LIKE @TuKhoa
                    OR Email LIKE @TuKhoa";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DocGia dg = new DocGia
                            {
                                MaDocGia = Convert.ToInt32(reader["MaDocGia"]),
                                HoTen = reader["HoTen"]?.ToString() ?? string.Empty,

                                NgaySinh = reader["NgaySinh"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["NgaySinh"]),

                                GioiTinh = reader["GioiTinh"] == DBNull.Value ? null : reader["GioiTinh"].ToString(),
                                SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString()
                            };

                            danhSach.Add(dg);
                        }
                    }
                }
            }

            return danhSach;
        }

        // Gán tham số SQL
        private void GanThamSo(SqlCommand cmd, DocGia dg)
        {
            cmd.Parameters.AddWithValue("@HoTen", (object)dg.HoTen ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@NgaySinh",
                dg.NgaySinh.HasValue
                    ? (object)dg.NgaySinh.Value
                    : DBNull.Value
            );

            cmd.Parameters.AddWithValue("@GioiTinh",
                (object?)dg.GioiTinh ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@SoDienThoai",
                (object?)dg.SoDienThoai ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@DiaChi",
                (object?)dg.DiaChi ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Email",
                (object?)dg.Email ?? DBNull.Value);
        }
    }
}

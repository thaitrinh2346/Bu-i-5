using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
using QuanLyThuVien.UI.Helpers;

namespace QuanLyThuVien.UI.Forms
{
    public partial class FrmDocGia : Form
    {
        private readonly DocGiaBLL bll = new DocGiaBLL();

        public FrmDocGia()
        {
            InitializeComponent();

            // Gắn sự kiện cho các nút
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;

            // Sự kiện chọn dòng
            dgvDocGia.CellClick += dgvDocGia_CellClick;

            // Sự kiện khi mở Form
            this.Load += FrmDocGia_Load;
        }

        // Khởi tạo Form
        private void FrmDocGia_Load(object? sender, EventArgs e)
        {
            txtMaDocGia.ReadOnly = true;

            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.Items.Add("Khác");
            cboGioiTinh.SelectedIndex = -1;

            dtpNgaySinh.ShowCheckBox = true;

            dgvDocGia.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvDocGia.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDocGia.MultiSelect = false;

            dgvDocGia.ReadOnly = true;
            dgvDocGia.AllowUserToAddRows = false;

            LoadDanhSach();
        }

        // Hiển thị danh sách độc giả
        private void LoadDanhSach()
        {
            try
            {
                dgvDocGia.DataSource = null;
                dgvDocGia.DataSource = bll.LayDanhSach();

                if (dgvDocGia.Columns.Count > 0)
                {
                    if (dgvDocGia.Columns["MaDocGia"] != null)
                        dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã độc giả";
                    if (dgvDocGia.Columns["HoTen"] != null)
                        dgvDocGia.Columns["HoTen"].HeaderText = "Họ tên";
                    if (dgvDocGia.Columns["NgaySinh"] != null)
                    {
                        dgvDocGia.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                        dgvDocGia.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    if (dgvDocGia.Columns["GioiTinh"] != null)
                        dgvDocGia.Columns["GioiTinh"].HeaderText = "Giới tính";
                    if (dgvDocGia.Columns["SoDienThoai"] != null)
                        dgvDocGia.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                    if (dgvDocGia.Columns["DiaChi"] != null)
                        dgvDocGia.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    if (dgvDocGia.Columns["Email"] != null)
                        dgvDocGia.Columns["Email"].HeaderText = "Email";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Logger.GhiLog("Lỗi tải danh sách: " + ex.Message);
            }
        }

        // Lấy dữ liệu từ giao diện
        private DocGia LayDuLieu()
        {
            DocGia dg = new DocGia();

            dg.HoTen = txtHoTen.Text.Trim();

            dg.NgaySinh = dtpNgaySinh.Checked
                ? dtpNgaySinh.Value.Date
                : (DateTime?)null;

            dg.GioiTinh = cboGioiTinh.Text;
            dg.SoDienThoai = txtSoDienThoai.Text.Trim();
            dg.DiaChi = txtDiaChi.Text.Trim();
            dg.Email = txtEmail.Text.Trim();

            return dg;
        }

        // Thêm độc giả
        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                DocGia dg = LayDuLieu();

                if (bll.Them(dg))
                {
                    MessageBox.Show("Thêm độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Logger.GhiLog("Thêm độc giả: " + dg.HoTen);

                    LoadDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Thêm độc giả thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logger.GhiLog("Lỗi thêm độc giả: " + ex.Message);
            }
        }

        // Sửa độc giả
        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
                {
                    MessageBox.Show("Vui lòng chọn độc giả cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DocGia dg = LayDuLieu();
                dg.MaDocGia = int.Parse(txtMaDocGia.Text);

                if (bll.Sua(dg))
                {
                    MessageBox.Show("Cập nhật độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Logger.GhiLog("Sửa độc giả mã: " + dg.MaDocGia);

                    LoadDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy độc giả cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logger.GhiLog("Lỗi sửa độc giả: " + ex.Message);
            }
        }

        // Xóa độc giả
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
                {
                    MessageBox.Show("Vui lòng chọn độc giả cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maDocGia = int.Parse(txtMaDocGia.Text);

                DialogResult ketQua = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa độc giả này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (ketQua == DialogResult.Yes)
                {
                    if (bll.Xoa(maDocGia))
                    {
                        MessageBox.Show("Xóa độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Logger.GhiLog("Xóa độc giả mã: " + maDocGia);

                        LoadDanhSach();
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logger.GhiLog("Lỗi xóa độc giả: " + ex.Message);
            }
        }

        // Tìm kiếm độc giả
        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();

                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    LoadDanhSach();
                    return;
                }

                dgvDocGia.DataSource = bll.TimKiem(tuKhoa);

                Logger.GhiLog("Tìm kiếm độc giả: " + tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logger.GhiLog("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // Chọn độc giả trên DataGridView
        private void dgvDocGia_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                txtMaDocGia.Text =
                    row.Cells["MaDocGia"].Value?.ToString();

                txtHoTen.Text =
                    row.Cells["HoTen"].Value?.ToString();

                cboGioiTinh.Text =
                    row.Cells["GioiTinh"].Value?.ToString();

                txtSoDienThoai.Text =
                    row.Cells["SoDienThoai"].Value?.ToString();

                txtDiaChi.Text =
                    row.Cells["DiaChi"].Value?.ToString();

                txtEmail.Text =
                    row.Cells["Email"].Value?.ToString();

                object? ngaySinh = row.Cells["NgaySinh"].Value;

                if (ngaySinh != null && ngaySinh != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(ngaySinh);
                    dtpNgaySinh.Checked = true;
                }
                else
                {
                    dtpNgaySinh.Checked = false;
                }
            }
        }

        // Làm mới Form
        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
            LoadDanhSach();
        }

        private void LamMoi()
        {
            txtMaDocGia.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtTimKiem.Clear();

            cboGioiTinh.SelectedIndex = -1;

            dtpNgaySinh.Value = DateTime.Today;
            dtpNgaySinh.Checked = false;

            dgvDocGia.ClearSelection();
        }
    }
}

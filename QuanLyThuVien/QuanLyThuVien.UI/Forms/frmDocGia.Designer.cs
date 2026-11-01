using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien.UI.Forms
{
    public partial class FrmDocGia : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox txtMaDocGia;
        private TextBox txtHoTen;
        private TextBox txtSoDienThoai;
        private TextBox txtDiaChi;
        private TextBox txtEmail;
        private TextBox txtTimKiem;

        private ComboBox cboGioiTinh;
        private DateTimePicker dtpNgaySinh;

        private DataGridView dgvDocGia;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            this.SuspendLayout();

            // Form chính
            this.Text = "QUẢN LÝ ĐỘC GIẢ";
            this.Size = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 10);

            // Tiêu đề
            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ ĐỘC GIẢ";
            lblTieuDe.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(30, 70, 120);
            lblTieuDe.Location = new Point(20, 15);
            lblTieuDe.Size = new Size(500, 45);
            this.Controls.Add(lblTieuDe);

            // Panel thông tin
            Panel pnlThongTin = new Panel();
            pnlThongTin.Location = new Point(20, 75);
            pnlThongTin.Size = new Size(1090, 245);
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlThongTin);

            Label lblThongTin = new Label();
            lblThongTin.Text = "THÔNG TIN ĐỘC GIẢ";
            lblThongTin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblThongTin.ForeColor = Color.FromArgb(30, 70, 120);
            lblThongTin.Location = new Point(20, 12);
            lblThongTin.Size = new Size(300, 30);
            pnlThongTin.Controls.Add(lblThongTin);

            // Cột bên trái
            TaoLabel(pnlThongTin, "Mã độc giả:", 25, 60);
            txtMaDocGia = TaoTextBox(pnlThongTin, 145, 56, true);

            TaoLabel(pnlThongTin, "Họ và tên:", 25, 105);
            txtHoTen = TaoTextBox(pnlThongTin, 145, 101);

            TaoLabel(pnlThongTin, "Ngày sinh:", 25, 150);

            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Location = new Point(145, 146);
            dtpNgaySinh.Size = new Size(300, 30);
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.ShowCheckBox = true;
            dtpNgaySinh.Checked = false;
            pnlThongTin.Controls.Add(dtpNgaySinh);

            // Cột bên phải
            TaoLabel(pnlThongTin, "Giới tính:", 550, 60);

            cboGioiTinh = new ComboBox();
            cboGioiTinh.Location = new Point(680, 56);
            cboGioiTinh.Size = new Size(330, 30);
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            pnlThongTin.Controls.Add(cboGioiTinh);

            TaoLabel(pnlThongTin, "Số điện thoại:", 550, 105);
            txtSoDienThoai = TaoTextBox(pnlThongTin, 680, 101);

            TaoLabel(pnlThongTin, "Địa chỉ:", 550, 150);
            txtDiaChi = TaoTextBox(pnlThongTin, 680, 146);

            TaoLabel(pnlThongTin, "Email:", 25, 195);
            txtEmail = TaoTextBox(pnlThongTin, 145, 191);

            // Panel chức năng
            Panel pnlChucNang = new Panel();
            pnlChucNang.Location = new Point(20, 335);
            pnlChucNang.Size = new Size(1090, 65);
            pnlChucNang.BackColor = Color.White;
            this.Controls.Add(pnlChucNang);

            btnThem = TaoButton("THÊM", 15, Color.FromArgb(40, 167, 69));
            btnSua = TaoButton("SỬA", 150, Color.FromArgb(0, 123, 255));
            btnXoa = TaoButton("XÓA", 285, Color.FromArgb(220, 53, 69));
            btnLamMoi = TaoButton("LÀM MỚI", 420, Color.FromArgb(108, 117, 125));

            pnlChucNang.Controls.AddRange(new Control[]
            {
                btnThem, btnSua, btnXoa, btnLamMoi
            });

            // Ô tìm kiếm
            Label lblTimKiem = new Label();
            lblTimKiem.Text = "Tìm độc giả:";
            lblTimKiem.Location = new Point(20, 425);
            lblTimKiem.Size = new Size(110, 30);
            this.Controls.Add(lblTimKiem);

            txtTimKiem = new TextBox();
            txtTimKiem.Location = new Point(130, 421);
            txtTimKiem.Size = new Size(350, 30);
            this.Controls.Add(txtTimKiem);

            btnTimKiem = TaoButton("TÌM KIẾM", 495, Color.FromArgb(30, 70, 120));
            btnTimKiem.Location = new Point(495, 417);
            this.Controls.Add(btnTimKiem);

            // Tiêu đề bảng
            Label lblDanhSach = new Label();
            lblDanhSach.Text = "DANH SÁCH ĐỘC GIẢ";
            lblDanhSach.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblDanhSach.ForeColor = Color.FromArgb(30, 70, 120);
            lblDanhSach.Location = new Point(20, 470);
            lblDanhSach.Size = new Size(300, 30);
            this.Controls.Add(lblDanhSach);

            // DataGridView
            dgvDocGia = new DataGridView();
            dgvDocGia.Location = new Point(20, 510);
            dgvDocGia.Size = new Size(1090, 185);

            dgvDocGia.BackgroundColor = Color.White;
            dgvDocGia.BorderStyle = BorderStyle.None;
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDocGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDocGia.MultiSelect = false;
            dgvDocGia.ReadOnly = true;
            dgvDocGia.AllowUserToAddRows = false;
            dgvDocGia.RowHeadersVisible = false;

            dgvDocGia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 70, 120);
            dgvDocGia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDocGia.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvDocGia.EnableHeadersVisualStyles = false;
            dgvDocGia.RowTemplate.Height = 30;

            this.Controls.Add(dgvDocGia);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TaoLabel(Panel panel, string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(120, 30);
            panel.Controls.Add(lbl);
        }

        private TextBox TaoTextBox(Panel panel, int x, int y, bool readOnly = false)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(300, 30);
            txt.ReadOnly = readOnly;
            panel.Controls.Add(txt);
            return txt;
        }

        private Button TaoButton(string text, int x, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, 12);
            btn.Size = new Size(120, 40);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }
}

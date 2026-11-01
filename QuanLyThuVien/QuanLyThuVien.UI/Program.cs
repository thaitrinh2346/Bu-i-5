using System;
using System.Windows.Forms;
using QuanLyThuVien.UI.Forms;

namespace QuanLyThuVien.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.Run(new FrmDocGia());
        }
    }
}
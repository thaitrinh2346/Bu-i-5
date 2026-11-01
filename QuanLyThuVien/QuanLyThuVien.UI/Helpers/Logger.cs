using System;
using System.IO;

namespace QuanLyThuVien.UI.Helpers
{
    public class Logger
    {
        public static void GhiLog(string noiDung)
        {
            try
            {
                string folder = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Logs"
                );

                Directory.CreateDirectory(folder);

                string file = Path.Combine(
                    folder,
                    "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt"
                );

                string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    + " - " + noiDung + Environment.NewLine;

                File.AppendAllText(file, log);
            }
            catch (IOException)
            {
                // Không làm dừng chương trình nếu không ghi được log
            }
            catch (UnauthorizedAccessException)
            {
                // Không làm dừng chương trình nếu thiếu quyền ghi file
            }
        }
    }
}
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;

namespace RICH_Connector.Helper
{
    public static class PrintHtmlHelper
    {
        //static string htmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.html";
        public static void Print(string printerName, string html, Margins margins)
        {
            PrintHtml(printerName, html, margins);

        }

        public static void PrintHtmlV2(string printerName, string html, Margins margins)
        {

            string executablePath = $"{AppDomain.CurrentDomain.BaseDirectory}lib\\p-html-2\\PrintHtml.exe";
            PrintHtml(printerName, html, margins, executablePath);

        }

        private static void PrintHtml(string printerName, string html, Margins margins, string executablePath = "")
        {
            var htmlFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RICH\\{Guid.NewGuid().ToString()}.html";
            SaveStringHTMLToFile(html, htmlFilePath);

            if (File.Exists(htmlFilePath))
            {
                // Replace "PrintHtml.exe" with the actual name of your command-line application

            

                executablePath = string.IsNullOrEmpty( executablePath) ?  $"{AppDomain.CurrentDomain.BaseDirectory}lib\\p-html\\PrintHtml.exe" : executablePath;
                string commandLineArgs = @$"-p ""{printerName}"" -l {margins.Left} -t {margins.Top} -r {margins.Right} -b {margins.Bottom} ""{htmlFilePath}""";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = executablePath,
                    Arguments = commandLineArgs,
                    Verb = "runas", // This will prompt for administrator privileges
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static void SaveStringHTMLToFile(string htmlContent, string htmlFilePath)
        {
            try
            {
                // Save the HTML content to the file
                File.WriteAllText(htmlFilePath, htmlContent);
                Console.WriteLine("HTML content saved to file: " + htmlFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void Kill()
        {
           

            try
            {
                Process[] processes = Process.GetProcessesByName("PrintHtml");
                foreach (var process in processes)
                {
                    process.Kill();
                }

                var di = new DirectoryInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RICH\\");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

            }
            catch (Exception)
            {

               // throw;
            }
        }

    }
}
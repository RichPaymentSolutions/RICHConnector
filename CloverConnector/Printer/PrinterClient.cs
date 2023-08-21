using PDFtoPrinter;
using RICH_Connector.Helper;
using System;
using System.Drawing.Printing;

namespace RICH_Connector.Printer
{
    public class PrinterClient
    {
        public PrinterClient () { }
        //static string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.png";

        public void printHtmlFile(string html, int numberOfprints)
        {
            long milliseconds = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            var printerName = "POS-80C";
            for (int index =0; index < numberOfprints; index++)
            {
                PrintHtmlHelper.PrintHtmlV2(printerName, html ,new Margins(0,0,0,0));
            }
            
            long milliseconds4 = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            Console.WriteLine(milliseconds4);
        }

        public void printPdf(int numberOfprints)
        {

            string printerName = "POS-80C"; //"Microsoft Print to PDF"; // "POS-80C";
            string pdfFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.pdf";
             var printer2 = new PDFtoPrinterPrinter();
            for (int index = 0; index < numberOfprints; index++)
            {
               printer2.Print(new PrintingOptions(printerName, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.pdf"));
            }

        }


    }
}

using com.clover.sdk.v3.printer;
using PDFtoPrinter;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RICH_Connector.Printer
{
    public class PrinterClient
    {
        public PrinterClient () { }
        static string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.png";

        public void printHtmlFile(string html)
        {
            Image i = Image.FromFile(fileName);
            SelectPdf.HtmlToPdf converter2 = new SelectPdf.HtmlToPdf();
            var height = i.Height;
            var width = i.Width;
            // set converter options
            if (i.Height < i.Width)
            {
                height = width;
            }
            converter2.Options.PdfPageSize = PdfPageSize.Custom;
            converter2.Options.PdfPageCustomSize = new SizeF(width - 30, height - 30);
            converter2.Options.MarginBottom = 0;
            converter2.Options.MarginTop = 0;
            converter2.Options.MarginLeft = 0;
            converter2.Options.MarginRight = 0;
            converter2.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            converter2.Options.WebPageWidth = width;
            converter2.Options.WebPageHeight = height;
            converter2.Options.WebPageFixedSize = false;
            converter2.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;


            SelectPdf.PdfDocument doc = converter2.ConvertHtmlString(html);

            doc.Save(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.pdf");
            doc.Close();

            i.Dispose();
           
            var printerName = "POS-80C";
            var printer2 = new PDFtoPrinterPrinter();
            printer2.Print(new PrintingOptions(printerName, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.pdf"));
         
        }
    }
}

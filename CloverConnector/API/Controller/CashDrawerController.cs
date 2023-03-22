using Microsoft.AspNetCore.Mvc;
using Microsoft.PointOfService;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace RICH_Connector.API
    {
        public class CashDrawerController
        {
            
            [HttpPost]
            [Route("cash-drawer")]
            public IActionResult OpenCashDrawer()
            {
            //CloverClient.Instance.OpenCashDrawer("test");
            // Define the control code to open the cash drawer
            string controlCode = "\x1B\x70\x01\x50\x50";

            // Define the raw print data as a byte array
            byte[] rawPrintData = Encoding.ASCII.GetBytes(controlCode);

            // Create a new PrintDocument object
            PrintDocument printDocument = new PrintDocument();

            // Set the printer name to the name of your printer
            printDocument.PrinterSettings.PrinterName = "POS-80C";

            // Define a PrintPage event handler that sends the raw print data to the printer
            printDocument.PrintPage += (sender, args) =>
            {
               
                args.Graphics.DrawString(Encoding.ASCII.GetString(rawPrintData), new Font("Courier New", 8), Brushes.Black, 0, 0);
            };

            // Send the raw print data to the printer
            printDocument.Print();

            return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
        }
    }


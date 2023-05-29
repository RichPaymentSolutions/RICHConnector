using Microsoft.AspNetCore.Mvc;
using RICH_Connector.API.Model;
using RICH_Connector.Clover;
using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace RICH_Connector.API
{
    [Route("sales")]
    //[ApiController]
    public class SaleController
    {
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] SaleRequest saleRequest, CancellationToken cancellation)
        {
            CloverClient.Instance.CheckConnection();
            var result = CloverClient.Instance.CreateSale(saleRequest, cancellation);
            return new ObjectResult(new
            {
                status = true,
                data = result
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("print")]
        public IActionResult PrintAsync([FromBody] Receipt receipt)
        {
            var html = new PrintUtils().PrepareReceiptAsync(receipt, "munbyn");
            //string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.png";

            // CloverClient.Instance.CheckConnection();
            int numberOfPrints = receipt.NumberOfSheet > 0 ? receipt.NumberOfSheet : 0;
            if (receipt.IsTmp)
            {
                numberOfPrints = 1;
            }
            new PrinterClient().printHtmlFile(html, 1);
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("cancel")]
        public IActionResult Post()
        {
            var result = CloverClient.Instance.CancelPendingSale();
            return new ObjectResult(new
            {
                status = true,
                data = result
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("cash-drawer")]
        public IActionResult OpenCashDrawer()
        {
            CloverClient.Instance.OpenCashDrawer("test");
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

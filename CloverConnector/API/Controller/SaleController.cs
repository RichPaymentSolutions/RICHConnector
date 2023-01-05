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

namespace RICH_Connector.API
{
    [Route("sales")]
    public class SaleController
    {
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] SaleRequest saleRequest, CancellationToken cancellationToken)
        {
            CloverClient.Instance.CheckConnection();
            var result = CloverClient.Instance.CreateSale(saleRequest, cancellationToken);
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
        [Route("prints")]
        public IActionResult Print([FromBody] Receipt receipt)
        {
            string fileName = PrintUtils.PrepareReceipt(receipt, "clover");
            // CloverClient.Instance.CheckConnection();
            CloverClient.Instance.Print(fileName);
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
    }
}

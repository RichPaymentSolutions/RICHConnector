using Microsoft.AspNetCore.Mvc;
using RICH_Connector.Clover;
using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RICH_Connector.API
    {
        public class CashDrawerController
        {
            
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


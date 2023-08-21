using Microsoft.AspNetCore.Mvc;
using Microsoft.PointOfService;
using RICH_Connector.Clover;
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


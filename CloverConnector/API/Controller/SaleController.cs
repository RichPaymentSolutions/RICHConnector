using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RICH_Connector.API
{
    [Route("[controller]")]
    [ApiController]
    public class SaleController
    {
        [HttpGet]
        [Route("test")]
        public ActionResult<string> Get()
        {
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RICH_Connector.API.Model;
using RICH_Connector.Helper;
using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RICH_Connector.API.Controller
{
    [Route("print/v2")]
    public class PrintV2Controller : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> printPdf()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.pdf";

            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                // Make sure to use a unique name for each uploaded file
               

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                new PrinterClient().printPdf(1);
              
                return Ok(new { filePath });
            }
            else
            {
                return BadRequest("File is not present / file is empty.");
            }
        }

        [HttpPost]
        [Route("print-html")]
        public async Task<IActionResult> printHTMl([FromBody] HtmlModel model)
        {
            if (model != null) {
                var html = model.HtmlContent;
                if (!string.IsNullOrEmpty(html) && HttpHelper.ContainsXHTML(html))
                {
                    // Make sure to use a unique name for each uploaded file
                    new PrinterClient().printHtmlFile(html, 1);

                    return Ok(new { status = true, data = "Success" });
                }
                else
                {
                    return BadRequest("File is not DOM Html or not empty.");
                }
            }
            return BadRequest("Html is null.");
        }
    }
}

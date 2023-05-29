using Microsoft.AspNetCore.Mvc;
using RICH_Connector.API.Model;
using RICH_Connector.Clover;
using RICH_Connector.Printer;
using RichCloverConnector;
using RichCloverConnector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RICH_Connector.API
{
    [Route("print")]
    public class PrintController
    {   
        [HttpPost]
        [Route("payroll")]
        public IActionResult PrintPayrollAsync([FromBody] PrintPayrollRequest request)
        {
            string html = new PrintUtils().PreparePayroll(request.Payroll, "munbyn");
            
            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }
        
        [HttpPost]
        [Route("payroll-staff")]
        public IActionResult PrintPayrolStaff([FromBody] PrintPayrollStaffRequest request)
        {
            string html = new PrintUtils().PreparePayrollStaff(request.PayrollStaff, "munbyn");

            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }
        
        [HttpPost]
        [Route("ticket")]
        public IActionResult PrintTicket([FromBody] PrintTicketRequest request)
        {
            string html = new PrintUtils().PrepareReceiptAsync(request.Ticket, "munbyn");

            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("total-report")]
        public IActionResult PrintTotalReport([FromBody] PrintReportRequest request)
        {
            long milliseconds = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            string html = new PrintUtils().PrepareTotalReport(request.Report, "munbyn");
            milliseconds = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;         
            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("income-report")]
        public IActionResult PrintIncomeReport([FromBody] PrintIncomeRequest request)
        {
            string html = new PrintUtils().PrepareIncomeReport(request.Income, "munbyn");

            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
            return new ObjectResult(new
            {
                status = true,

            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult PrintTransaction([FromBody] PrintTransactionRequest request)
        {
            string html = new PrintUtils().PrepareTransaction(request.Transaction, "munbyn");

            if (request.Printer == "munbyn")
            {
                new PrinterClient().printHtmlFile(html, 1);
            }
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

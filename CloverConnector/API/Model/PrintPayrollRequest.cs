using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichCloverConnector
{
    public class PrintPayrollRequest
    {
        public String Printer { get; set; }
        public Payroll Payroll { get; set; }
    }

    public class PrintPayrollStaffRequest
    {
        public String Printer { get; set; }
        public PayrollStaff PayrollStaff { get; set; }
    }
}

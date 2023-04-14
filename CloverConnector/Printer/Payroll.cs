using System;
using System.Collections.Generic;

namespace RICH_Connector.Printer
{
    public class Payroll
    {
        public string PageWidth { get; set; }
        public string BusinessName { get; set; }
        public string BusinessState { get; set; }
        public string BusinessPhone { get; set; }
        public List<PayrollItem> Staffs { get; set; }
        public List<PayrollItem> Gratuities { get; set; }
        public String Total { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String Period { get; set; }
        public Payroll() { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.Printer
{
    public class Report
    {
        public string PageWidth { get; set; }
        public string TenantName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Cash { get; set; }
        public string CreditCard { get; set; }
        public string ExternalCreditCard { get; set; }
        public string Total { get; set; }
        public string Service { get; set; }
        public string OwnerDiscount { get; set; }
        public string CashGratuity { get; set; }
        public string CreditGratuity { get; set; }
        public string ExternalCreditCardGratuity { get; set; }
    }
}

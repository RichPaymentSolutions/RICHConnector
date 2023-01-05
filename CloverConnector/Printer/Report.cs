using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.Printer
{
    public class Report
    {
        public string PageWidth { get; internal set; }
        public string TenantName { get; internal set; }
        public string StartDate { get; internal set; }
        public string EndDate { get; internal set; }
        public string Cash { get; internal set; }
        public string CreditCard { get; internal set; }
        public string ExternalCreditCard { get; internal set; }
        public string Total { get; internal set; }
        public string Service { get; internal set; }
        public string OwnerDiscount { get; internal set; }
        public string CashGratuity { get; internal set; }
        public string CreditGratuity { get; internal set; }
        public string ExternalCreditCardGratuity { get; internal set; }
    }
}

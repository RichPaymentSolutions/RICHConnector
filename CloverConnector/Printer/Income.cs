using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.Printer
{
    public class Income
    {
        public string PageWidth { get; set; }
        public string BusinessName { get; set; }
        public string Date { get; set; }
        public string Card { get; set; }
        public string ExternalCreditCard { get; set; }
        public string Cash { get; set; }
        public string Check { get; set; }
        public string SubTotal { get; set; }
        public string Tax { get; set; }
        public string TotalGratuity { get; set; }
        public string TotalDiscount { get; set; }
        public string SupplyFee { get; set; }
        public string TotalCashDiscount { get; set; }
        public string TransactionFee { get; set; }
        public string Total { get; set; }
    }
}

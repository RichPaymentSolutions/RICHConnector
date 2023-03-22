using System.Collections.Generic;

namespace RICH_Connector.Printer
{
    public class Transaction
    {
        public string PageWidth { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessState { get; set; }
        public string BusinessPhone { get; set; }
        public string ReceiptNo { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionNo { get; set; }
        public string Tip { get; set; }
        public string Total { get; set; }
        public string SubTotal { get; set; }

        public Transaction() { }

    }
}

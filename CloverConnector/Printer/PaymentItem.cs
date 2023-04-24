using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.Printer
{
    public class PaymentItem
    {
        public string Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Gratuity { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionStatus{ get; set; }
        public string CardLastNumbers { get; set; }

        public PaymentItem() { }
    }
}

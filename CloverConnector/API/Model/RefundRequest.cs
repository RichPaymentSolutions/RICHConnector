using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.API.Model
{
    public class RefundRequest
    {
        public int Amount { get; set; }
        public bool FullRefund { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public bool DisableReceiptSelection { get; set; }
        public bool? DisablePrinting { get; set; }
    }
}

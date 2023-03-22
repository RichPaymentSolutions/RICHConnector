using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.API.Model
{
    public class AddTipRequest
    {
        public int Amount { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
    }
}

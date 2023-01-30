using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.API.Model
{
    public class SaleRequest
    {
        public String Id { get; set; }
        public int Amount { get; set; }
        public int Tax { get; set; }
        public int Tip { get; set; }
        public bool DisablePrinting { get; set; }
        public bool DisableReceiptSelection { get; set; }
        public String TipSettings { get; set; }
        public int TippableAmount { get; set; }
    }
}

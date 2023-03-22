using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RichCloverConnector
{
    public class PrintTransactionRequest
    {
        public String Printer { get; set; }
        public Transaction Transaction { get; set; }
    }
}

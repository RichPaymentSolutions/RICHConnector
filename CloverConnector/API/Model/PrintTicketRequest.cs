using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichCloverConnector
{
    public class PrintTicketRequest
    {
        public String Printer { get; set; }
        public Receipt Ticket { get; set; }
    }
}

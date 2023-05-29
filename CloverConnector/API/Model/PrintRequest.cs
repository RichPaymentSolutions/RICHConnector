using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichCloverConnector.Model
{
    public class PrintRequest
    {
        public String Printer { get; set; }
        public String Base64 { get; set; }
    }


}

using RICH_Connector.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichCloverConnector.Model
{
    public class PrintReportRequest
    {
        public String Printer { get; set; }
        public Report Report { get; set; }
    }


}

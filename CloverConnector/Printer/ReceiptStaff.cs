using System.Collections.Generic;

namespace RICH_Connector.Printer
{
    public class ReceiptStaff
    {
        public string StaffName { get; set; }
        public List<ReceiptItem> Items { get; set; }

        public ReceiptStaff() { }
    }
}

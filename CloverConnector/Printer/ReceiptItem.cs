namespace RICH_Connector.Printer
{
    public class ReceiptItem
    {
        public string ServiceName { get; set; }
        public string ServiceQuantity { get; set; }
        public string ServicePrice { get; set; }
        public string Discount { get; set; }

        public ReceiptItem() { }
    }
}

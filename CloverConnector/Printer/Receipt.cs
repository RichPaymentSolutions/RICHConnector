using System.Collections.Generic;

namespace RICH_Connector.Printer
{
    public class Receipt
    {
        public bool IsTmp { get; set; }
        public bool IsOnPrintedReceipt { get; set; }
        public string PageWidth { get; set; }
        public string BusinessName { get; set; }
        public AddressItem BusinessAddress { get; set; }
        public string BusinessState { get; set; }
        public string BusinessPhone { get; set; }
        public string ReceiptNo { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public string CardLastNumbers { get; set; }
        public List<string> GiftCardIds { get; set; }
        public string TransactionNo { get; set; }
        public string EntryMethod { get; set; }
        public string Discount { get; set; }
        public string SubTotal { get; set; }
        public string Tax { get; set; }
        public string Tip { get; set; }
        public string TransactionFee { get; set; }
        public string Total { get; set; }
        public string CashPaid { get; set; }
        public string ChangeDue { get; set; }
        public string CustomerPoint { get; set; }
        public bool IsPartialPayment { get; set; }
        public List<PaymentItem> Payments { get; set; }
        public List<ReceiptStaff> Staffs { get; set; }

        public Receipt() { }

    }
}

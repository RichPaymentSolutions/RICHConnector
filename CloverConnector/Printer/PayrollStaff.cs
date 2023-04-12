using System.Collections.Generic;

namespace RICH_Connector.Printer
{
    public class PayrollStaff
    {
        public string PageWidth { get; set; }
        public string StaffName { get; set; }
        public string Contracted { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string Ticket { get; set; }
        public string Service { get; set; }
        public string Gratuity { get; set; }
        public string RawGratuity { get; set; }
        public string TipChargePercent { get; set; }
        public string TotalRevenue { get; set; }
        public List<PayrollStaffItem> PayrollStaffItems { get; set; }
        public string OwnerOnly { get; set; }
        public string Owner { get; set; }
        public string Technician { get; set; }
        public string TechnicianOnly { get; set; }
        public string OwnerAndTechnician { get; set; }
        public string CommissionAmount { get; set; }
        public string TipsCollected { get; set; }
        public string Tips { get; set; }
        public string TotalTip { get; set; }
        public string HardSalary { get; set; }
        public string TotalCheckAmount { get; set; }
        public string TotalCashAmount { get; set; }
        public string SupplyFee { get; set; }
        public PayrollStaff() { }
    }
}

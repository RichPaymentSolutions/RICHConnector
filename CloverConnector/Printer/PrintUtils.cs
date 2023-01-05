using CoreHtmlToImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RICH_Connector.Printer
{
    public class PrintUtils
    {

        public static string style = @"<style>body{padding-bottom: 20px;margin:0;width:{page_width}px;font-family:Arial}h5{margin:4px 0;font-size:28px}h6{margin:4px 0;font-size:24px}table{width:100%}div,span,strong{font-size:24px}.text-center{text-align:center}.text-right{text-align:right;float:right}.width-400{width:{page_width}px;}.hr{overflow:hidden;width:{page_width}px;border:1px dashed #000000;margin:4px 0px;}.tip-line{height:80px;}.no-wrap{white-space: nowrap;}</style>";
        public static string receiptTemplate = @"<!DOCTYPE html><html><head>{style}</head><body><h5 class=""text-center"">{business_name}</h5><div class=""text-center"">{business_address}</div><div class=""text-center"">{business_state}</div><div class=""text-center"">{business_phone}</div><br><div class=""subcontent""><div class=""flex"">Receipt No:<strong>{receipt_no}</strong></div><div class=""flex"">Date:<span>{created_date}</span></div><div class=""flex"">Transaction No:<strong>{transaction_no}</strong></div></div><div class=""hr""></div><div class=""subcontent""><h6 class=""width-400 text"">Customer: {customer_name}</h6><h6 class=""width-400"">{payment_method}</h6></div><table>{ticket_items}</table><div class=""hr""></div><table><tr><td><span class=""text"">Discount</span></td><td class=""text-right""><div>{discount}</div></td></tr><tr><td><span class=""text"">Subtotal</span></td><td class=""text-right""><div>{sub_total}</div></td></tr><tr><td><span class=""text"">Taxes</span></td><td class=""text-right""><div>{taxes}</div></td></tr><tr><td><span class=""text"">Tip</span></td><td class=""text-right""><div>{tip}</div></td></tr><tr><td><strong class=""text"">Total</strong></td><td class=""text-right""><strong>{total}</strong></td></tr></table><div class=""tip-line""</div></body></html>";
        public static string receiptTemplateWithTip = @"<!DOCTYPE html><html><head>{style}</head><body><h5 class=""text-center"">{business_name}</h5><div class=""text-center"">{business_address}</div><div class=""text-center"">{business_state}</div><div class=""text-center"">{business_phone}</div><br><div class=""flex"">Receipt No:<strong>{receipt_no}</strong></div><div class=""flex"">Date:<span>{created_date}</span></div><div class=""flex"">Transaction No:<strong>{transaction_no}</strong></div><div class=""hr""></div><h6 class=""width-400 text"">Customer: {customer_name}</h6><h6 class=""width-400"">{payment_method}</h6><table>{ticket_items}</table><div class=""hr""></div><table><tr><td><span class=""text"">Discount</span></td><td class=""text-right""><div>{discount}</div></td></tr><tr><td><span class=""text"">Subtotal</span></td><td class=""text-right""><div>{sub_total}</div></td></tr><tr><td><span class=""text"">Taxes</span></td><td class=""text-right""><div>{taxes}</div></td></tr></table><br/><table><tr><td><span class=""text"">Tip</span></td><td class=""text-right""><div>_________</div></td></tr></table><br/><table><tr><td><strong class=""text"">Total</strong></td><td class=""text-right""><div>_________</div></td></tr></table><br/><br/><br/><br/><div>________________________</div><div class=""text-center"">Signature</div><br/><div class=""text-center"">I agree to pay the above amount per the cardholder and/or merchant agreement</div></body></html>";
        public static string tmpReceiptTemplate = @"<!DOCTYPE html><html><head>{style}</head><body><div class=""flex"">Receipt No:<strong>{receipt_no}</strong></div><div class=""flex"">Date:<span>{created_date}</span></div><div class=""flex"">Transaction No:<strong>{transaction_no}</strong></div><div class=""hr""></div><h6 class=""width-400 text"">Customer: {customer_name}</h6><table>{ticket_items}</table><div class=""tip-line""</div></body></html>";
        public static string ticketTemplate = @"<tr><td colspan=""3""><span class=""width-400"">{staff}</span></td></tr>";
        public static string ticketItemTemplate = @"<tr><td width=""100%""><span class=""text"">{service}</span></td><td><span>{quantity}</span></td><td class=""text-right""><span class=""no-wrap"">{price}</span></td></tr>";

        public static string munbynStyle = @"<style>
            body{padding: 0px 0px;margin:0;width:270px;font-family:Arial;}
            h5{margin:4px 0;font-size:14px}
            h6{font-size:13px; margin:4px 0} 
            table{width:80%; margin: 0 auto; border-collapse: collapse;}
            div,span,strong{font-size:13px; font-weight: 400}
            .text-center{text-align:center}
            .row{}
            .width-400{width:270px;}
            .hr{margin: 0 auto;overflow:hidden;width:80%;border:1px dashed #000000;}
            .no-wrap{white-space: nowrap;}
            .content{width:100%;} 
            .line{border-top: 1px solid; padding-top: 10px} 
            .subcontent{width:80%; margin: 0 auto;} 
            .text-right{float:right; text-align:right}
            .leftBit{margin-left:-4px}</style>";
        public static string payrollTemplate = @"<!DOCTYPE html><html><head>{style}</head><body><h5 class=""text-center"">Technicans Report</h5><h5 class=""text-center"">{business_name}</h5><div class=""text-center""><span>{start_date} - {end_date}</span></div><div class=""content""><table><tr class=""row""><td><h6>Name</h6></td><td><h6>Amount</h6></td><td><h6>Service#</h6></td></tr>{payroll_staffs}<tr class=""line""><td><strong class=""text"">Total</strong></td><td><strong>{total}</strong></td><td></td></tr></table><table></table></div><h5 class=""text-center"">Technicans Gratuity Collected</h5><div class=""content""><table><tr class=""row""><td><h6>Name</h6></td><td><h6>Amount</h6></td><td><h6>Gratuity#</h6></td></tr>{gratuity_staffs}</table></div></body></html>";
        public static string payrollItemsTemplate = @"<tr class=""row""><td><span>{staff_name}</span></td><td><span>{total}</span></td><td><span>{count}</span></td></tr>";
        public static string payrollStaffTemplate = @"<!DOCTYPE html><html><head>{style}</head>
            <body>
                <div class=""subcontent"">
                    <h5 class=""text-center"">Technicans Report</h5>
                    <h6>{StaffName}</h6>
                    <div>Contracted: {Contracted}% </div>
                    <div>{StartDay} - {EndDay}</div>
                    <br/>
                    <div>Ticket: {Ticket}</div>
                    <div>Service: ${Service}</div>
                    <div>Gratuity: ${Gratuity}</div>
                    <div>Total Revenue: ${TotalRevenue}</div>
                </div>
                <table>
                    <tr>
                        <td><h6>Day</h6></td>
                        <td><h6>Service</h6></td>
                        <td><h6>Tip</h6></td>
                    </tr>
                    {PayrollStaffItems}
                </table>
                <div class=""subcontent"">
                    <hr/>
                    <h6>Discounts</h6>
                    <div>Owner & Technician (OE) = ${OwnerAndTechnician}</div>
                    <div>Owner Only (O) = ${OwnerOnly}</div>
                    <div>Technician Only (E) = ${TechnicianOnly}</div>
                    <br/>
                    <h6>{Contracted}% = ${CommissionAmount}</h6>
                    <div>#Tips: ${Tips}</div>
                    <div>Total Tip: ${TotalTip}</div>
                    <br/>
                    <h6>Payroll:</h6>
                    <div>Total Check Amount: ${TotalCheckAmount}</div>
                    <div>Total Bonus Amount: ${TotalBonusAmount}</div>
                </div>
            </body></html>";
        public static string payrollStaffItemTemplate = @"
        <tr>
            <td><span>{Day}</span></td>
            <td><span>{Service}</span></td>
            <td><span>{Tip}</span></td>
        </tr>";
        public static string totalReportTemplate = @"<!DOCTYPE html>
            <html>
                <head>{style}</head>
                <body> 
                    <h5 class=""text-center"">Total Sale Report</h5>
                    <h5 class=""text-center"">{TenantName}</h5>
                    <h6 class=""text-center"">{StartDate} - {EndDate}</h6>
                    <table>
                        <tr>
                            <td><span>Cash</span></td>
                            <td><span>${Cash}</span></td>
                        </tr>
                        <tr>
                            <td><span>Credit Card</span></td>
                            <td><span>${CreditCard}</span></td>
                        </tr>
                        <tr>
                            <td><span>External Credit Card</span></td>
                            <td><span>${ExternalCreditCard}</span></td>
                        </tr>
                        <tr class=""line"">
                            <td><span>Total</span></td>
                            <td><span>${Total}</span></td>
                        </tr>
                    </table>
                    <br/>
                    <table>
                        <tr>
                            <td><span>Service</span></td>
                            <td><span>${Service}</span></td>
                        </tr>
                        <tr>
                            <td><span>Owner Discount</span></td>
                            <td><span class=""leftBit"">-${OwnerDiscount}</span></td>
                        </tr>
                        <tr>
                            <td><span>Cash Gratuity</span></td>
                            <td><span>${CashGratuity}</span></td>
                        </tr>
                        <tr>
                            <td><span>Credit Gratuity</span></td>
                            <td><span>${CreditGratuity}</span></td>
                        </tr>
                        <tr>
                            <td><span>External Credit Card Gratuity</span></td>
                            <td><span>${ExternalCreditCardGratuity}</span></td>
                        </tr>
                    </table>
                </body>
            </html>";
        public static string PrepareReceipt(Receipt receipt, String printer)
        {
            string template = receiptTemplate;
            if (receipt.IsOnPrintedReceipt)
            {
                template = receiptTemplateWithTip;
            }
            string fileName = "receipt.png";

            if (receipt.IsTmp)
            {
                template = tmpReceiptTemplate;
                fileName = "tmp_receipt.png";
            }
            string pageWidth = receipt.PageWidth == null || receipt.PageWidth.Equals("") ? "400" : receipt.PageWidth;
            string html = BuildReceipt(template, pageWidth, printer);
            html = BindingGeneralInfo(html, receipt);
            html = BindingTicket(html, receipt);

            var converter = new HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, width: int.Parse(pageWidth));
            File.WriteAllBytes(fileName, bytes);

            if (printer == "munbyn")
            {
                fileName = "receipt";
                File.WriteAllText(fileName + ".html", html);
            }
            return fileName;
        }

        static string BuildReceipt(string html, string pageWidth, string printer)
        {
            return html
                .Replace("{style}", printer == "munbyn" ? munbynStyle : style)
                .Replace("{page_width}", pageWidth);
        }

        static string BindingGeneralInfo(string html, Receipt receipt)
        {
            if (receipt == null) return html;

            return html
                .Replace("{business_name}", receipt.BusinessName)
                .Replace("{business_address}", receipt.BusinessAddress)
                .Replace("{business_state}", receipt.BusinessState)
                .Replace("{business_phone}", receipt.BusinessPhone)
                .Replace("{receipt_no}", receipt.ReceiptNo)
                .Replace("{created_date}", receipt.CreatedDate)
                .Replace("{customer_name}", receipt.CustomerName)
                .Replace("{payment_method}", receipt.PaymentMethod)
                .Replace("{transaction_no}", receipt.TransactionNo)
                .Replace("{discount}", "$" + receipt.Discount)
                .Replace("{sub_total}", "$" + receipt.SubTotal)
                .Replace("{taxes}", "$" + receipt.Tax)
                .Replace("{tip}", "$" + receipt.Tip)
                .Replace("{total}", "$" + receipt.Total);
        }

        static string BindingTicket(string html, Receipt receipt)
        {
            if (receipt == null) return html;

            string output = "";
            List<ReceiptStaff> staffs = receipt.Staffs;
            foreach (ReceiptStaff receiptStaff in staffs)
            {
                output += ticketTemplate.Replace("{staff}", receiptStaff.StaffName);

                foreach (ReceiptItem item in receiptStaff.Items)
                {
                    output += ticketItemTemplate
                        .Replace("{service}", item.ServiceName)
                        .Replace("{quantity}", "x" + item.ServiceQuantity)
                        .Replace("{price}", "$" + item.ServicePrice);

                    var discount = item.Discount;
                    if (discount != null && discount != "")
                    {
                        output += ticketItemTemplate
                           .Replace("{service}", "")
                           .Replace("{quantity}", "")
                           .Replace("{price}", "Discount " + discount + "%");
                    }
                }
            }

            return html.Replace("{ticket_items}", output);
        }

        static string BindingPayroll(string html, Payroll payroll)
        {
            if (payroll == null) return html;

            string output = "";
            string gratuity_output = "";
            List<PayrollItem> staffs = payroll.Staffs;
            foreach (PayrollItem payrollStaff in staffs)
            {
                output += payrollItemsTemplate.Replace("{staff_name}", payrollStaff.StaffName)
                                                .Replace("{total}", "$" + payrollStaff.Total)
                                                .Replace("{count}", payrollStaff.Number);
            }
            List<PayrollItem> gratuities = payroll.Gratuities;
            foreach (PayrollItem payrollStaff in gratuities)
            {
                gratuity_output += payrollItemsTemplate.Replace("{staff_name}", payrollStaff.StaffName)
                                                .Replace("{total}", "$" + payrollStaff.Total)
                                                .Replace("{count}", payrollStaff.Number);
            }

            return html.Replace("{payroll_staffs}", output)
                       .Replace("{gratuity_staffs}", gratuity_output)
                       .Replace("{business_name}", payroll.BusinessName)
                       .Replace("{start_date}", payroll.StartDate)
                       .Replace("{end_date}", payroll.EndDate)
                       .Replace("{period}", payroll.Period)
                       .Replace("{total}", "$" + payroll.Total);
        }

        public static string PreparePayroll(Payroll payroll, String printer)
        {
            string template = payrollTemplate;
            string fileName = "payroll.png";

            string pageWidth = payroll.PageWidth == null || payroll.PageWidth.Equals("") ? "400" : payroll.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingPayroll(html, payroll);

            var converter = new HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, width: int.Parse(pageWidth));
            File.WriteAllBytes(fileName, bytes);

            if (printer == "munbyn")
            {
                fileName = "payroll";
                File.WriteAllText(fileName + ".html", html);
            }
            return fileName;
        }

        static string BindingPayrollStaff(string html, PayrollStaff payroll)
        {
            if (payroll == null) return html;
            string output = "";

            List<PayrollStaffItem> staffs = payroll.PayrollStaffItems;
            foreach (PayrollStaffItem payrollStaff in staffs)
            {
                output += payrollStaffItemTemplate.Replace("{Day}", payrollStaff.Day)
                                                .Replace("{Service}", "$" + payrollStaff.Service)
                                                .Replace("{Tip}", "$" + payrollStaff.Tip);
            }
            return html.Replace("{Contracted}", payroll.Contracted)
                        .Replace("{StaffName}", payroll.StaffName)
                        .Replace("{StartDay}", payroll.StartDay)
                        .Replace("{EndDay}", payroll.EndDay)
                        .Replace("{Ticket}", payroll.Ticket)
                        .Replace("{Service}", payroll.Service)
                        .Replace("{TotalRevenue}", payroll.TotalRevenue)
                        .Replace("{Gratuity}", payroll.Gratuity)
                        .Replace("{CommissionAmount}", payroll.CommissionAmount)
                        .Replace("{OwnerAndTechnician}", payroll.OwnerAndTechnician)
                        .Replace("{OwnerOnly}", payroll.OwnerOnly)
                        .Replace("{TechnicianOnly}", payroll.TechnicianOnly)
                        .Replace("{PayrollStaffItems}", output)
                        .Replace("{TipsCollected}", payroll.TipsCollected)
                        .Replace("{Tips}", payroll.Tips)
                        .Replace("{TotalTip}", payroll.TotalTip)
                        .Replace("{TotalCheckAmount}", payroll.TotalCheckAmount)
                        .Replace("{TotalBonusAmount}", payroll.TotalBonusAmount);
        }

        public static string PreparePayrollStaff(PayrollStaff payroll, String printer)
        {
            string template = payrollStaffTemplate;
            string fileName = "payroll_staff.png";

            string pageWidth = payroll.PageWidth == null || payroll.PageWidth.Equals("") ? "400" : payroll.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingPayrollStaff(html, payroll);

            var converter = new HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, width: int.Parse(pageWidth));
            File.WriteAllBytes(fileName, bytes);
            if (printer == "munbyn")
            {
                fileName = "payroll_staff";
                File.WriteAllText(fileName + ".html", html);
            }
            return fileName;
        }

        static string BindingTotalReport(string html, Report report)
        {
            if (report == null) return html;

            return html.Replace("{TenantName}", report.TenantName)
                        .Replace("{StartDate}", report.StartDate)
                        .Replace("{EndDate}", report.EndDate)
                        .Replace("{Cash}", report.Cash)
                        .Replace("{CreditCard}", report.CreditCard)
                        .Replace("{ExternalCreditCard}", report.ExternalCreditCard)
                        .Replace("{Total}", report.Total)
                        .Replace("{Service}", report.Service)
                        .Replace("{OwnerDiscount}", report.OwnerDiscount)
                        .Replace("{CashGratuity}", report.CashGratuity)
                        .Replace("{CreditGratuity}", report.CreditGratuity)
                        .Replace("{ExternalCreditCardGratuity}", report.ExternalCreditCardGratuity);
        }

        public static string PrepareTotalReport(Report report, String printer)
        {
            string template = totalReportTemplate;
            string fileName = "totalReport.png";

            string pageWidth = report.PageWidth == null || report.PageWidth.Equals("") ? "400" : report.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingTotalReport(html, report);

            var converter = new HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, width: int.Parse(pageWidth));
            //var bytes = PdfSharpConvert(html);
            File.WriteAllBytes(fileName, bytes);
            if (printer == "munbyn")
            {
                fileName = "totalReport";
                File.WriteAllText(fileName + ".html", html);
            }

            return fileName;
        }

        static string BuildPayroll(string html, string pageWidth, string printer)
        {
            return html
                .Replace("{style}", printer == "munbyn" ? munbynStyle : style)
                .Replace("{page_width}", pageWidth);
        }

    }
}

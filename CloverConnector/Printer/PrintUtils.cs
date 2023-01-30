using System;
using System.Collections.Generic;
using System.IO;

namespace RICH_Connector.Printer
{
    public class PrintUtils
    {
        public PrintUtils () { }
        public string style = @"<style>
            body{padding-bottom: 20px;margin:0;width:{page_width}px;font-family:Arial}
            h5{margin:4px 0;font-size:28px}
            h6{margin:4px 0;font-size:24px}
            table{width:80%; margin: 0 auto; border-collapse: collapse;}
            div,span,strong{font-size:24px}
            .text-center{text-align:center}
            .text-right{text-align:right;float:right}
            .width-400{width:{page_width}px;}
            .hr{overflow:hidden;width:{page_width}px;border:1px dashed #000000;margin:4px 0px;}
            .tip-line{height:80px;}
            .no-wrap{white-space: nowrap;}
            .black-box{background:black; color:white;}
            .subcontent{width:80%; margin: 0 auto;}
            </style>";
        public string receiptTemplate = @"<!DOCTYPE html><html>
<head>{style}</head>
<body>
<h5 class=""text-center"">{business_name}</h5>
<div class=""text-center"">{business_phone}</div><br>
<div class=""subcontent"">
<div class=""flex"">Receipt No:<strong>{receipt_no}</strong></div>
<div class=""flex"">Date:<span>{created_date}</span></div>
<div class=""flex"">Transaction No:<strong>{transaction_no}</strong></div>
</div>
<div class=""hr"">
</div>
<div class=""subcontent"">
<h6>Customer: {customer_name}</h6>
<h6>{payment_method}</h6></div>
<table>{ticket_items}</table>
<div class=""hr""></div><table><tr><td><span class=""text"">Discount</span></td><td class=""text-right""><div>{discount}</div></td></tr><tr><td><span class=""text"">Subtotal</span></td><td class=""text-right""><div>{sub_total}</div></td></tr><tr><td><span class=""text"">Taxes</span></td><td class=""text-right""><div>{taxes}</div></td></tr><tr><td><span class=""text"">Tip</span></td><td class=""text-right""><div>{tip}</div></td></tr><tr><td><strong class=""text"">Total</strong></td><td class=""text-right""><strong>{total}</strong></td></tr></table><div class=""tip-line""</div></body></html>";
        public string receiptTemplateWithTip = @"<!DOCTYPE html><html><head>{style}</head><body>
<h5 class=""text-center"">{business_name}</h5>
<div class=""text-center"">{business_phone}</div>
<br>
<div class=""subcontent"">
<div>Receipt No:<strong>{receipt_no}</strong></div>
<div>Date:<span>{created_date}</span></div>
<div>Transaction No:<strong>{transaction_no}</strong></div></div>
<div class=""hr"">
</div>
<div class=""subcontent"">
<h6>Customer: {customer_name}</h6>
<h6>{payment_method}</h6></div><table>{ticket_items}</table><div class=""hr""></div><table><tr><td><span class=""text"">Discount</span></td><td class=""text-right""><div>{discount}</div></td></tr><tr><td><span class=""text"">Subtotal</span></td><td class=""text-right""><div>{sub_total}</div></td></tr><tr><td><span class=""text"">Taxes</span></td><td class=""text-right""><div>{taxes}</div></td></tr></table><br/><table><tr><td><span class=""text"">Tip</span></td><td class=""text-right""><div>_________</div></td></tr></table><br/><table><tr><td><strong class=""text"">Total</strong></td><td class=""text-right""><div>_________</div></td></tr></table><br/><br/><br/><br/><div>_______________________________________</div><div class=""text-center"">Signature</div><br/><div class=""text-center"">I agree to pay the above amount per the cardholder and/or merchant agreement</div></body></html>";
        public string ticketTemplate = @"<tr><td colspan=""3""><span class=""width-400"">{staff}</span></td></tr>";
        public string ticketItemTemplate = @"<tr><td width=""100%""><span class=""text"">{service}</span></td><td><span>{quantity}</span></td><td class=""text-right""><span class=""no-wrap"">&nbsp &nbsp &nbsp {price}</span></td></tr>";

        public string munbynStyle = @"<style>
            body{padding: 0px 0px;margin:0;width:270px;font-family:Arial;}
            h5{margin:4px 0;font-size:14px}
            h6{font-size:13px; margin:4px 0} 
            table{width:80%; margin: 0 auto; border-collapse: collapse;}
            div,span,strong, b{font-size:13px;}
            .text-center{text-align:center}
            .row{}
            .width-400{width:270px;}
            .hr{margin: 0 auto;overflow:hidden;width:80%;border:1px dashed #000000;}
            .no-wrap{white-space: nowrap;}
            .content{width:100%;} 
            .line{border-top: 1px solid; padding-top: 10px} 
            .subcontent{width:80%; margin: 0 auto;} 
            .text-right{float:right; text-align:right}
            .leftBit{margin-left:-4px}
            .black-box{background:black; color:white}</style>";
        public string payrollTemplate = @"<!DOCTYPE html><html><head>{style}</head><body><h5 class=""text-center"">Technicans Report</h5><h5 class=""text-center"">{business_name}</h5><div class=""text-center""><span>{start_date} - {end_date}</span></div><div class=""content""><table><tr class=""row""><td><h6>Name</h6></td><td><h6>Amount</h6></td><td><h6>Service#</h6></td></tr>{payroll_staffs}<tr class=""line""><td><strong class=""text"">Total</strong></td><td><strong>{total}</strong></td><td></td></tr></table><table></table></div><br/><h5 class=""text-center"">Technicans Gratuity Collected</h5><br/><div class=""content""><table><tr class=""row""><td><h6>Name</h6></td><td><h6>Amount</h6></td><td><h6>Gratuity#</h6></td></tr>{gratuity_staffs}</table></div></body></html>";
        public string payrollItemsTemplate = @"<tr class=""row""><td><span>{staff_name}</span></td><td><span>{total}</span></td><td><span>{count}</span></td></tr>";
        public string payrollStaffTemplate = @"<!DOCTYPE html><html><head>{style}</head>
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
                    <div>Owner = ${Owner}</div>
                    <div>Technician = ${Technician}</div>
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
        public string payrollStaffItemTemplate = @"
        <tr>
            <td><span>{Day}</span></td>
            <td><span>{Service}</span></td>
            <td><span>{Tip}</span></td>
        </tr>";
        public string totalReportTemplate = @"<!DOCTYPE html>
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
        public string tmpReceiptTemplate = @"<!DOCTYPE html><html><head>{style}</head>
            <body>
       
                <div class=""subcontent"">
                    <h5 class=""text-center""><div class=""black-box"">{receipt_no}</div></h5>
                    <h6 class=""text-center"">{business_name}</h6>
                   
                    <div class=""text-center"">{business_phone}</div>
                    <div class=""text-center"">{created_date}</div>

                    <br/>
                </div>
               
                <table>
                    {ticket_items}
                    <tr>
                        <td><span></span></td>
                        <td><span>TOTAL</span></td>
                        <td><span>{total}</span></td>
                    </tr>
                </table>
                
                <br/>   
              
                <div class=""text-center"">Tip Amount: $_________</div>
                <br/>           
                <div class=""subcontent"">
                    <div class=""text-center"">
                    <p><b class=""text-center"">Service received and accepted. Agree to pay. Non refundable.</b></p>
                    <p><b class=""text-center"">Thank you</b></p>
                    </div>
                </div>
              
            </body>";
        public string tmpReceiptItemsTemplate = @"
        <tr>
            <td><span>{no} &nbsp</span></td>
            <td><span>{service}</span></td>
            <td><span>{price}</span></td>
        </tr>";
        public string incomeReportTemplate = @"<!DOCTYPE html>
            <html>
                <head>{style}</head>
                <body> 
                    <h5 class=""text-center"">Income Review</h5>
                    <h5 class=""text-center"">{BusinessName}</h5>
                    <h6 class=""text-center"">{Date}</h6>
                    <br/>
                    <table>
                        <tr>
                            <td><span>Card</span></td>
                            <td><span>${Card}</span></td>
                        </tr>
                        <tr>
                            <td><span>External Credit Card</span></td>
                            <td><span>${ExternalCreditCard}</span></td>
                        </tr>
                        <tr>
                            <td><span>Cash</span></td>
                            <td><span>${Cash}</span></td>
                        </tr>
                        <tr>
                            <td><span>Check</span></td>
                            <td><span>${Check}</span></td>
                        </tr>
                        <tr class=""line"">
                            <td><span>Sub Total</span></td>
                            <td><span>${SubTotal}</span></td>
                        </tr>                     
                        <tr>
                            <td><span>Tax</span></td>
                            <td><span>${Tax}</span></td>
                        </tr>
                        <tr>
                            <td><span>Total Gratuity</span></td>
                            <td><span>${TotalGratuity}</span></td>
                        </tr>
                        <tr>
                            <td><span>Total Discount</span></td>
                            <td><span>${TotalDiscount}</span></td>
                        </tr>
                        <tr>
                            <td><span>Total Redeem</span></td>
                            <td><span>${TotalRedeem}</span></td>
                        </tr>
                        <tr>
                            <td><span>Total Cash Discount</span></td>
                            <td><span>${TotalCashDiscount}</span></td>
                        </tr>
                        <tr>
                            <td><span>Transaction Fee</span></td>
                            <td><span>${TransactionFee}</span></td>
                        </tr>
                        <tr class=""line"">
                            <td><p>Total</p></td>
                            <td><p>${Total}</p></td>
                        </tr>
                                    
                    </table>
                </body>
            </html>";
        public string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH\\print.png";
        public string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RICH";
        public string PrepareReceiptAsync(Receipt receipt, String printer)
        {
            string template = receiptTemplate;
            System.IO.Directory.CreateDirectory(folder);
            if (receipt.IsOnPrintedReceipt)
            {
                template = receiptTemplateWithTip;
            }
            
            if (receipt.IsTmp)
            {
                template = tmpReceiptTemplate;       
            }
            string pageWidth = receipt.PageWidth == null || receipt.PageWidth.Equals("") ? "400" : receipt.PageWidth;
            string html = BuildReceipt(template, pageWidth, printer);
            html = BindingGeneralInfo(html, receipt);
            html = BindingTicket(html, receipt);

            var converter = new CoreHtmlToImage.HtmlConverter() { };
            var bytes = converter.FromHtmlString(html, 270);
            File.WriteAllBytes(fileName, bytes);

       
            return html;
        }

        string BuildReceipt(string html, string pageWidth, string printer)
        {
            return html
                .Replace("{style}", printer == "munbyn" ? munbynStyle : style)
                .Replace("{page_width}", pageWidth);
        }

        string BindingGeneralInfo(string html, Receipt receipt)
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

        string BindingTicket(string html, Receipt receipt)
        {
            if (receipt == null) return html;

            string output = "";
            int count = 1;
            if (receipt.IsTmp) {
                List<ReceiptStaff> staffs = receipt.Staffs;
                foreach (ReceiptStaff receiptStaff in staffs)
                {
                    foreach (ReceiptItem item in receiptStaff.Items)
                    {
                        output += tmpReceiptItemsTemplate
                            .Replace("{no}", count++.ToString())
                            .Replace("{service}", item.ServiceName)
                            .Replace("{quantity}", "x" + item.ServiceQuantity)
                            .Replace("{price}", "$" + item.ServicePrice);
                    }
                }
            }
            else
            {
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
                        if (discount != null && discount != "" && discount != "0" && discount != "0.00")
                        {
                            output += ticketItemTemplate
                               .Replace("{service}", "")
                               .Replace("{quantity}", "")
                               .Replace("{price}", "Discount " + discount + "%");
                        }
                    }
                }
            }
            return html.Replace("{ticket_items}", output);
        }

        string BindingPayroll(string html, Payroll payroll)
        {
            if (payroll == null) return html;

            string output = "";
            string gratuity_output = "";
            List<PayrollItem> staffs = payroll.Staffs;
            foreach (PayrollItem payrollStaff in staffs)
            {
                output += payrollItemsTemplate.Replace("{staff_name}", payrollStaff.StaffName)
                                                .Replace("{total}", "$" + String.Format("{0:N2}", float.Parse(payrollStaff.Total)))
                                                .Replace("{count}", payrollStaff.Number);
            }
            List<PayrollItem> gratuities = payroll.Gratuities;
            foreach (PayrollItem payrollStaff in gratuities)
            {
                gratuity_output += payrollItemsTemplate.Replace("{staff_name}", payrollStaff.StaffName)
                                                .Replace("{total}", "$" + String.Format("{0:N2}", float.Parse(payrollStaff.Total)))
                                                .Replace("{count}", payrollStaff.Number);
            }

            return html.Replace("{payroll_staffs}", output)
                       .Replace("{gratuity_staffs}", gratuity_output)
                       .Replace("{business_name}", payroll.BusinessName)
                       .Replace("{start_date}", payroll.StartDate)
                       .Replace("{end_date}", payroll.EndDate)
                       .Replace("{period}", payroll.Period)
                       .Replace("{total}", "$" + String.Format("{0:N2}", float.Parse(payroll.Total)));
        }

        public string PreparePayroll(Payroll payroll, String printer)
        {
            string template = payrollTemplate;
            System.IO.Directory.CreateDirectory(folder);
            string pageWidth = payroll.PageWidth == null || payroll.PageWidth.Equals("") ? "400" : payroll.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);
            html = BindingPayroll(html, payroll);

            var converter = new CoreHtmlToImage.HtmlConverter();
            var bytes = converter.FromHtmlString(html, 270);
           
            File.WriteAllBytes(fileName, bytes);
            
            return html;
        }

        string BindingPayrollStaff(string html, PayrollStaff payroll)
        {
            if (payroll == null) return html;
            string output = "";

            List<PayrollStaffItem> staffs = payroll.PayrollStaffItems;
            foreach (PayrollStaffItem payrollStaff in staffs)
            {
                output += payrollStaffItemTemplate.Replace("{Day}", payrollStaff.Day)
                                                .Replace("{Service}", "$" + String.Format("{0:N2}", float.Parse(payrollStaff.Service)))
                                                .Replace("{Tip}", "$" + String.Format("{0:N2}", float.Parse(payrollStaff.Tip)));
            }
            return html.Replace("{Contracted}", payroll.Contracted)
                        .Replace("{StaffName}", payroll.StaffName)
                        .Replace("{StartDay}", payroll.StartDay)
                        .Replace("{EndDay}", payroll.EndDay)
                        .Replace("{Ticket}", payroll.Ticket)
                        .Replace("{Service}", String.Format("{0:N2}", float.Parse(payroll.Service)))
                        .Replace("{TotalRevenue}", String.Format("{0:N2}", float.Parse(payroll.TotalRevenue)))
                        .Replace("{Gratuity}", String.Format("{0:N2}", float.Parse(payroll.Gratuity)))
                        .Replace("{CommissionAmount}", String.Format("{0:N2}", float.Parse(payroll.CommissionAmount)))
                        .Replace("{OwnerAndTechnician}", String.Format("{0:N2}", float.Parse(payroll.OwnerAndTechnician)))
                        .Replace("{Owner}", String.Format("{0:N2}", float.Parse(payroll.Owner)))
                        .Replace("{Technician}", String.Format("{0:N2}", float.Parse(payroll.Technician)))
                        .Replace("{OwnerOnly}", String.Format("{0:N2}", float.Parse(payroll.OwnerOnly)))
                        .Replace("{TechnicianOnly}", String.Format("{0:N2}", float.Parse(payroll.TechnicianOnly)))
                        .Replace("{PayrollStaffItems}", output)
                        .Replace("{TipsCollected}", payroll.TipsCollected)
                        .Replace("{Tips}", payroll.Tips)
                        .Replace("{TotalTip}", String.Format("{0:N2}", float.Parse(payroll.TotalTip)))
                        .Replace("{TotalCheckAmount}", String.Format("{0:N2}", float.Parse(payroll.TotalCheckAmount)))
                        .Replace("{TotalBonusAmount}", String.Format("{0:N2}", float.Parse(payroll.TotalBonusAmount)));
        }

        public string PreparePayrollStaff(PayrollStaff payroll, String printer)
        {
            string template = payrollStaffTemplate;
            System.IO.Directory.CreateDirectory(folder);
            string pageWidth = payroll.PageWidth == null || payroll.PageWidth.Equals("") ? "400" : payroll.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingPayrollStaff(html, payroll);

            var converter = new CoreHtmlToImage.HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, 270);
            File.WriteAllBytes(fileName, bytes);
           
            return html;
        }

        string BindingTotalReport(string html, Report report)
        {
            if (report == null) return html;

            return html.Replace("{TenantName}", report.TenantName)
                        .Replace("{StartDate}", report.StartDate)
                        .Replace("{EndDate}", report.EndDate)
                        .Replace("{Cash}", String.Format("{0:N2}", float.Parse(report.Cash)))
                        .Replace("{CreditCard}", String.Format("{0:N2}", float.Parse(report.CreditCard)))
                        .Replace("{ExternalCreditCard}", String.Format("{0:N2}", float.Parse(report.ExternalCreditCard)))
                        .Replace("{Total}", String.Format("{0:N2}", float.Parse(report.Total)))
                        .Replace("{Service}", String.Format("{0:N2}", float.Parse(report.Service)))
                        .Replace("{OwnerDiscount}", String.Format("{0:N2}", float.Parse(report.OwnerDiscount)))
                        .Replace("{CashGratuity}", String.Format("{0:N2}", float.Parse(report.CashGratuity)))
                        .Replace("{CreditGratuity}", String.Format("{0:N2}", float.Parse(report.CreditGratuity)))
                        .Replace("{ExternalCreditCardGratuity}", String.Format("{0:N2}", float.Parse(report.ExternalCreditCardGratuity)));
        }

        public string PrepareTotalReport(Report report, String printer)
        {
            string template = totalReportTemplate;
            System.IO.Directory.CreateDirectory(folder);
            string pageWidth = report.PageWidth == null || report.PageWidth.Equals("") ? "400" : report.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingTotalReport(html, report);

            var converter = new CoreHtmlToImage.HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, 270);
            //var bytes = PdfSharpConvert(html);

            File.WriteAllBytes(fileName, bytes);

            return html;
        }

        string BindingIncomeReport(string html, Income report)
        {
            if (report == null) return html;

            return html.Replace("{Card}", String.Format("{0:N2}", float.Parse(report.Card)))
                        .Replace("{ExternalCreditCard}", String.Format("{0:N2}", float.Parse(report.ExternalCreditCard)))
                        .Replace("{Cash}", String.Format("{0:N2}", float.Parse(report.Cash)))
                        .Replace("{Check}", String.Format("{0:N2}", float.Parse(report.Check)))
                        .Replace("{SubTotal}", String.Format("{0:N2}", float.Parse(report.SubTotal)))
                        .Replace("{Tax}", String.Format("{0:N2}", float.Parse(report.Tax)))
                        .Replace("{TotalGratuity}", String.Format("{0:N2}", float.Parse(report.TotalGratuity)))
                        .Replace("{TotalDiscount}", String.Format("{0:N2}", float.Parse(report.TotalDiscount)))
                        .Replace("{TotalRedeem}", String.Format("{0:N2}", float.Parse(report.TotalRedeem)))
                        .Replace("{TotalCashDiscount}", String.Format("{0:N2}", float.Parse(report.TotalCashDiscount)))
                        .Replace("{TransactionFee}", String.Format("{0:N2}", float.Parse(report.TransactionFee)))
                        .Replace("{Total}", String.Format("{0:N2}", float.Parse(report.Total)))
                        .Replace("{BusinessName}", report.BusinessName)
                        .Replace("{Date}", report.Date);
        }

        public string PrepareIncomeReport(Income report, String printer)
        {
            string template = incomeReportTemplate;
            System.IO.Directory.CreateDirectory(folder);
            string pageWidth = report.PageWidth == null || report.PageWidth.Equals("") ? "400" : report.PageWidth;
            string html = BuildPayroll(template, pageWidth, printer);

            html = BindingIncomeReport(html, report);

            var converter = new CoreHtmlToImage.HtmlConverter() { };

            var bytes = converter.FromHtmlString(html, 270);
            //var bytes = PdfSharpConvert(html);

            File.WriteAllBytes(fileName, bytes);

            return html;
        }

        string BuildPayroll(string html, string pageWidth, string printer)
        {
            return html
                .Replace("{style}", printer == "munbyn" ? munbynStyle : style)
                .Replace("{page_width}", pageWidth);
        }

    }
}

using s3827202_s3687609_a2.Areas.Banking.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Common
{
    public class SendEmail
    {
        public async Task<bool> Sendemail(EmailTemp emailTemp)
        {
            //get email api
            var client = new SendGridClient("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");
            var from = new EmailAddress("wsx283848@qq.com", "Example User");
            var subject = "Recent Transactions";
            var to = new EmailAddress(emailTemp.receiveEmail, emailTemp.receiveName);
            var plainTextContent = "";
            //get email format
            var htmlContent = GetHtmlContent(emailTemp);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }

            return false;
        }
        public class EmailTemp
        {
            public string receiveEmail { get; set; }
            public string receiveName { get; set; }
            public List<Transaction> transactions { get; set; }
            public decimal balance { get; set; }
            public decimal prebalance { get; set; }
            public string accountID { get; set; }
            public string customerAddress { get; set; }
        }
        public string GetHtmlContent(EmailTemp emailTemp)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            if (emailTemp.balance == -1)
            {
                stringBuilder.Append("My Balance:  <br/>");
                stringBuilder.Append(" <br/>");
            }
            else
            {
                stringBuilder.Append("My Balance:" + emailTemp.balance + "   <br/>");
                stringBuilder.Append(" <br/>");
            }            
            stringBuilder.Append("Receive Email:" + emailTemp.receiveEmail + "   <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append("Receive Name:" + emailTemp.receiveName + "   <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append("Account ID:" + emailTemp.accountID + "   <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append("Customer Address:" + emailTemp.customerAddress + "   <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append(" <br/>");
            stringBuilder.Append("<table><tr><th>AccountNumber</th><th>DestAccount</th><th>Amount</th><th>ModifyDate</th><th>Comment</th><th>TransactionType</th></tr>");
            foreach (var item in emailTemp.transactions)
            {
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td>" + item.AccountNumber+"</td>");
                stringBuilder.Append("<td>" + item.DestAccount + "</td>");
                stringBuilder.Append("<td>" + item.Amount + "</td>");
                stringBuilder.Append("<td>" + item.ModifyDate + "</td>");
                stringBuilder.Append("<td>" + item.Comment + "</td>");
                stringBuilder.Append("<td>" + item.TransactionType + "</td>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }
    }
}
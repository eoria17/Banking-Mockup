using s3827202_s3687609_a2.Areas.Banking.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3827202_s3687609_a2.Common
{
    public class SendEmail
    {
        public async Task<bool> Sendemail(EmailTemp emailTemp)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("wsx283848@qq.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("wsx283848@gmail.com", emailTemp.receiveName);
            var plainTextContent = "and easy to do anywhere, even with C#";
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
            public string accountID { get; set; }
            public string customerAddress { get; set; }
        }
        public string GetHtmlContent(EmailTemp emailTemp)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("My Balance:" + emailTemp.balance+ "   <br/>");
            stringBuilder.Append("receiveEmail:" + emailTemp.receiveEmail + "   <br/>");
            stringBuilder.Append("receiveName:" + emailTemp.receiveName + "   <br/>");
            stringBuilder.Append("accountID:" + emailTemp.accountID + "   <br/>");
            stringBuilder.Append("customerAddress:" + emailTemp.customerAddress + "   <br/>");
            foreach (var item in emailTemp.transactions)
            {
                stringBuilder.Append("AccountNumber:" + item.AccountNumber + "   <br/>");
                stringBuilder.Append("DestAccount:" + item.DestAccount + "   <br/>");
                stringBuilder.Append("Amount:" + item.Amount + "   <br/>");
                stringBuilder.Append("ModifyDate:" + item.ModifyDate + "   <br/>");
                stringBuilder.Append("Comment:" + item.Comment + "   <br/>");                
            }
            return stringBuilder.ToString();
        }
    }
}
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace s3827202_s3687609_a2.Common
{
    public class SendEmail
    {
        public async Task<bool> Sendemail(EmailTemp emailTemp)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("wsx283848@qq.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(emailTemp.receiveEmail, emailTemp.receiveName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent ="111111111111";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);         
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }

            return false;
        }
        public class EmailTemp { 
            public string receiveEmail { get; set; }
            public string receiveName { get; set; }
            public List<Transaction> transactions { get; set; }
        }
    }
}

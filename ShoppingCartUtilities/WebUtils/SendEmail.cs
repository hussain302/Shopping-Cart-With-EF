using System.Net.Mail;
using System.Net;
using System.Text;

namespace ShoppingCartUtilities.WebUtils
{
    public static class SendEmail
    {
        public static readonly string EmailTemp = "<html>\r\n  <body>\r\n    <p><strong>Order Confirmation - Order Id: <i>{Order Id}</i></strong> </p>\r\n    <br>\r\n    <p>Dear {Customer Name},</p>\r\n    <p>Thank you for placing an order with us. We have received your order and it is being processed. Please find the details of your order below:</p>\r\n <b> Product Names:</b>  {Product Rows}\r\n <strong><br>Total:</strong>Rs: {Total}\r\n  <br>\r\n    <p><strong>Shipping Address:</strong></p>\r\n    <p>{Customer Name}</p>\r\n    <p>{Street}</p>\r\n    <p>{City}, {Province} {Postal Code}</p>\r\n    <p>{Country}</p>\r\n    <br>\r\n    <p>Your order is currently in <strong>{Order Status}</strong> status. We will notify you by email once your order has been shipped. If you have any questions or concerns, please do not hesitate to contact us at {Customer Support Email}.</p>\r\n    <br>\r\n      <p>Best regards,</p>\r\n    <p>M. Hussain<br>CEO, Brand Matrix</p>  </body>\r\n</html>\r\n";
       
        public static async Task<bool> PostAnEmail(string senderEmail, string senderEmailKey, string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderEmailKey),
                    EnableSsl = true
                };
                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };
                message.To.Add(to);
                message.To.Add(senderEmail);
                // Add attachments
                // message.Attachments.Add(new Attachment("path/to/file.pdf"));
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

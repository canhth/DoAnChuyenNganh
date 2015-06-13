using System;
using HtmlAgilityPack;

using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using TrackingDefaceDAO;
using TrackingDefaceDTO;
namespace TrackingDefaceBUS
{
    public class EmailBUS
    {
        private EmailDAO objectDAO = new EmailDAO();
        public void SendEmail(Web web, string subjects)
        {
            Email email = objectDAO.GetEmailByisHost(true);
            
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = Int32.Parse(email.port);
            client.Host = email.host;
            client.EnableSsl = email.enableSSL;
            client.Timeout = email.timeSend;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(email.email, email.passWords);

            MailMessage mm = new MailMessage(email.email, web.emails);   
            mm.Subject = "Cảnh báo về trạng thái Website của bạn.";
     
            string body = "Xin chào ban quản trị" + web.nameSite + ",";
            body += "<br/> Chúng tôi vừa phát hiện được sự thay đổi bất thường về nội dung WebSite của bạn vào lúc "+ DateTime.Now.ToString();
            body += "<br/> Website của bạn bị tấn công bằng cách ";
            body += "<br/> Vui lòng kiểm tra lại website của bạn.";
            body += "<br /><br />";
            body += "<br /><center><b><u> </center></u></b>";
            body += "<br /><br />Cảm ơn";
            mm.IsBodyHtml = true;
            mm.Body = body;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

        }
    }
}

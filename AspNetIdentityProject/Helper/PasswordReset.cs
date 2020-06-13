using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetIdentityProject.Helper
{
    public static  class PasswordReset
    {
       
       
        public static void PasswordResetSendEmail(string link,string emailAddress)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("jimmahmet05@gmail.com");
            mail.To.Add(emailAddress);

            mail.Subject = "Şifre Sıfırlama ";
            mail.Body = "<h2> Şifrenizi yenilemek için lütfen aşağıdaki linke tıklayınız.</h2>";
            mail.Body += $" <a href='{link}'> Şifre yenileme linki";
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("jimmahmet05@gmail.com", "343ahmet343");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
    
}

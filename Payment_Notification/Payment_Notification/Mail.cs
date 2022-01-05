// Нужны данные:

// Адрес получателя
// Дата отправки
// Имя 
// Сообщение
// Пример сообщения

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Payment_Notification
{

    class Mail
    {
        public string login;
        public string myName;        
        public string password;
        
        
        
        public string path = "Z:\\КамРус\\Programs\\Напоминалка\\Sheet1.html";
        //public string path = "C:\\Users\\USER\\Desktop\\Ruslan Prog\\Text.txt";
        public void SendMessage(string mailAddress, string name, string message)
        {
            MailAddress fromMailAddress = new MailAddress(login, myName, Encoding.UTF8);
            MailAddress toMailAddress = new MailAddress(mailAddress, name, Encoding.UTF8);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.Subject = "Оплата";
                mailMessage.Body = message;
                //mailMessage.Body = "Привет!";
                mailMessage.IsBodyHtml = true;
                //mailMessage.IsBodyHtml = false;
                smtpClient.Host = "smtp.mail.ru";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, password);

                smtpClient.Send(mailMessage);
            }
        }
    }
}

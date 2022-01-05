using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Payment_Notification
{
    public class MessageClass
    {
        public string tomorrow = "Завтра";
        public string today = "Сегодня";

        public string message = "";
        public MessageClass()
        {

        }

        public string ReadMessage(string path, string day, string dealNumber, string dealDate) 
        {

            string[] text = File.ReadAllLines(path, Encoding.Default);

            text[4] = "Напоминаем Вам, что " + day + " срок оплаты по счету № " +
                dealNumber + " от " + dealDate + " на ИП Камалова Р.Р.";

            string message = "";
            for (int i = 0; i < text.Length; i++)
                message += text[i];

            return message;


            message = "< div class=\"class_1573903534\"><div>Добрый день!</div><div>&nbsp;</div><div>Напоминаем Вам, что TOMORROW наступает срок оплаты по счету № DEALNUMBER от DEALDATE на ИП Камалова Р.Р.</div><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><div data-signature-widget=\"container\"><div data-signature-widget=\"content\"><div><p><span style = \"font-size:11pt\" >< span style=\"line-height:normal\"><span style = \"font-family:Calibri,sans-serif\" >< span style=\"font-size:12.0pt\">С уважением к Вам и Вашему бизнесу!</span><br><span style = \"font-size:12.0pt\" >< a href=\"https://kamrustrans.ru/ \" target=\"_blank\" rel=\" noopener noreferrer\"><span style = \"font-size:11.5pt\" >< span style=\"color:#0077cc\">ООО \"КамРусТранс\"</span></span></a></span><br><span style = \"font-size:12.0pt\" > Руслан Камалов</span><br><span style = \"font-size:12.0pt\" >8 & nbsp;968 339-98-44<br><br>" +
                "<a href = \"#mailruanchor_mailruanchor_mailruanchor_mailruanchor_mailruanchor_mailruanchor_mailruanchor_\" anchor=\"1\" rel=\" noopener noreferrer\"> <span style = \"color:blue\" > www.</ span ></ a >< span style=\"color:#6a60ec\"><a href = \"https://kamrustrans.ru/ \" target=\"_blank\" rel=\" noopener noreferrer\"><span style = \"color:#173bd3\" > kamrustrans.ru </ span ></ a ></ span ></ span ></ span ></ span ></ span ></ p >< p > &nbsp;</p><p><span style = \"font-size:11pt\" >< span style=\"line-height:normal\"><span style = \"font-family:Calibri,sans-serif\" >< span style=\"font-size:12.0pt\"><img src = \"Z:\\КамРус\\Programs\\Напоминалка\\Kamrustrans.png\" style=\"width:206px;height:34px\"></span></span></span></span></p></div><div><br><span><span><strong><span><a href = \"https://ati.su/firms/616663/passport \" target=\"_blank\" rel=\" noopener noreferrer\"><img alt = \"\" src=\"Z:\\КамРус\\Programs\\Напоминалка\\Passport.png\">" +
                "</a></span></strong></span></span></div></div></div></div>";

        }
}
}

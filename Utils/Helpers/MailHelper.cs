using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Utils.Helpers
{
    public class MailHelper
    {
        public void Send(string? subject, string? from, string? psw, string? link, params string?[] recipients)
        {
            try
            {
                //string? senderEmail = "smontero@fmp.com.do";
                //string? senderEmail = "luismella2403@gmail.com";
                //
                //You must enable access to less secure apps
                //https://myaccount.google.com/u/0/lesssecureapps?pli=1
                // https://myaccount.google.com/apppasswords?pli=1

                string? body = "<!doctype html>\n<html>\n  <head>\n    <meta name=\"viewport\" content=\"width=device-width\">\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\n    <title>Simple Transactional Email</title>\n    <style>\n    /* -------------------------------------\n        INLINED WITH htmlemail.io/inline\n    ------------------------------------- */\n    /* -------------------------------------\n        RESPONSIVE AND MOBILE FRIENDLY STYLES\n    ------------------------------------- */\n    @media only screen and (max-width: 620px) {\n      table[class=body] h1 {\n        font-size: 28px !important;\n        margin-bottom: 10px !important;\n      }\n      table[class=body] p,\n            table[class=body] ul,\n            table[class=body] ol,\n            table[class=body] td,\n            table[class=body] span,\n            table[class=body] a {\n        font-size: 16px !important;\n      }\n      table[class=body] .wrapper,\n            table[class=body] .article {\n        padding: 10px !important;\n      }\n      table[class=body] .content {\n        padding: 0 !important;\n      }\n      table[class=body] .container {\n        padding: 0 !important;\n        width: 100% !important;\n      }\n      table[class=body] .main {\n        border-left-width: 0 !important;\n        border-radius: 0 !important;\n        border-right-width: 0 !important;\n      }\n      table[class=body] .btn table {\n        width: 100% !important;\n      }\n      table[class=body] .btn a {\n        width: 100% !important;\n      }\n      table[class=body] .img-responsive {\n        height: auto !important;\n        max-width: 100% !important;\n        width: auto !important;\n      }\n    }\n\n    /* -------------------------------------\n        PRESERVE THESE STYLES IN THE HEAD\n    ------------------------------------- */\n    @media all {\n      .ExternalClass {\n        width: 100%;\n      }\n      .ExternalClass,\n            .ExternalClass p,\n            .ExternalClass span,\n            .ExternalClass font,\n            .ExternalClass td,\n            .ExternalClass div {\n        line-height: 100%;\n      }\n      .apple-link a {\n        color: inherit !important;\n        font-family: inherit !important;\n        font-size: inherit !important;\n        font-weight: inherit !important;\n        line-height: inherit !important;\n        text-decoration: none !important;\n      }\n      #MessageViewBody a {\n        color: inherit;\n        text-decoration: none;\n        font-size: inherit;\n        font-family: inherit;\n        font-weight: inherit;\n        line-height: inherit;\n      }\n      .btn-primary table td:hover {\n        background-color: #34495e !important;\n      }\n      .btn-primary a:hover {\n        background-color: #34495e !important;\n        border-color: #34495e !important;\n      }\n    }\n    </style>\n  </head>\n  <body class=\"\" style=\"background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;\">\n    <span class=\"preheader\" style=\"color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;\">This is preheader text. Some clients will show this text as a preview.</span>\n    <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"body\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;\">\n      <tr>\n        <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\">&nbsp;</td>\n        <td class=\"container\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;\">\n          <div class=\"content\" style=\"box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;\">\n\n            <!-- START CENTERED WHITE CONTAINER -->\n            <table role=\"presentation\" class=\"main\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;\">\n\n              <!-- START MAIN CONTENT AREA -->\n              <tr>\n                <td class=\"wrapper\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;\">\n                  <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;\">\n                    <tr>\n                      <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\">\n                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;\">Hola,</p>\n                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;\">Para continual con el proceso, una vez presionado el botón Confirmar correo, se le notificá lo antes posible el estatus de su solicitud.</p>\n                        <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"btn btn-primary\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;\">\n                          <tbody>\n                            <tr>\n                              <td align=\"left\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;\">\n                                <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;\">\n                                  <tbody>\n                                    <tr>\n                                      <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #3498db; border-radius: 5px; text-align: center;\"> <a href=" + link + " target=\"_blank\" style=\"display: inline-block; color: #ffffff; background-color: #3498db; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;\">Confirmar Correo</a> </td>\n                                    </tr>\n                                  </tbody>\n                                </table>\n                              </td>\n                            </tr>\n                          </tbody>\n                        </table>\n                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;\">Buena Suerte!.</p>\n                      </td>\n                    </tr>\n                  </table>\n                </td>\n              </tr>\n\n            <!-- END MAIN CONTENT AREA -->\n            </table>\n\n            <!-- START FOOTER -->\n            <div class=\"footer\" style=\"clear: both; Margin-top: 10px; text-align: center; width: 100%;\">\n              <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;\">\n                <tr>\n                  <td class=\"content-block\" style=\"font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;\">\n                                      </td>\n                </tr>\n              </table>\n            </div>\n            <!-- END FOOTER -->\n\n          <!-- END CENTERED WHITE CONTAINER -->\n          </div>\n        </td>\n        <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\">&nbsp;</td>\n      </tr>\n    </table>\n  </body>\n</html>";

                var mail = new MailMessage()
                {
                    //Sender email address
                    //From = new MailAddress("no-reply@fmp.com.do"),
                    From = new MailAddress(from),
                    Sender = new MailAddress(from),
                    //  Sender = new MailAddress("no-reply@fmp.com.do"),
                    // ReplyToList = { new MailAddress("support@fmp.com.do") },
                    Subject = subject,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    Priority = MailPriority.High,
                    IsBodyHtml = true,
                    
                    //= "luismella2403@gmaill.com"

                };


                foreach (var recipient in recipients)
                {
                    mail.To.Add(recipient);

                }

                var smtpServer = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(from, psw),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryFormat = SmtpDeliveryFormat.International,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Host= "smtp.gmail.com"

                };

                smtpServer.Send(mail);
                smtpServer.Dispose();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
        public void Correo(string? subject, string? from, string? psw, string? message,string? text, string? CompanyName,string? link, params string?[] recipients)
        {
            try
            {
                //string? senderEmail = "smontero@fmp.com.do";
                //string? senderEmail = "luismella2403@gmail.com";

                //You must enable access to less secure apps
                //https://myaccount.google.com/u/0/lesssecureapps?pli=1
                // https://myaccount.google.com/apppasswords?pli=1

                var facebook = "https://www.facebook.com/fucecard";
                var instagram = "https://www.instagram.com/fucecard/";
                var linkedin = "https://www.linkedin.com/in/fuceca-rd-461912300/";
                var twitter = "https://twitter.com/fuceca";
                // string? body = "<!DOCTYPE html>< html >  < head >    < meta charset = \"UTF-8\" />    < meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />    < title >"+ subject+" </ title >    < style >      *,      ::before,      ::after {                box - sizing: border - box;            margin: 0;            padding: 0;                font - size: 16px;            }      .body {                background - color: #f6f6f6;        font - family: sans - serif;                -webkit - font - smoothing: antialiased; font - size: 14px;                line - height: 1.4;            margin: 0;            padding: 0;                margin - bottom: 2rem;                -ms - text - size - adjust: 100 %;                -webkit - text - size - adjust: 100 %;            }      .body.logo {            width: 14rem;            margin: 2rem auto;            }      .body img {            width: 100 %;            }      .container {                background - color: white;            width: 90 %;                min - height: 20rem;               max - width: 50rem;            margin: 0 auto;                margin - top: 2.5rem;                border - radius: 1.35rem;            padding: 2rem 0.6rem 2.5rem 0.6rem;               box - shadow: 0px 10px 20px - 6px rgba(0, 0, 0, 0.1);            }      .titulo {            width: 90 %;            margin: auto;                margin - bottom: 1rem;                font - size: 1.3rem;            }      .btn {                min - width: 10rem;            color: #ffffff;        background - color: #00004c;        border: solid 1px #00004c;        border-radius: 5px;                text - decoration: none;                font - size: 16px;            margin: 0;            padding: 1rem;                border - color: #00004c;        width: 30 %;                text - align: center;            margin: 0 auto;            display: flex;                justify - content: center;                margin - top: 2rem;            transition: all 0.3s ease-in-out;           }      .btn: hover {           background - color: #020231;        border - color: #020231;        transition: all 0.3s ease-in-out;           }      .message__container {            padding: 0.6rem 0rem 1rem 0rem;                margin - bottom: 1.5rem;            width: 90 %;            margin: auto;           }      .message__container p {                line - height: 1.8;                text - align: justify;            }      .footer {            width: 90 %;                padding - top: 2rem;            margin: 3rem auto 0px;                border - top: 1px solid #00004c;      }      .footer - social - media {            display: flex;                justify - content: center;            gap: 1.2rem;            }    </ style >  </ head >  < body class=\"body\">    <div class=\"logo\">      <img src = \"http://fuceca.org/fuceca/wp-content/uploads/2024/03/Logo_FUCECA-1-1536x626.png\" />    </ div >    < div class=\"container\">      <h2 class=\"titulo\">{Title    }</h2>      <div class=\"message__container\">        {message}        < p > Atentamente,</ p >        < p >{ CompanyName}</ p >      </ div >            < a href = \"{link} \" target = \"_blank\" class= \"btn\" >{ text}</ a >          < div class= \"footer\" >        < div class= \"footer-social-media\" >          < a href = \"{instagram}\" >            < svg xmlns = \"http://www.w3.org/2000/svg\" width = \"25\" viewBox = \"0 0 448 512\" >              < path                fill = \"#00004C\"                d = \"M224.1 141c-63.6 0-114.9 51.3-114.9 114.9s51.3 114.9 114.9 114.9S339 319.5 339 255.9 287.7 141 224.1 141zm0 189.6c-41.1 0-74.7-33.5-74.7-74.7s33.5-74.7 74.7-74.7 74.7 33.5 74.7 74.7-33.6 74.7-74.7 74.7zm146.4-194.3c0 14.9-12 26.8-26.8 26.8-14.9 0-26.8-12-26.8-26.8s12-26.8 26.8-26.8 26.8 12 26.8 26.8zm76.1 27.2c-1.7-35.9-9.9-67.7-36.2-93.9-26.2-26.2-58-34.4-93.9-36.2-37-2.1-147.9-2.1-184.9 0-35.8 1.7-67.6 9.9-93.9 36.1s-34.4 58-36.2 93.9c-2.1 37-2.1 147.9 0 184.9 1.7 35.9 9.9 67.7 36.2 93.9s58 34.4 93.9 36.2c37 2.1 147.9 2.1 184.9 0 35.9-1.7 67.7-9.9 93.9-36.2 26.2-26.2 34.4-58 36.2-93.9 2.1-37 2.1-147.8 0-184.8zM398.8 388c-7.8 19.6-22.9 34.7-42.6 42.6-29.5 11.7-99.5 9-132.1 9s-102.7 2.6-132.1-9c-19.6-7.8-34.7-22.9-42.6-42.6-11.7-29.5-9-99.5-9-132.1s-2.6-102.7 9-132.1c7.8-19.6 22.9-34.7 42.6-42.6 29.5-11.7 99.5-9 132.1-9s102.7-2.6 132.1 9c19.6 7.8 34.7 22.9 42.6 42.6 11.7 29.5 9 99.5 9 132.1s2.7 102.7-9 132.1z\"              />            </ svg >          </ a >          < a href = \"{facebook}\" >            < svg xmlns = \"http://www.w3.org/2000/svg\" width = \"25\" viewBox = \"0 0 512 512\" >              < path                fill = \"#00004C\"                d = \"M512 256C512 114.6 397.4 0 256 0S0 114.6 0 256C0 376 82.7 476.8 194.2 504.5V334.2H141.4V256h52.8V222.3c0-87.1 39.4-127.5 125-127.5c16.2 0 44.2 3.2 55.7 6.4V172c-6-.6-16.5-1-29.6-1c-42 0-58.2 15.9-58.2 57.2V256h83.6l-14.4 78.2H287V510.1C413.8 494.8 512 386.9 512 256h0z\"              />            </ svg >          </ a >                    < a href = \"{x-twitter}\" >            < svg xmlns = \"http://www.w3.org/2000/svg\" width = \"25\" viewBox = \"0 0 512 512\" >              < path                fill = \"#00004C\"                d = \"M389.2 48h70.6L305.6 224.2 487 464H345L233.7 318.6 106.5 464H35.8L200.7 275.5 26.8 48H172.4L272.9 180.9 389.2 48zM364.4 421.8h39.1L151.1 88h-42L364.4 421.8z\"              />            </ svg >          </ a >          < a href = \"{linkedIn}\" >            < svg xmlns = \"http://www.w3.org/2000/svg\" width = \"25\" viewBox = \"0 0 448 512\" >              < path                fill = \"#00004C\"                d = \"M100.3 448H7.4V148.9h92.9zM53.8 108.1C24.1 108.1 0 83.5 0 53.8a53.8 53.8 0 0 1 107.6 0c0 29.7-24.1 54.3-53.8 54.3zM447.9 448h-92.7V302.4c0-34.7-.7-79.2-48.3-79.2-48.3 0-55.7 37.7-55.7 76.7V448h-92.8V148.9h89.1v40.8h1.3c12.4-23.5 42.7-48.3 87.9-48.3 94 0 111.3 61.9 111.3 142.3V448z\"              />            </ svg >          </ a >        </ div >      </ div >    </ div >  </ body ></ html >";
                string? body = $"<!DOCTYPE html> <html " +
                    $"style='box-sizing: border-box; margin: 0; padding: 0; font-size: 16px'> <head> <meta charset='UTF-8' /> " +
                    $"<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css\" integrity=\"sha512-SnH5WK+bZxgPHs44uWIX+LLJAJ9/2PkPKZ5QiAj6Ta86w+fsb2TkcmfRyVX3pBnMFcV7oQPJkl9QevSCWr3W6A==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\" />" +
                    $"<meta name='viewport' content='width=device-width, initial-scale=1.0' /> <title>{subject}</title> " +
                    $"</head> <body style=' background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 4rem; margin-bottom: 2rem; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; ' > " 
                    +
                    $"<div style='width: 14rem; margin: 2rem auto'> <img style='width: 100%' src='http://fuceca.org/fuceca/wp-content/uploads/2024/03/Logo_FUCECA-1-1536x626.png' /> </div> <div style=' background-color: white; width: 90%; min-height: 20rem; max-width: 50rem; " +
                    $"margin: 0 auto; margin-top: 2.5rem; border-radius: 1.35rem; padding: 2rem 0.6rem 2.5rem 0.6rem; box-shadow: 0px 10px 20px -6px rgba(0, 0, 0, 0.1); ' > <h2 style='width: 90%; margin: auto; margin-bottom: 1rem; font-size: 1.3rem'>{subject}</h2>" +
                    $" <div style=' padding: 0.6rem 0rem 1rem 0rem; margin-bottom: 1.5rem; width: 90%; margin: auto; line-height: 1.8; text-align: justify; ' > {message} <p>Atentamente,</p> <p>{CompanyName}</p> </div> <a href='{link} ' target='_blank' style=' min-width: 10rem; color: #ffffff; background-color: #00004c; border: solid 1px #00004c; border-radius: 5px; text-decoration: none; font-size: 16px; " +
                    $"margin: 0; padding: 0rem; border-color: #00004c; width: 30%; text-align: center; margin: 0 auto; display: flex; justify-content: center; margin-top: 2rem; transition: all 0.3s ease-in-out;' ><p style='display:flex; margin:auto;padding:1rem;'>{text}</p></a > <div style='width: 90%; padding-top: 2rem; margin: 3rem auto 0px; border-top: 1px solid #00004c' >" +
                    $" <div style='display: flex; justify-content: center; gap: 1.2rem'> " +
                    $"</div> </div> </div> </body> </html>";
                var mail = new MailMessage()
                {
                    //Sender email address
                    //From = new MailAddress("no-reply@fmp.com.do"),
                    From = new MailAddress(from),
                    Sender = new MailAddress(from),
                    //  Sender = new MailAddress("no-reply@fmp.com.do"),
                    // ReplyToList = { new MailAddress("support@fmp.com.do") },
                    Subject = subject,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    Priority = MailPriority.High,
                    IsBodyHtml = true,
                    //= "luismella2403@gmaill.com"

                };


                foreach (var recipient in recipients)
                {
                    mail.To.Add(recipient);

                }

                var smtpServer = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(from, psw),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryFormat = SmtpDeliveryFormat.International,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Host= "smtp.gmail.com"

                };

                smtpServer.Send(mail);
                smtpServer.Dispose();
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}

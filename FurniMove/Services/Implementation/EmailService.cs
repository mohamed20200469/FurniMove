using FurniMove.Models;
using FurniMove.Services.Abstract;
using MailKit.Net.Smtp;
using MimeKit;


namespace FurniMove.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        public MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        public void Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public void ConfirmEmailAddress(string email, string confirmationLink)
        {
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FurniMove", "furnimoveproject@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Email Confirmation";

            var bodyHtml = @"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""x-ua-compatible"" content=""ie=edge"">
                <title>Email Confirmation</title>
                <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                <style>
                /* Styles for the button */
                .button-container {
                    text-align: center;
                }

                .button-container button {
                    background-color: red;
                    color: white;
                    padding: 20px 40px;
                    border: none;
                    border-radius: 8px;
                    font-size: 20px;
                    cursor: pointer;
                    text-decoration: none;
                }

                .button-container button a {
                    text-decoration: none;
                    color: white;
                }
                </style>
                </head>
                <body style=""background-color: #e9ecef; font-family: Arial, sans-serif;"">

                <!-- start body -->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <!-- start explanation text -->
                    <tr>
                        <td align=""center"" bgcolor=""#ffffff"" style=""padding: 30px 20px;"">
                            <p style=""font-size: 18px; margin-bottom: 20px;"">Please confirm your email address to activate your account. If you didn't request this email, you can safely ignore it.</p>
                        </td>
                    </tr>
                    <!-- end explanation text -->
                    <!-- start button -->
                    <tr>
                        <td align=""center"" bgcolor=""#ffffff"" style=""padding-bottom: 30px;"">
                            <div class=""button-container"">
                                <button>
                                    <a href=""" + confirmationLink + @""">Confirm Email Address</a>
                                </button>
                            </div>
                        </td>
                    </tr>
                    <!-- end button -->
                </table>
                <!-- end body -->

                </body>
                </html>
                ";


            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = bodyHtml;


            message.Body = bodyBuilder.ToMessageBody();

            Send(message);
        }
    }
}

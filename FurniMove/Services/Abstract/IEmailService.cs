using FurniMove.Models;
using MimeKit;

namespace FurniMove.Services.Abstract
{
    public interface IEmailService
    {
        MimeMessage CreateEmailMessage(Message message);
        void Send(MimeMessage message);
        void SendEmail(Message message);
        public void ConfirmEmailAddress(string email, string confirmationLink);
    }
}

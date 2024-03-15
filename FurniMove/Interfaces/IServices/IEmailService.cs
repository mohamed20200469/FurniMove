using FurniMove.Models;
using MimeKit;

namespace FurniMove.Interfaces.IServices
{
    public interface IEmailService
    {
        MimeMessage CreateEmailMessage(Message message);
        void Send(MimeMessage message);
        void SendEmail(Message message);
    }
}

using BiblioTechDomain.Services.IService;

namespace BiblioTechDomain.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}

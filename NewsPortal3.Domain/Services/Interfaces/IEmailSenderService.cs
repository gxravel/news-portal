using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal3.Domain.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(string email, string subject, string message);
    }
}

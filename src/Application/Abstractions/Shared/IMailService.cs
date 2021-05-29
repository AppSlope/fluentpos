using FluentPOS.Application.Requests.Shared;
using System.Threading.Tasks;

namespace FluentPOS.Application.Interfaces.Services.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
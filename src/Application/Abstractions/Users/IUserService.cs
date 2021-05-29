using FluentPOS.Application.Requests.Users;
using FluentPOS.Application.Wrapper;
using System.Threading.Tasks;

namespace FluentPOS.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult<int>> ConfirmEmailAsync(int userId, string code);

        Task<IResult> ForgotPasswordAsync(string emailId, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
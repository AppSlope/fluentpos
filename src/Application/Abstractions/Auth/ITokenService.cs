using FluentPOS.Application.Requests.Auth;
using FluentPOS.Application.Responses.Auth;
using FluentPOS.Shared.Wrapper;
using System.Threading.Tasks;

namespace FluentPOS.Application.Abstractions.Auth
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest model, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest model, string ipAddress);
    }
}
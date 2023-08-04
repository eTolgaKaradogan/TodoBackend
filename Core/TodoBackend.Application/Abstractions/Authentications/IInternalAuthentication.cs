using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs;

namespace TodoBackend.Application.Abstractions.Authentications
{
    public interface IInternalAuthentication
    {
        Task<TokenDto> LoginAsync(string email, string password, int accessTokenLifeTime);
        Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
    }
}

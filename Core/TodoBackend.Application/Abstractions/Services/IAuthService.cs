using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Authentications;

namespace TodoBackend.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetToken(string resetToken, string userId);
        Task<string> GetNameSurname(string email);
    }
}

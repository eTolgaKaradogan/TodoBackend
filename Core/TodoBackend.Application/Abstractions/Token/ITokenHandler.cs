using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}

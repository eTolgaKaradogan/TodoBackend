using TodoBackend.Application.DTOs;

namespace TodoBackend.Application.Features.AppUser.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
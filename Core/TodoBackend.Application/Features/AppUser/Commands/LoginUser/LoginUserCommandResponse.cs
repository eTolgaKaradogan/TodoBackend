using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string NameSurname { get; set; }
    }

    public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
    {
        public TokenVM Token { get; set; }
    }

    public class LoginUserCommandErrorResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
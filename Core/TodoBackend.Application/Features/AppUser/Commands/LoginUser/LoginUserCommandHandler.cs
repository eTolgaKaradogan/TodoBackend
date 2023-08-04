using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.DTOs;
using TodoBackend.Application.DTOs.Task;
using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService authService;
        readonly IMapper mapper;
        public LoginUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDto token = await authService.LoginAsync(request.Email, request.Password, 900);
            string nameSurname = await authService.GetNameSurname(request.Email);
            return new LoginUserCommandSuccessResponse()
            {
                Token = mapper.Map<TokenVM>(token),
                NameSurname= nameSurname
            };
        }
    }
}

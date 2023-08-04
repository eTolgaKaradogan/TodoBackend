using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.Abstractions.Token;
using TodoBackend.Application.DTOs;
using TodoBackend.Application.Exceptions;
using TodoBackend.Application.Helpers;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> userManager;
        readonly ITokenHandler tokenHandler;
        readonly IConfiguration configuration;
        readonly SignInManager<AppUser> signInManager;
        readonly IUserService userService;
        readonly IMailService mailService;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            this.userManager = userManager;
            this.tokenHandler = tokenHandler;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userService = userService;
            this.mailService = mailService;
        }

        public async Task<string> GetNameSurname(string email)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            return user.NameSurname;
        }

        public async Task<TokenDto> LoginAsync(string email, string password, int accessTokenLifeTime)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                throw new AuthenticationErrorException();

            TokenDto token = tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                resetToken = resetToken.UrlEncode();
                await mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if(user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                TokenDto token = tokenHandler.CreateAccessToken(150, user);
                await userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }

            throw new NotFoundUserException();
        }

        public async Task<bool> VerifyResetToken(string resetToken, string userId)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            resetToken = resetToken.UrlDecode();

            return await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.DTOs.User;
using TodoBackend.Application.Exceptions;
using TodoBackend.Application.Helpers;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public int TotalUsersCount => userManager.Users.Count();

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
        {
            IdentityResult result = await userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.Email.Split('@')[0],
                Email = model.Email
            }, model.Password);

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";

            else
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            return response;
        }

        public async Task<List<UserDto>> GetAllAsync(int page, int size)
        {
            var users = await userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled

            }).ToList();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenCreatedDate, int refreshTokenLifeTime)
        {
            if (user == null)
                throw new NotFoundUserException();

            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenCreatedDate.AddSeconds(refreshTokenLifeTime);
            await userManager.UpdateAsync(user);
        }

        public async Task AssignRoleToUser(string id, string[] roles)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, userRoles);

                await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}

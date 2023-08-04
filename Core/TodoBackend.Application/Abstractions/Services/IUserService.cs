using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs.User;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenCreatedDate, int addOnAccessTokenLifeTime);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    }
}

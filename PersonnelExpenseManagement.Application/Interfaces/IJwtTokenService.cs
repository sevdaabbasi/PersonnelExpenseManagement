using Microsoft.AspNetCore.Identity;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(User user, UserManager<User> userManager);
} 
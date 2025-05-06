using Microsoft.AspNetCore.Identity;
using PersonnelExpenseManagement.Domain.Entities;
using System.Security.Claims;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user, IList<string> roles);
    ClaimsPrincipal? ValidateToken(string token);
} 
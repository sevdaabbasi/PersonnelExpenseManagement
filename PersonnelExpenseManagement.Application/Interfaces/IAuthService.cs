using System.Security.Claims;
using PersonnelExpenseManagement.Application.DTOs.Auth;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<User> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
} 
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Infrastructure.Services;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public interface IAuthService
{
    Task<(bool Success, string Token)> LoginAsync(string email, string password);
    Task<(bool Success, string Message)> RegisterAsync(User user, string password);
    Task<User?> GetCurrentUserAsync(ClaimsPrincipal principal);
}

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(bool Success, string Token)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, string.Empty);

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
            return (false, string.Empty);

        var token = _jwtTokenService.GenerateToken(user);
        return (true, token);
    }

    public async Task<(bool Success, string Message)> RegisterAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));

        return (true, "User registered successfully");
    }

    public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return null;

        return await _userManager.FindByIdAsync(userId);
    }
} 
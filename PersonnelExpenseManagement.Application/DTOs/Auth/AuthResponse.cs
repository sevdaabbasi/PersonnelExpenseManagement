namespace PersonnelExpenseManagement.Application.DTOs.Auth;

public record AuthResponse
{
    public string Token { get; init; } = string.Empty;
    public DateTime Expiration { get; init; }
    public string UserId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
} 
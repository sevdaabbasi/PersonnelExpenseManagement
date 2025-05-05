namespace PersonnelExpenseManagement.Application.DTOs.Auth;
 
public record AuthRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
} 
namespace PersonnelExpenseManagement.Application.DTOs.Auth;

public record RegisterResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
} 
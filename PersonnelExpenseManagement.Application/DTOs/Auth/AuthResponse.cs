namespace PersonnelExpenseManagement.Application.DTOs.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
} 
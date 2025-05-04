namespace PersonnelExpenseManagement.Application.DTOs.Auth;

public record RegisterRequest
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string IBAN { get; init; } = string.Empty;
    public string RoleId { get; init; } = string.Empty;
} 
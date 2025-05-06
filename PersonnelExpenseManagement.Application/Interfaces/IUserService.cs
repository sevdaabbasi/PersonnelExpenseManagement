using PersonnelExpenseManagement.Application.DTOs.User;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task<UserDto> UpdateUserAsync(string id, UpdateUserDto dto);
    Task DeleteUserAsync(string id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
}

public static class AppConstants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class ErrorMessages
    {
        public const string InvalidExpenseAmount = "Expense amount must be greater than zero";
        public const string CategoryRequired = "Expense category is required";
        public const string InvalidBankDetails = "Invalid bank account details";
    }

    public static class ValidationRules
    {
        public const int MaxDescriptionLength = 500;
        public const int MinPasswordLength = 8;
        public const decimal MaxExpenseAmount = 10000M;
    }
} 
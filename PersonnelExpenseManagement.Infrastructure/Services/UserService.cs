using Microsoft.AspNetCore.Identity;
using PersonnelExpenseManagement.Application.DTOs.User;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Domain.Exceptions;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IBAN = dto.IBAN
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(user, "User");

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IBAN = user.IBAN
        };
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException($"User with ID {id} not found");

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IBAN = user.IBAN
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IBAN = user.IBAN
        });
    }

    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException($"User with ID {id} not found");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.IBAN = dto.IBAN;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IBAN = user.IBAN
        };
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException($"User with ID {id} not found");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
} 
using FluentValidation;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.DTOs.Expense.Validators;

public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
{
    public CreateExpenseDtoValidator()
    {
       

        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0)
            .WithMessage("Valid category must be selected");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .LessThanOrEqualTo(10000)
            .WithMessage("Amount must be between 0 and 10000");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description is required and must not exceed 500 characters");

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Location is required and must not exceed 200 characters");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .Must(method => new[] { "CreditCard", "Cash", "BankTransfer" }.Contains(method))
            .WithMessage("Invalid payment method");

        RuleFor(x => x.ExpenseDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Expense date cannot be in the future");
    }
}

public class ExpenseDtoValidator : AbstractValidator<ExpenseDto>
{
    public ExpenseDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid expense ID");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0)
            .WithMessage("Valid category must be selected");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .LessThanOrEqualTo(10000)
            .WithMessage("Amount must be between 0 and 10000");
    }
} 
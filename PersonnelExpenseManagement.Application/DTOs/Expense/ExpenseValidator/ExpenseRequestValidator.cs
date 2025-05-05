using FluentValidation;

namespace PersonnelExpenseManagement.Application.DTOs.Expense.ExpenseValidator;

public class ExpenseRequestValidator : AbstractValidator<CreateExpenseDto>
{
    public ExpenseRequestValidator()
    {
        RuleFor(x => x.ExpenseCategoryId)
            .NotEmpty().WithMessage("Expense category is required.")
            .GreaterThan(0).WithMessage("Invalid expense category.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.ExpenseDate)
            .NotEmpty().WithMessage("Expense date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expense date cannot be in the future.");
    }
}

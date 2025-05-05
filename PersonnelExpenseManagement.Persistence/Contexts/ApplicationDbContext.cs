using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.ExpenseCategory)
            .WithMany(ec => ec.Expenses)
            .HasForeignKey(e => e.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Expense)
            .WithMany()
            .HasForeignKey(p => p.ExpenseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);
    }
}
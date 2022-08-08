using Microsoft.EntityFrameworkCore;
using HomeAccountingWithAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccountingWithAspNet.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomeCategory>().HasData(
                new IncomeCategory
                {
                    Id = 1,
                    Name = "Заработная плата"
                },
                new IncomeCategory
                {
                    Id = 2,
                    Name = "Доход с сдачи в аренду недвижимости"
                },
                new IncomeCategory
                {
                    Id = 3,
                    Name = "Иные доходы"
                });

            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory
                {
                    Id = 1,
                    Name = "Продукты питания",
                },
                new ExpenseCategory
                {
                    Id = 2,
                    Name = "Транспорт",
                },
                new ExpenseCategory
                {
                    Id = 3,
                    Name = "Мобильная связь",
                },
                new ExpenseCategory
                {
                    Id = 4,
                    Name = "Интернет",
                },
                new ExpenseCategory
                {
                    Id = 5,
                    Name = "Развлечения",
                },
                new ExpenseCategory
                {
                    Id = 6,
                    Name = "Другое",
                });
        }
    }
}

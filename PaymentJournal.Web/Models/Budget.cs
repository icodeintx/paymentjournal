﻿namespace PaymentJournal.Web.Models;

public class Budget
{
    public decimal AnnualSalary { get; set; }
    public List<AccountList> AccountLists => GetAccountLists();
    public List<BankAccount> BankAccounts { get; set; } = new();
    public Guid BudgetId { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public List<CreditCard> CreditCards { get; set; } = new();
    public decimal Debt_Income_Ratio => CalculateDTI();
    public List<Expense> Expenses { get; set; } = new();
    public List<Income> Incomes { get; set; } = new();
    public DateTime LastSavedDate { get; set; } = DateTime.Now;
    public string Name { get; set; } = string.Empty;
    public List<OnlineService> OnlineServices { get; set; } = new();
    public decimal TotalMonthlyExpenses => Expenses.Select(y => y.Amount).Sum();
    public decimal TotalMonthlyIncomes => Incomes.Select(x => x.Amount).Sum();
    public decimal TotalYearlyExpenses => Expenses.Select(y => y.Amount).Sum() * 12;
    public decimal TotalYearlyIncomes => Incomes.Select(x => x.Amount).Sum() * 12;
    public decimal YearlyWitholdings => AnnualSalary - TotalYearlyIncomes;
    public decimal HalfMonthlyExpenses => TotalMonthlyExpenses / 2;

    public List<AccountList> GetAccountLists()
    {
        var list = new List<AccountList>();

        //get accounts from Bank
        foreach (var account in BankAccounts)
        {
            list.Add(
                        new AccountList
                        {
                            AccountNumber = account.AccountNumber,
                            Description = account.Description
                        });
        }

        //get accounts from Cards
        foreach (var account in CreditCards)
        {
            list.Add(
                        new AccountList
                        {
                            AccountNumber = account.AccountNumber,
                            Description = account.Description
                        });
        }

        //get Accounts from Online Service
        foreach (var account in OnlineServices)
        {
            list.Add(
                        new AccountList
                        {
                            //AccountNumber = account.PaidTo,
                            AccountNumber = "n/a",
                            Description = $"{account.Service} ({account.PaidTo})"
                        });
        }

        return list;
    }

    public IList<ExpensePayGroup> GetExpensePayGroups()
    {
        var result = (from expense in Expenses
                      group expense by expense.PaidBy into expGroup
                      select new ExpensePayGroup
                      {
                          Account = expGroup.Key,
                          Sum = expGroup.Sum(x => x.Amount)
                      }).ToList();

        return result;
    }

    private decimal CalculateDTI()
    {
        if (AnnualSalary > 0 && TotalMonthlyExpenses > 0)
        {
            //Monthy Expenses / (Annual Income / 12) * 100
            //return (TotalMonthlyExpenses / (AnnualSalary / 12)) * 100;
            return (TotalMonthlyExpenses / (AnnualSalary / 12)) ;
        }
        else
        {
            return 0m;
        }
    }
}
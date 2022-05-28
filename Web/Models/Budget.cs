namespace PaymentJournal_Web.Models;

public class Budget
{
    public decimal AnnualSalary { get; set; }
    public List<BankAccount> BankAccounts { get; set; } = new();
    public Guid BudgetId { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public List<CreditCard> CreditCards { get; set; } = new();
    public decimal Debt_Income_Ratio => CalculateDTI();
    public List<Expense> Expenses { get; set; } = new();
    public List<Income> Incomes { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public List<OnlineService> OnlineServices { get; set; } = new();
    public decimal TotalExpenses => Expenses.Select(y => y.Amount).Sum();
    public decimal TotalIncomes => Incomes.Select(x => x.Amount).Sum();

    /// <summary>
    /// Returns expenses summed by PayTo acct
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ExpensePayGroup> GetExpensePayGroups()
    {
        var result = from expense in Expenses
                     group expense by expense.PaidBy into expGroup
                     select new ExpensePayGroup
                     {
                         Account = expGroup.Key,
                         Sum = expGroup.Sum(x => x.Amount)
                     };

        return result;
    }

    /// <summary>
    /// Debt_To_Income_Ratio
    /// </summary>
    /// <returns></returns>
    private decimal CalculateDTI()
    {
        if (AnnualSalary > 0 && TotalExpenses > 0)
        {
            //Monthy Expenses / Annual Income (monthly) * 100
            return (TotalExpenses / (AnnualSalary / 12)) * 100;
        }
        else
        {
            return 0m;
        }
    }
}
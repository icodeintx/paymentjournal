namespace PaymentJournal_Web.Models;

public class Budget
{
    public List<BankAccount> BankAccounts { get; set; }
    public Guid BudgetId { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public List<CreditCard> CreditCards { get; set; }
    public List<Expense> Expenses { get; set; }
    public List<Income> Incomes { get; set; }
    public List<OnlineService> OnlineServices { get; set; }
    public decimal TotalExpenses => Expenses.Select(y => y.Amount).Sum();
    public decimal TotalIncomes => Incomes.Select(x => x.Amount).Sum();
}
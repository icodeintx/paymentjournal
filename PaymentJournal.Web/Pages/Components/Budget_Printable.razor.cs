using Microsoft.AspNetCore.Components;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;
using System.Globalization;


namespace PaymentJournal.Web.Pages.Components;

public partial class Budget_Printable : ComponentBase
{
    public Budget Budget { get; set; } = new();

    [Parameter]
    public string BudgetId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    [Inject]
    private BudgetRepo Repo { get; set; }
    
    public List<SummaryItem> SummaryItems { get; set; } = new();
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }
    

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (string.IsNullOrEmpty(BudgetId))
        {
            //if no budgetID is passed as parameter Get the latest budget

            //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
            Budget = Repo.GetLatestBudget();

            if (Budget is null)
            {
                //if no latest budget found then create a new budget
                Message = "No Budget Found.  Creating New Budget.";
                Budget = new();
            }
            else
            {
                Message = string.Empty;
            }
        }
        else
        {
            //BudgetId was passed as parameter, lookup the budget and display
            try
            {
                //if BudgetId pass as parameter is Guid Default then create new budget
                if (Guid.Parse(BudgetId) == Guid.Empty)
                {
                    Budget = new();
                }

                Budget = Repo.GetBudget(Guid.Parse(BudgetId));
                if (Budget is null)
                {
                    //if budgetId was not found create a new budget
                    Message = "No Budget Found.  Creating New Budget.";
                    Budget = new();
                }
                else
                {
                    Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                //if any errors were encountered, create a new budget
                Message = $"Error retrieving Budget. {ex.Message} ";
                Budget = new Budget();
            }
        }
        PopulateSummaryItems();
    }

    private void PopulateSummaryItems()
    {
        this.SummaryItems = new List<SummaryItem>();

        this.SummaryItems.Add(new SummaryItem("Annual Salary (before Taxes)", FormatMoney(Budget.AnnualSalary)));
        this.SummaryItems.Add(new SummaryItem("Total Yearly Income (after Taxes)",
            FormatMoney(Budget.TotalYearlyIncomes)));
        this.SummaryItems.Add(new SummaryItem("Total Yearly Expenses", FormatMoney(Budget.TotalYearlyExpenses)));
        this.SummaryItems.Add(new SummaryItem("Total Yearly Difference",
            FormatMoney(Budget.TotalYearlyIncomes - Budget.TotalYearlyExpenses)));
        this.SummaryItems.Add(new SummaryItem("Total Monthly Income", FormatMoney(Budget.TotalMonthlyIncomes)));
        this.SummaryItems.Add(new SummaryItem("Total Monthly Expenses", FormatMoney(Budget.TotalMonthlyExpenses)));
        this.SummaryItems.Add(new SummaryItem("Total Monthly Difference",
            FormatMoney(Budget.TotalMonthlyIncomes - Budget.TotalMonthlyExpenses)));
        this.SummaryItems.Add(new SummaryItem("Half Month Expense", FormatMoney(Budget.HalfMonthlyExpenses)));
        this.SummaryItems.Add(new SummaryItem("Debt to Income Ratio", Budget.Debt_Income_Ratio.ToString("P2")));
    }

    private string FormatMoney(decimal value)
    {
        return value.ToString("C", new CultureInfo("en-US"));
    }

    private void NavigateToBudget()
    {
        NavigationManager.NavigateTo($"budget/view/{BudgetId}");
    }
    
}

public record SummaryItem(string Title, string Value);

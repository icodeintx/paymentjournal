using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

/// <summary>
///
/// </summary>
public partial class Budget_View : ComponentBase
{
    public Budget Budget { get; set; } = new();

    [Parameter]
    public string BudgetId { get; set; } = string.Empty;

    public bool Disabled { get; set; } = true;

    public bool EditMode
    {
        get { return !Disabled; }
        set
        {
            Disabled = !value;
        }
    }

    public string Message { get; set; } = string.Empty;

    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private BudgetRepo Repo { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="budgetId"></param>
    public async void DeleteBudget(Guid budgetId)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this BUDGET?");
        if (confirmed)
        {
            DbResult result = Repo.DeleteBudget(budgetId);
            NavigationManager.NavigateTo("/budgets", true);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public void HandleSubmit()
    {
        DbResult result = Repo.SaveBudget(Budget);
        NavigationManager.NavigateTo($"/budget/view/{Budget.BudgetId}", true);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public string IsTrueToYesNo(bool item)
    {
        return item == true ? "Yes" : "No";
    }

    public void ToggleEnabled()
    {
        Disabled = Disabled == true ? false : true;
    }

    /// <summary>
    ///
    /// </summary>
    protected void AddBankAccount()
    {
        Budget.BankAccounts.Add(new BankAccount());
    }

    /// <summary>
    ///
    /// </summary>
    protected void AddCreditCard()
    {
        Budget.CreditCards.Add(new CreditCard());
    }

    /// <summary>
    ///
    /// </summary>
    protected void AddExpense()
    {
        Budget.Expenses.Add(new Expense());
    }

    /// <summary>
    ///
    /// </summary>
    protected void AddIncome()
    {
        Budget.Incomes.Add(new Income());
    }

    /// <summary>
    ///
    /// </summary>
    protected void AddOnlineService()
    {
        Budget.OnlineServices.Add(new OnlineService());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteBankAccount(BankAccount model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");
        if (confirmed)
        {
            return Budget.BankAccounts.Remove(model);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteCreditCard(CreditCard model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");
        if (confirmed)
        {
            return Budget.CreditCards.Remove(model);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteExpense(Expense model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");
        if (confirmed)
        {
            return Budget.Expenses.Remove(model);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteIncome(Income model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");
        if (confirmed)
        {
            return Budget.Incomes.Remove(model);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteOnlineService(OnlineService model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");
        if (confirmed)
        {
            return Budget.OnlineServices.Remove(model);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
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
                    Disabled = false;
                }

                Budget = Repo.GetBudget(Guid.Parse(BudgetId));
                if (Budget is null)
                {
                    //if budgetId was not found create a new budget
                    Message = "No Budget Found.  Creating New Budget.";
                    Budget = new();
                }
            }
            catch (Exception ex)
            {
                //if any errors were encountered, create a new budget
                Message = $"Error retrieving Budget. {ex.Message} ";
                Budget = new Budget();
            }
        }
    }

    protected void Payments(Guid budgetId)
    {
        NavigationManager.NavigateTo($"payments/{budgetId}");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private string FormatMoney(decimal value)
    {
        return value.ToString("0.00");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private string FormatPercent(decimal value)
    {
        return value.ToString("0.0");
    }
}
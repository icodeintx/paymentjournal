using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using PaymentJournal_Web.Models;
using PaymentJournal_Web.Repositories;

namespace PaymentJournal_Web.Pages.Components;

/// <summary>
///
/// </summary>
public partial class Budget_View : ComponentBase
{
    public Budget Budget { get; set; } = new();

    [Parameter]
    public string BudgetId { get; set; } = string.Empty;

    public bool Disabled { get; set; } = true;
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
            NavigationManager.NavigateTo("/budget", true);
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

    public void ToggleDisabled()
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
            //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
            Budget = Repo.GetLatestBudget();

            if (Budget is null)
            {
                Message = "No Budget Found.  Creating New Budget.";
                Budget = new();
            }
        }
        else
        {
            try
            {
                Budget = Repo.GetBudget(Guid.Parse(BudgetId));

                if (Budget is null)
                {
                    Message = "No Budget Found.  Creating New Budget.";
                    Budget = new();
                }
            }
            catch (Exception ex)
            {
                //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
                Message = $"Error retrieving Budget. {ex.Message} ";
                Budget = new Budget();
            }
        }
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
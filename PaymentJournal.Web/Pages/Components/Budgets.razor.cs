using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

public partial class Budgets : ComponentBase
{
    [Parameter]
    public string BudgetId { get; set; } = string.Empty;

    public List<Budget> BudgetList { get; set; } = new();
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
            NavigationManager.NavigateTo("/budgets", true);
        }
    }

    public void ToggleDisabled()
    {
        Disabled = Disabled == true ? false : true;
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BudgetList = Repo.GetAllBudgets();

        if (BudgetList is null)
        {
            Message = "No Budget Found.  Creating New Budget.";
            BudgetList = new();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="budgetId"></param>
    protected void ViewBudget(Guid budgetId)
    {
        NavigationManager.NavigateTo($"/budget/view/{budgetId}");
    }

    /// <summary>
    ///
    /// </summary>
    private void CreateNewBudget()
    {
        NavigationManager.NavigateTo($"/budget/view/{Guid.Empty}");
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
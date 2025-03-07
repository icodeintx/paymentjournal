using Microsoft.AspNetCore.Components;
using MudBlazor;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

public partial class Budgets : ComponentBase
{
    [Parameter] public string BudgetId { get; set; } = string.Empty;

    public List<Budget> BudgetList { get; set; } = new();

    public bool Disabled { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    [Inject] private CacheRepo CacheRepo { get; set; } = null !;

    [Inject] private IJSRuntime JSRuntime { get; set; } = null !;

    [Inject] private NavigationManager NavigationManager { get; set; } = null !; 

    [Inject] private BudgetRepo Repo { get; set; } = null !;

    public async void DeleteBudget(Guid budgetId)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Payment Item? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result != null && result.Canceled)
        {
            return;
        }
        else if (result != null && result.Data != null && (bool)result.Data == true)
        {
            DbResult dbresult = Repo.DeleteBudget(budgetId);
            NavigationManager.NavigateTo("/budgets", true);
        }
    }

    public void ToggleDisabled()
    {
        Disabled = Disabled == true ? false : true;
    }

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

    protected void ViewBudget(Guid budgetId)
    {
        NavigationManager.NavigateTo($"/budget/view/{budgetId}");
    }

    private void CreateNewBudget()
    {
        NavigationManager.NavigateTo($"/budget/view/{Guid.Empty}");
    }


    private string FormatMoney(decimal value)
    {
        return value.ToString("0.00");
    }

    private string FormatPercent(decimal value)
    {
        return value.ToString("0.0");
    }
}
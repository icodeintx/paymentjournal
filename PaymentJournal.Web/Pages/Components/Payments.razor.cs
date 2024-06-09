using Microsoft.AspNetCore.Components;
using MudBlazor;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

/// <summary>
///
/// </summary>
public partial class Payments : ComponentBase
{
    public Payments()
    {
    }

    public Budget Budget { get; set; }

    [Parameter]
    public string BudgetId { get; set; }

    public List<PaymentItem> PaymentItems { get; set; }

    protected string GetReturnURL => $"/payments/{BudgetId}";

    [Inject]
    private BudgetRepo budgetRepo { get; set; }

    [Inject]
    private CacheRepo CacheRepo { get; set; }

    [Inject] private ILogger<Payments> Logger { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private PaymentRepo repo { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    protected async Task DeleteDocument(PaymentItem model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Payment Item? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result.Cancelled)
        {
            return;
        }
        else
        if (result.Data != null && (bool)result.Data == true)
        {
            PaymentItems.Remove(model);
            repo.DeleteDocument(model.PaymentItemId);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task EditDocument(PaymentItem model)
    {
        NavigationManager.NavigateTo($"/payment/edit/{model.PaymentItemId.ToString()}");
    }

    protected void NavigateToBudget()
    {
        NavigationManager.NavigateTo($"/budget/view/{this.BudgetId}");
    }

    protected void NavToPayments()
    {
        NavigationManager.NavigateTo($"/payment/insert/{BudgetId}");
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(BudgetId))
        {
            Budget = budgetRepo.GetBudget(Guid.Parse(BudgetId));
        }

        var appState = CacheRepo.GetAppState();

        var useAppStateDate = UseAppStateDate(appState);

        if (useAppStateDate)
        {
            PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), appState.MonthYear.Month, appState.MonthYear.Year);
        }
        else
        {
            PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), DateTime.Now.Month, DateTime.Now.Year);
        }
    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    private void Search(MonthYear model)
    {
        Logger.LogInformation($"Class ({nameof(Payments)}) Method ({nameof(Search)}) started.");

        PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), model.Month, model.Year);
    }

    private bool UseAppStateDate(AppState appState)
    {
        try
        {
            if (appState.MonthYear == null || appState.MonthYear.Month == 0 || appState.MonthYear.Year == 0)
            {
                return false;
            }
            else
            {
                if (appState.MonthYear.LastSetDate != DateOnly.FromDateTime(DateTime.Now))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
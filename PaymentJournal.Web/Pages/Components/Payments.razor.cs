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
    /// <summary>
    ///
    /// </summary>
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

    [Inject]
    private NavigationManager navigationManager { get; set; }

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
        navigationManager.NavigateTo($"/payment/edit/{model.PaymentItemId.ToString()}");
    }

    protected void NavToPayments()
    {
        navigationManager.NavigateTo($"/payment/insert/{BudgetId}");
    }

    protected override void OnInitialized()
    {
        var x = 1;
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

        //check if we have Month and Year
        if (appState.MonthYear == null || appState.MonthYear.Month == 0 || appState.MonthYear.Year == 0)
        {
            //one or both of the parameters above are blank so use this month and year
            PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), DateTime.Now.Month, DateTime.Now.Year);
        }
        else
        {
            try
            {
                appState.test = 11;
                //Monty and Year were passed, try to parse them and use them.
                PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), appState.MonthYear.Month, appState.MonthYear.Year);
            }
            catch
            {
                //the parsing failed above so use this month/year
                PaymentItems = repo.GetItemsByMonthYear(Guid.Parse(BudgetId), DateTime.Now.Month, DateTime.Now.Year);
            }
        }
    }
}
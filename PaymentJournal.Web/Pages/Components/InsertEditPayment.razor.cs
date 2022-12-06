using Microsoft.AspNetCore.Components;
using MudBlazor;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;
using System.Reflection;

namespace PaymentJournal.Web.Pages.Components;

public partial class InsertEditPayment : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public InsertEditPayment()
    {
        PaymentItem = new PaymentItem();
    }

    public Budget Budget { get; set; }

    [Parameter]
    public string BudgetId { get; set; }

    [Inject]
    public BudgetRepo BudgetRepo { get; set; }

    public bool HasPayees => PaymentItem.Payees.Count > 0;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public PaymentItem PaymentItem { get; set; }

    [Parameter]
    public string PaymentItemId { get; set; } = string.Empty;

    [Inject]
    public PaymentRepo PaymentRepo { get; set; }

    /// <summary>
    ///
    /// </summary>
    public void AddNewPayee()
    {
        PaymentItem.Payees.Add(
            new Payee()
            {
                PayeeId = Guid.NewGuid(),
                Date = DateTime.Now
            });
    }

    /// <summary>
    ///
    /// </summary>
    public void HandleSubmit()
    {
        DbResult result;

        if (PaymentItem.PaymentItemId == Guid.Empty)
        {
            //we have no valid ID so insert document
            result = PaymentRepo.InsertDocument(PaymentItem);
        }
        else
        {
            //we have valid ID so update document
            result = PaymentRepo.UpdateDocument(PaymentItem);
        }

        if (result.Success)
        {
            NavigationManager.NavigateTo($"/payments/{BudgetId}", true);
            //PaymentItem = new PaymentItem();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public bool IsValidPaymentItemId()
    {
        try
        {
            var tmpguid = Guid.Parse(PaymentItemId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="item"></param>
    public async Task RemovePayee(Payee item)
    {

        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete item {item.Name}? This process cannot be undone after clicking SAVE.");
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
            PaymentItem.Payees.Remove(item);
        }

        //PaymentItem.Payees.Remove(item);
    }

    /// <summary>
    ///
    /// </summary>
    protected void NavToPayments()
    {
        NavigationManager.NavigateTo($"/payments/{BudgetId}", true);
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(PaymentItemId) == false)
        {
            PaymentItem = PaymentRepo.GetItemsById(Guid.Parse(PaymentItemId));
            BudgetId = PaymentItem.BudgetId.ToString();
        }
        else
        {
            //if we have BudgetId then create a new PaymentItem
            if (!string.IsNullOrWhiteSpace(BudgetId))
            {
                PaymentItem = new PaymentItem();
                PaymentItem.BudgetId = Guid.Parse(BudgetId);
            }
            else
            {
                throw new NullReferenceException(nameof(BudgetId));
            }
        }

        //if we have a budgetID then load the budget repo
        if (!string.IsNullOrWhiteSpace(BudgetId))
        {
            Budget = BudgetRepo.GetBudget(Guid.Parse(BudgetId));
        }
    }
}
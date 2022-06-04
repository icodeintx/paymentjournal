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

public partial class InsertEditPayment : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public InsertEditPayment()
    {
        PaymentItem = new PaymentItem();
    }

    public int Counter { get; set; } = 0;

    [Inject]
    public PaymentRepo DB { get; set; }

    public bool HasPayees => PaymentItem.Payees.Count > 0;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public PaymentItem PaymentItem { get; set; }

    [Parameter]
    public string PaymentItemId { get; set; } = string.Empty;

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
            result = DB.InsertDocument(PaymentItem);
        }
        else
        {
            //we have valid ID so update document
            result = DB.UpdateDocument(PaymentItem);
        }

        if (result.Success)
        {
            NavigationManager.NavigateTo("/payments", true);
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
    public void RemovePayee(Payee item)
    {
        PaymentItem.Payees.Remove(item);
    }

    /// <summary>
    ///
    /// </summary>
    protected void NavToPayments()
    {
        NavigationManager.NavigateTo("/payments", true);
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(PaymentItemId) == false)
        {
            PaymentItem = DB.GetItemsById(Guid.Parse(PaymentItemId));
        }
        else
        {
            PaymentItem = new PaymentItem();
        }
    }
}
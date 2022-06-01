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

public partial class EditPaymentItem : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public EditPaymentItem()
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
    public string PaymentItemId { get; set; }

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
        if (!string.IsNullOrEmpty(PaymentItem.Note))
        {
            //there is a note so go from there.
            var result = DB.UpdateDocument(PaymentItem);

            if (result.Success)
            {
                //NavigationManager.NavigateTo("/insert", true);
                PaymentItem = new PaymentItem();

                NavigationManager.NavigateTo("/payments");
            }
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
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (PaymentItemId != String.Empty)
        {
            PaymentItem = DB.GetItemsById(Guid.Parse(PaymentItemId));
        }
    }
}
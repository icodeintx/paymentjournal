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

public partial class InsertPaymentItem : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public InsertPaymentItem()
    {
        PaymentItem = new PaymentItem();
    }

    public int Counter { get; set; } = 0;

    [Inject]
    public LiteDbRepo DB { get; set; }

    public bool HasPayees => PaymentItem.Payees.Count > 0;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public PaymentItem PaymentItem { get; set; }

    /// <summary>
    ///
    /// </summary>
    public void AddNewPayee()
    {
        PaymentItem.Payees.Add(
            new Payee()
            {
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
            var result = DB.InsertDocument(PaymentItem);

            if (result.Success)
            {
                //NavigationManager.NavigateTo("/insert", true);
                PaymentItem = new PaymentItem();
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
}
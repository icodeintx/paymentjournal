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

namespace PaymentJournal_Web.Pages;

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

    public List<PaymentItem> PaymentItems { get; set; }

    [Inject]
    private LiteDbRepo repo { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
    }
}
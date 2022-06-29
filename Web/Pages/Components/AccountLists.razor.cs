using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Pages.Components;

public partial class AccountLists : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public AccountLists()
    {
    }

    [Parameter]
    public List<AccountList> Accounts { get; set; } = new();
}
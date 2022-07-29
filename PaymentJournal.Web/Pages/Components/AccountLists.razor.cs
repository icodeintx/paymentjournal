using Microsoft.AspNetCore.Components;
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
    public List<AccountList> AccountsList { get; set; } = new();
}
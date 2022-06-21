using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

public partial class AccountLists : ComponentBase
{
    /// <summary>
    ///
    /// </summary>
    public AccountLists()
    {
    }

    public List<AccountLists> Account { get; set; } = new();
}
using Microsoft.AspNetCore.Components;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Pages.Components;

public partial class AccountLists : ComponentBase
{
    public AccountLists()
    {
    }

    [Parameter]
    public List<AccountList> AccountsList { get; set; } = new();
}
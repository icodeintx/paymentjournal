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

public partial class MonthYearSelector : ComponentBase
{
    public MonthYear MonthYearModel { get; set; } = new MonthYear();

    [Parameter]
    public string ReturnURL { get; set; } = "";

    public IEnumerable<int> Years { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    [Inject]
    private PaymentRepo repo { get; set; }

    public void HandleValidSubmit()
    {
        var redirectTo = $"{ReturnURL}/{MonthYearModel.Month}/{MonthYearModel.Year}";
        Nav.NavigateTo(redirectTo);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Years = repo.GetDistintYears();
    }
}
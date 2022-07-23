using Microsoft.AspNetCore.Components;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

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
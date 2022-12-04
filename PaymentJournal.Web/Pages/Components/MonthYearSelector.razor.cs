using Microsoft.AspNetCore.Components;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

public partial class MonthYearSelector : ComponentBase
{
    [Parameter]
    public string ReturnURL { get; set; } = "";

    public IEnumerable<int> Years { get; set; }

    private AppState AppState { get; set; }

    [Inject]
    private CacheRepo CacheRepo { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    [Inject]
    private PaymentRepo repo { get; set; }

    public void HandleValidSubmit()
    {
        CacheRepo.SaveAppState(AppState);

        var redirectTo = $"{ReturnURL}";
        Nav.NavigateTo(redirectTo, true);
    }

    protected override void OnParametersSet()
    {
        AppState = CacheRepo.GetAppState();

        if (Years == null)
        {
            Years = repo.GetDistintYears();
        }
    }
}
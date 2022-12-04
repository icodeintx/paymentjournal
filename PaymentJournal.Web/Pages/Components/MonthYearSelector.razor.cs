using Microsoft.AspNetCore.Components;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

public partial class MonthYearSelector : ComponentBase
{
    [Parameter]
    public EventCallback<MonthYear> OnSearch { get; set; }

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

    public async Task MonthClosed()
    {
        CacheRepo.SaveAppState(AppState);
        await OnSearch.InvokeAsync(AppState.MonthYear);
    }

    public async Task SetToday()
    {
        AppState.MonthYear.Month = DateTime.Now.Month;
        AppState.MonthYear.Year = DateTime.Now.Year;
        await MonthClosed();
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
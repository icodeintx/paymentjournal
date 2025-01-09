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

    public async Task Persist()
    {
        AppState.MonthYear.LastSetDate = DateOnly.FromDateTime(DateTime.Now);
        CacheRepo.SaveAppState(AppState);
        await OnSearch.InvokeAsync(AppState.MonthYear);
    }

    public async Task SetToday()
    {
        AppState.MonthYear.Month = DateTime.Now.Month;
        AppState.MonthYear.Year = DateTime.Now.Year;
        AppState.MonthYear.LastSetDate = DateOnly.FromDateTime(DateTime.Now);
        await Persist();
    }

    protected override void OnParametersSet()
    {
        AppState = CacheRepo.GetAppState();

        if (Years == null)
        {
            Years = repo.GetDistintYears();
        }
    }

    public async Task Increment()
    {
        if (AppState.MonthYear.Month == 12)
        {
            AppState.MonthYear.Month = 1;
            AppState.MonthYear.Year++;
        }
        else
        {
            AppState.MonthYear.Month++;
        }
        await Persist();
    }

    public async Task Decrement()
    {
        if (AppState.MonthYear.Month == 1)
        {
            AppState.MonthYear.Month = 12;
            AppState.MonthYear.Year--;
        }
        else
        {
            AppState.MonthYear.Month--;
        }
        await Persist();
    }
}
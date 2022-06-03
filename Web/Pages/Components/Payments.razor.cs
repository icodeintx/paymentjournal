using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;

namespace PaymentJournal.Web.Pages.Components;

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

    [Parameter]
    public string Month { get; set; }

    public List<PaymentItem> PaymentItems { get; set; }

    [Parameter]
    public string Year { get; set; }

    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    [Inject]
    private PaymentRepo repo { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    protected async Task DeleteDocument(PaymentItem model)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");

        if (confirmed)
        {
            //remove from gui
            PaymentItems.Remove(model);

            repo.DeleteDocument(model.PaymentItemId);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected async Task EditDocument(PaymentItem model)
    {
        navigationManager.NavigateTo($"/edit/{model.PaymentItemId.ToString()}");
    }

    protected void NavToPayments()
    {
        navigationManager.NavigateTo($"/insert");
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    /// <summary>
    ///
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        //check if we have Month and Year
        if (string.IsNullOrWhiteSpace(Month) || string.IsNullOrWhiteSpace(Year))
        {
            //one or both of the parameters above are blank so use this month and year
            PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
        }
        else
        {
            try
            {
                //Monty and Year were passed, try to parse them and use them.
                PaymentItems = repo.GetItemsByMonthYear(int.Parse(Month), int.Parse(Year));
            }
            catch
            {
                //the parsing failed above so use this month/year
                PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
            }
        }
    }
}
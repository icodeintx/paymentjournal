﻿using System.Net.Http;
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

    /// <summary>
    /// Property to indicate if we are searching by ALL entries or not
    /// </summary>
    public bool IsAll => string.IsNullOrEmpty(Month) && string.IsNullOrEmpty(Year);

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
    private LiteDbRepo repo { get; set; }

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
        if (IsAll)
        {
            //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
            PaymentItems = repo.GetAllItems();
        }
        else
        {
            try
            {
                PaymentItems = repo.GetItemsByMonthYear(int.Parse(Month), int.Parse(Year));
            }
            catch
            {
                //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
                PaymentItems = repo.GetAllItems();
            }
        }
    }
}
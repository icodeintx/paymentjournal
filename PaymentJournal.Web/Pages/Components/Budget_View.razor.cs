using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PaymentJournal.Web.Models;
using PaymentJournal.Web.Repositories;
using System.Globalization;

namespace PaymentJournal.Web.Pages.Components;

public partial class Budget_View : ComponentBase
{
    public Budget Budget { get; set; } = new();

    [Parameter]
    public string BudgetId { get; set; } = string.Empty;

    public bool Disabled { get; set; } = true;

    public bool EditMode
    {
        get { return !Disabled; }
        set
        {
            Disabled = !value;
        }
    }

    public string Message { get; set; } = string.Empty;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null !;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null !;

    [Inject]
    private BudgetRepo Repo { get; set; } = null !;

   public async void DeleteBudget(Guid budgetId)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Budget {this.Budget.Name}? This process cannot be undone.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result is {Canceled:true})
        {
            return;
        }
        else
        if (result?.Data is true)
        {
            DbResult deleteResult = Repo.DeleteBudget(budgetId);
            NavigationManager.NavigateTo("/budgets", true);
        }
    }

    public void HandleSubmit()
    {
        DbResult result = Repo.SaveBudget(Budget);
        NavigationManager.NavigateTo($"/budget/view/{Budget.BudgetId}", true);
    }

  public string IsTrueToYesNo(bool item)
    {
        return item == true ? "Yes" : "No";
    }

    public void ToggleEnabled()
    {
        Disabled = Disabled != true;
    }

    protected void AddBankAccount()
    {
        Budget.BankAccounts.Add(new BankAccount());
    }

    protected void AddCreditCard()
    {
        Budget.CreditCards.Add(new CreditCard());
    }

    protected void AddExpense()
    {
        Budget.Expenses.Add(new Expense());
    }

    protected void AddIncome()
    {
        Budget.Incomes.Add(new Income());
    }

    protected void AddOnlineService()
    {
        Budget.OnlineServices.Add(new OnlineService());
    }

    protected async Task<bool> DeleteBankAccount(BankAccount model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Bank Account {model.Bank}? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result is {Canceled:true})
        {
            return false;
        }
        else
        if (result?.Data is true)
        {
            return Budget.BankAccounts.Remove(model);
        }
        else
        {
            return false;
        }
    }

    protected async Task<bool> DeleteCreditCard(CreditCard model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Card {model.Holder}? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result is {Canceled:true})
        {
            return false;
        }
        else
        if (result?.Data is true)
        {
            return Budget.CreditCards.Remove(model);
        }
        else
        {
            return false;
        }
    }

    protected async Task<bool> DeleteExpense(Expense model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Monthy Expense {model.BillName}? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result == null)
        {
            return false;
        }

        if (result.Canceled)
        {
            return false;
        }
        else
        if (result.Data != null && (bool)result.Data == true)
        {
            return Budget.Expenses.Remove(model);
        }
        else
        {
            return false;
        }
    }

    protected async Task<bool> DeleteIncome(Income model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Income {model.Employer}? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result is {Canceled:true})
        {
            return false;
        }
        else
        if (result?.Data is true)
        {
            return Budget.Incomes.Remove(model);
        }
        else
        {
            return false;
        }
    }

    protected async Task<bool> DeleteOnlineService(OnlineService model)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", $"Delete Online Service {model.Service}? This process cannot be undone after clicking SAVE.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<SimpleDialog>("Delete", parameters, options);
        var result = await dialog.Result;

        if (result is {Canceled:true})
        {
            return false;
        }
        else
        if (result.Data != null && (bool)result.Data == true)
        {
            return Budget.OnlineServices.Remove(model);
        }
        else
        {
            return false;
        }
    }

    protected void NavigateToBudgets()
    {
        NavigationManager.NavigateTo($"/budgets");
    }

    protected void NavigateToPrint(Guid budgetId)
    {
        NavigationManager.NavigateTo($"budget/print/{budgetId}");
    }


    protected void NavigateToPayments(Guid budgetId)
    {
        NavigationManager.NavigateTo($"payments/{budgetId}");
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (string.IsNullOrEmpty(BudgetId))
        {
            //if no budgetID is passed as parameter Get the latest budget

            //PaymentItems = repo.GetItemsByMonthYear(DateTime.Now.Month, DateTime.Now.Year);
            Budget = Repo.GetLatestBudget();

            if (Budget is null)
            {
                //if no latest budget found then create a new budget
                Message = "No Budget Found.  Creating New Budget.";
                Budget = new();
            }
            else
            {
                Message = string.Empty;
            }
        }
        else
        {
            //BudgetId was passed as parameter, lookup the budget and display
            try
            {
                //if BudgetId pass as parameter is Guid Default then create new budget
                if (Guid.Parse(BudgetId) == Guid.Empty)
                {
                    Budget = new();
                    Disabled = false;
                }

                Budget = Repo.GetBudget(Guid.Parse(BudgetId));
                if (Budget is null)
                {
                    //if budgetId was not found create a new budget
                    Message = "No Budget Found.  Creating New Budget.";
                    Budget = new();
                }
                else
                {
                    Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                //if any errors were encountered, create a new budget
                Message = $"Error retrieving Budget. {ex.Message} ";
                Budget = new Budget();
            }
        }
    }

   private string FormatMoney(decimal value)
    {
        return value.ToString("C", new CultureInfo("en-US"));
    }

    private string FormatPercent(decimal value)
    {
        return value.ToString("0.0");
    }
}
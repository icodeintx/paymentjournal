using Newtonsoft.Json;

namespace PaymentJournal.Web.Models;

public class PaymentItem
{
    public Guid BudgetId { get; set; } = Guid.Empty;

    public DateTime? CreateDate { get; set; } = DateTime.Now;

    public string Note { get; set; } = string.Empty;

    public List<Payee> Payees { get; set; } = new List<Payee>();

    public Guid PaymentItemId { get; set; } = Guid.Empty;

    public override string ToString()
    {
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        return json;
    }
}
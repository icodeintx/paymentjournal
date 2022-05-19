using Newtonsoft.Json;

namespace PaymentJournal_Web.Models;

public class PaymentItem
{
    public DateTime CreateDate { get; set; }
    public string Note { get; set; } = string.Empty;
    public List<Payee> Payees { get; set; } = new List<Payee>();
    public Guid PaymentItemId { get; set; }

    public override string ToString()
    {
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        return json;
    }
}
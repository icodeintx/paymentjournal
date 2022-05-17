namespace PaymentJournal.Web.Models;

public class PaymentItem
{
    public DateTime CreateDate { get; set; }
    public string Note { get; set; } = string.Empty;
    public List<Payee> Payees { get; set; } = new List<Payee>();
}
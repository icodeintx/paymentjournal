namespace PaymentJournal_Web.Models;

public class Payee
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; } = string.Empty;
}
namespace PaymentJournal_Web.Models;

public class Payee
{
    public decimal Amount { get; set; }
    public decimal AmountFormatted => decimal.Parse(Amount.ToString("0.00"));
    public DateTime Date { get; set; }
    public string Name { get; set; } = string.Empty;
}
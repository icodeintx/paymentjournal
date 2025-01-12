namespace PaymentJournal.Web.Models;

public class BankAccount
{
    public string AccountNumber { get; set; }
    public string Bank { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
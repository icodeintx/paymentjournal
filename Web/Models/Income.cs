namespace PaymentJournal_Web.Models;

public class Income
{
    public decimal Amount { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string Employer { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
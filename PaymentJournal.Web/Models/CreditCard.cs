namespace PaymentJournal.Web.Models;

public class CreditCard
{
    public string AccountNumber { get; set; } = string.Empty;
    public string AttachedTo { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public string Holder { get; set; } = string.Empty; //visa etc.
    public bool IsDebt { get; set; } = false;
}
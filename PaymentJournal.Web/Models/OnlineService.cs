namespace PaymentJournal.Web.Models;

public class OnlineService
{
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string PaidTo { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
}
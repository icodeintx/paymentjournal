namespace PaymentJournal.Service.Models;

public class OnlineService
{
    public DateTime CreateDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last 4 of CC or Bank account number
    /// </summary>
    public string PaidTo { get; set; }

    /// <summary>
    /// payPal, Youtube, etc.
    /// </summary>
    public string Service { get; set; } = string.Empty;
}
namespace PaymentJournal.Web.Models;

public class AppState
{
    public AppState()
    {
        if (MonthYear == null)
        {
            MonthYear = new MonthYear();
        }
    }

    public Guid AppStateId { get; set; }
    public MonthYear MonthYear { get; set; }
}
namespace PaymentJournal.Web.Models;

public class AppState
{
    public AppState()
    {
        //test = 9;

        if (MonthYear == null)
        {
            MonthYear = new MonthYear();
        }
    }

    public Guid AppStateId { get; set; }

    public MonthYear MonthYear { get; set; }

    //public int test { get; set; }
}
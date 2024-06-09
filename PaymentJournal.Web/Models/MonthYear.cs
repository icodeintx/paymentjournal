namespace PaymentJournal.Web.Models
{
    public class MonthYear
    {
        public MonthYear()
        {
            if (this.Month == 0)
            {
                Month = DateTime.Now.Month;
            }

            if (this.Year == 0)
            {
                Year = DateTime.Now.Year;
            }
        }

        public DateOnly LastSetDate { get; set; }

        //[BindProperty]
        public int Month { get; set; }

        //[BindProperty]
        public int Year { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace PaymentJournal.Web.Models
{
    public class MonthYear
    {
        public MonthYear()
        {
            //CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month);
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }

        [BindProperty]
        public int Month { get; set; }

        [BindProperty]
        public int Year { get; set; }
    }
}
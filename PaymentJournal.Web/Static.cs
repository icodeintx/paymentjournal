using System.Globalization;

namespace PaymentJournal.Web
{
    public static class Static
    {
        public static string FormatMoney(decimal value)
        {
            return value.ToString("C", new CultureInfo("en-US"));
        }
    }
}
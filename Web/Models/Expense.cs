namespace PaymentJournal_Web.Models;

public class Expense
{
    public decimal Amount { get; set; }
    public string BillName { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public int EstimatedDueDay { get; set; } = 1;

    //group by this property to determin how much is paid by seperate accounts.
    public int PaidByAccountNumber { get; set; }

    public string PaidTo { get; set; } = string.Empty;
}
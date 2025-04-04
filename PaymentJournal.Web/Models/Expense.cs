﻿namespace PaymentJournal.Web.Models;

public class Expense
{
    public decimal Amount { get; set; }
    public string BillName { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public int EstimatedDueDay { get; set; } = 1;
    public string Note { get; set; } = string.Empty;
    //group by this property to determin how much is paid by seperate accounts.
    public string PaidBy { get; set; }  = null !;
    public string PaidTo { get; set; } = string.Empty;
}
namespace PaymentJournal_Web.Models;

public class BankAccount
{
    /// <summary>
    /// Usually last 4
    /// </summary>
    public int AccountNumber { get; set; }

    /// <summary>
    /// Bank Name - USAA
    /// </summary>
    public string Bank { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Bills acct etc.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Type of acct Checking/Savings
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
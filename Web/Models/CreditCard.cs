namespace PaymentJournal_Web.Models;

public class CreditCard
{
    /// <summary>
    /// usually last 4
    /// </summary>
    public string AccountNumber { get; set; }

    /// <summary>
    /// usually last 4
    /// </summary>
    public string AttachedTo { get; set; }

    /// <summary>
    /// Name of Bank - USAA / Chase
    /// </summary>
    public string Bank { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; } = DateTime.Now;
    //acct of bank if debt

    //USAA

    /// <summary>
    /// Description - User Defined
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Holder of the CC - Visa, MasterCard
    /// </summary>
    public string Holder { get; set; } = string.Empty; //visa etc.

    /// <summary>
    /// True if this is a debt card
    /// </summary>
    public bool IsDebt { get; set; } = false;
}
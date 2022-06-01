using LiteDB;
using PaymentJournal_Web.Models;

namespace PaymentJournal_Web.Repositories;

/// <summary>
///
/// </summary>
public class BudgetRepo : BaseRepo
{
    private string DBCollection = "Budget";

    /// <summary>
    ///
    /// </summary>
    public BudgetRepo(string connectionString) : base(connectionString)
    {
    }

    public LiteDatabase Database { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbResult DeleteBudget(Guid budgetId)
    {
        try
        {
            base.DeleteDocument<PaymentItem>(budgetId, DBCollection);

            return new DbResult()
            {
                Success = true,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new DbResult()
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    /// <summary>
    /// Get all budges in descending order
    /// </summary>
    /// <returns></returns>
    public List<Budget> GetAllBudgets()
    {
        try
        {
            var result = base.GetCollectionList<Budget>(DBCollection)
                .OrderByDescending(y => y.CreateDate).ToList();

            return result;
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="paymentItemId"></param>
    /// <returns></returns>
    public Budget GetBudget(Guid budgetId)
    {
        try
        {
            var result = base.GetDocumentById<Budget>(budgetId, DBCollection);

            return result;
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    /// <summary>
    /// Get last Budget Created
    /// </summary>
    /// <returns></returns>
    public Budget GetLatestBudget()
    {
        try
        {
            var result = base.GetCollectionList<Budget>(DBCollection)
                .OrderByDescending(y => y.LastSavedDate).FirstOrDefault();

            return result;
        }
        catch (Exception ex)
        {
            return new Budget();
            //for now just throw the error
            //throw;
        }
    }

    /// <summary>
    /// This method does an UPSERT. If wanting to create a new budget, change the BudgetId
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbResult SaveBudget(Budget document)
    {
        try
        {
            //always update the last saved date
            document.LastSavedDate = DateTime.Now;

            //Create new ID if this is a new PaymentItem
            if (document.BudgetId == Guid.Empty)
            {
                document.BudgetId = Guid.NewGuid();
            }

            base.UpsertDocument<Budget>(document, DBCollection);

            return new DbResult()
            {
                Success = true,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new DbResult()
            {
                Success = false,
                Error = ex.Message
            };
        }
    }
}
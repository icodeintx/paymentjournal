using LiteDB;
using PaymentJournal_Web.Models;

namespace PaymentJournal_Web.Repositories;

public class BudgetRepo
{
    private string DatabaseName = "";
    private string DBCollection = "Budget";

    /// <summary>
    ///
    /// </summary>
    public BudgetRepo(string connectionString)
    {
        DatabaseName = connectionString;
    }

    public LiteDatabase Database { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbInsertResult DeleteBudget(Guid budgetId)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(DBCollection);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Delete(budgetId);

                return new DbInsertResult()
                {
                    Success = true,
                    Error = ""
                };
            }
        }
        catch (Exception ex)
        {
            return new DbInsertResult()
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
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<Budget>(DBCollection);

                var results = col.Query().OrderByDescending(y => y.CreateDate).ToList();

                return results;
            }
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
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<Budget>(DBCollection);

                var results = col.Query()
                    .Where(x => x.BudgetId == budgetId)
                    .ToList().FirstOrDefault();

                return results;
            }
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
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<Budget>(DBCollection);

                var results = col.Query().OrderByDescending(y => y.CreateDate).ToList().FirstOrDefault();

                return results;
            }
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    /// <summary>
    /// This method does an UPSERT. If wanting to create a new budget, change the BudgetId
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbInsertResult SaveBudget(Budget document)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                //Create new ID if this is a new PaymentItem
                if (document.BudgetId == Guid.Empty)
                {
                    document.BudgetId = Guid.NewGuid();
                }

                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<Budget>(DBCollection);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Upsert(document);

                return new DbInsertResult()
                {
                    Success = true,
                    Error = ""
                };
            }
        }
        catch (Exception ex)
        {
            return new DbInsertResult()
            {
                Success = false,
                Error = ex.Message
            };
        }
    }
}
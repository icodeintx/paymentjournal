using LiteDB;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Repositories;

public class BudgetRepo : BaseRepo<Budget>
{
    private string DBCollection = "Budget";

    public BudgetRepo(string connectionString) : base(connectionString)
    {
    }

    public LiteDatabase Database { get; set; }

    public DbResult DeleteBudget(Guid budgetId)
    {
        try
        {
            base.DeleteDocument(budgetId, DBCollection);

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

    public List<Budget> GetAllBudgets()
    {
        try
        {
            var result = base.GetCollectionList(DBCollection)
                .OrderByDescending(y => y.CreateDate).ToList();

            return result;
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    public Budget GetBudget(Guid budgetId)
    {
        try
        {
            var result = base.GetDocumentById(budgetId, DBCollection);

            return result;
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    public Budget GetLatestBudget()
    {
        try
        {
            var result = base.GetCollectionList(DBCollection)
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

            base.UpsertDocument(document, DBCollection);

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
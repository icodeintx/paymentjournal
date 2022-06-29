using LiteDB;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Repositories;

public class PaymentRepo : BaseRepo
{
    private string DBCollection = "PaymentItems";

    /// <summary>
    ///
    /// </summary>
    public PaymentRepo(string connectionString) : base(connectionString)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbResult DeleteDocument(Guid paymentId)
    {
        try
        {
            var result = base.DeleteDocument<PaymentItem>(paymentId, DBCollection);

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
    ///
    /// </summary>
    /// <returns></returns>
    public List<PaymentItem> GetAllItems(Guid budgetId)
    {
        try
        {
            var results = base.GetCollectionList<PaymentItem>(DBCollection)
                .Where(x => x.BudgetId == budgetId)
                .OrderByDescending(x => x.CreateDate).ToList();

            return results;
        }
        catch
        {
            //for now just throw the error
            throw;
        }
    }

    /// <summary>
    /// Return all the Years in the DB
    /// </summary>
    /// <returns></returns>
    public IEnumerable<int> GetDistintYears()
    {
        try
        {
            //monkey balls
            var years = base.GetCollectionList<PaymentItem>(DBCollection)
                .Select(x => x.CreateDate.Year)
                .ToList().Distinct();

            return years;
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
    /// <param name="date"></param>
    /// <returns></returns>
    public List<PaymentItem> GetItemsByDate(Guid budgetId, DateTime date)
    {
        try
        {
            var results = base.GetCollectionList<PaymentItem>(DBCollection)
                .Where(x => x.CreateDate.Date == date.Date && x.BudgetId == budgetId)
                .OrderBy(x => x.CreateDate)
                .ToList();

            return results;
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
    public PaymentItem GetItemsById(Guid paymentItemId)
    {
        try
        {
            var results = base.GetCollectionList<PaymentItem>(DBCollection)
                .Where(x => x.PaymentItemId == paymentItemId)
                .ToList().FirstOrDefault();

            return results;
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
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public List<PaymentItem> GetItemsByMonthYear(Guid budgetId, int month, int year)
    {
        try
        {
            var results = base.GetCollectionList<PaymentItem>(DBCollection)
                .Where(x => x.CreateDate.Month == month && x.CreateDate.Year == year && x.BudgetId == budgetId)
                .OrderByDescending(x => x.CreateDate)
                .ToList();

            return results;
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
    /// <param name="document"></param>
    /// <returns></returns>
    public DbResult InsertDocument(PaymentItem document)
    {
        try
        {
            //Create new ID if this is a new PaymentItem
            if (document.PaymentItemId == Guid.Empty)
            {
                document.PaymentItemId = Guid.NewGuid();
            }

            //format the decimals correctly
            FixDollarValues(document);

            // Insert new PaymentItem document (Id will be auto-incremented)
            base.InsertDocument<PaymentItem>(document, DBCollection);

            //TODO fix this bellow.  collection is closed
            //base.GetCollection<PaymentItem>(DBCollection).EnsureIndex(x => x.CreateDate);

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
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbResult UpdateDocument(PaymentItem document)
    {
        try
        {
            //format the decimals correctly
            FixDollarValues(document);

            // Insert new PaymentItem document (Id will be auto-incremented)
            base.UpdateDocument<PaymentItem>(document, DBCollection);

            //TODO fix below, the collection is closed
            // Index document using document CreateDate property
            //base.GetCollection<PaymentItem>(DBCollection).EnsureIndex(x => x.CreateDate);

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
    ///
    /// </summary>
    /// <param name="model"></param>
    private void FixDollarValues(PaymentItem model)
    {
        foreach (var payee in model.Payees)
        {
            payee.Amount = decimal.Parse(payee.Amount.ToString("0.00"));
        }
    }
}
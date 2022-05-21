using LiteDB;
using PaymentJournal_Web.Models;

namespace PaymentJournal_Web.Repositories;

public class LiteDbRepo
{
    private string DatabaseName = "";
    private string PaymentItemsCollection = "PaymentItems";

    /// <summary>
    ///
    /// </summary>
    public LiteDbRepo(string connectionString)
    {
        DatabaseName = connectionString;
    }

    public LiteDatabase Database { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<PaymentItem> GetAllItems()
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                var results = col.Query()
                    .OrderBy(x => x.CreateDate)
                    .ToList();

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
    /// Return all the Years in the DB
    /// </summary>
    /// <returns></returns>
    public List<int> GetDistintYears()
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                //monkey balls
                var paymentItems = col.Query().ToList();

                var years = paymentItems.SelectMany(y => y.Payees)
                    .Select(x => x.Date.Year).Distinct().ToList();

                return years;
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
    /// <param name="date"></param>
    /// <returns></returns>
    public List<PaymentItem> GetItemsByDate(DateTime date)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                var results = col.Query()
                    .Where(x => x.CreateDate.Date == date.Date)
                    .OrderBy(x => x.CreateDate)
                    .ToList();

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
    public List<PaymentItem> GetItemsById(Guid paymentItemId)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                var results = col.Query()
                    .Where(x => x.PaymentItemId == paymentItemId)
                    .ToList();

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
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public List<PaymentItem> GetItemsByMonthYear(int month, int year)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                var results = col.Query()
                    .Where(x => x.CreateDate.Month == month && x.CreateDate.Year == year)
                    .OrderBy(x => x.CreateDate)
                    .ToList();

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
    /// <param name="document"></param>
    /// <returns></returns>
    public DbInsertResult InsertDocument(PaymentItem document)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                //set document defaults
                document.CreateDate = DateTime.Now;
                document.PaymentItemId = Guid.NewGuid();

                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Insert(document);

                // Index document using document CreateDate property
                col.EnsureIndex(x => x.CreateDate);

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
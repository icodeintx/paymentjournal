using LiteDB;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Repositories;

public class LiteDbRepo
{
    private string DatabaseName = ".\\data\\paymentjournal.db";
    private string PaymentItemsCollection = "PaymentItems";

    /// <summary>
    ///
    /// </summary>
    public LiteDbRepo()
    {
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
            // Get a collection (or create, if doesn't exist)
            var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

            var results = col.Query()
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
    /// <param name="date"></param>
    /// <returns></returns>
    public List<PaymentItem> GetItemsByDate(DateTime date)
    {
        try
        {
            // Get a collection (or create, if doesn't exist)
            var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

            var results = col.Query()
                .Where(x => x.CreateDate.Date == date.Date)
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
    public DbInsertResult InsertDocument(PaymentItem document)
    {
        try
        {
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
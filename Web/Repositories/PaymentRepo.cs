using LiteDB;
using PaymentJournal_Web.Models;

namespace PaymentJournal_Web.Repositories;

public class PaymentRepo
{
    private string DatabaseName = "";
    private string PaymentItemsCollection = "PaymentItems";

    /// <summary>
    ///
    /// </summary>
    public PaymentRepo(string connectionString)
    {
        DatabaseName = connectionString;
    }

    public LiteDatabase Database { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbInsertResult DeleteDocument(Guid paymentId)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Delete(paymentId);

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
                    .OrderByDescending(x => x.CreateDate)
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
    public IEnumerable<int> GetDistintYears()
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                //monkey balls
                var years = col.Query().Select(x => x.CreateDate.Year).ToList().Distinct();

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
    public PaymentItem GetItemsById(Guid paymentItemId)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                var results = col.Query()
                    .Where(x => x.PaymentItemId == paymentItemId)
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
                    .OrderByDescending(x => x.CreateDate)
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
                //Create new ID if this is a new PaymentItem
                if (document.PaymentItemId == Guid.Empty)
                {
                    document.PaymentItemId = Guid.NewGuid();
                }

                //format the decimals correctly
                FixDollarValues(document);

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

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public DbInsertResult UpdateDocument(PaymentItem document)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                //format the decimals correctly
                FixDollarValues(document);

                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<PaymentItem>(PaymentItemsCollection);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Update(document);

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
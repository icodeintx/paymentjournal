using LiteDB;
using PaymentJournal.Service.Models;

namespace PaymentJournal.Service.Repositories;

public class BaseRepo
{
    protected BaseRepo(string connectionString)
    {
        DatabaseName = connectionString;
    }

    protected LiteDatabase Database { get; set; }
    protected string DatabaseName { get; set; } = String.Empty;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="documentId"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected DbResult DeleteDocument<T>(Guid documentId, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Delete(documentId);

                return new DbResult()
                {
                    Success = true,
                    Error = ""
                };
            }
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
    /// <typeparam name="T"></typeparam>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected ILiteCollection<T> GetCollection<T>(string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var results = Database.GetCollection<T>(collectionName);

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
    /// <typeparam name="T"></typeparam>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected List<T> GetCollectionList<T>(string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                var results = col.Query().ToList();

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
    /// <typeparam name="T"></typeparam>
    /// <param name="documentId"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected T GetDocumentById<T>(Guid documentId, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                var results = col.FindById(documentId);

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
    /// <typeparam name="T"></typeparam>
    /// <param name="document"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected DbResult InsertDocument<T>(T document, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Insert(document);

                return new DbResult()
                {
                    Success = true,
                    Error = ""
                };
            }
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
    /// <typeparam name="T"></typeparam>
    /// <param name="document"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected DbResult UpdateDocument<T>(T document, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Update(document);

                return new DbResult()
                {
                    Success = true,
                    Error = ""
                };
            }
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
    /// <typeparam name="T"></typeparam>
    /// <param name="document"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected DbResult UpsertDocument<T>(T document, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // Insert new PaymentItem document (Id will be auto-incremented)
                col.Upsert(document);

                return new DbResult()
                {
                    Success = true,
                    Error = ""
                };
            }
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
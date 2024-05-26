using LiteDB;
using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Repositories;

/// <summary>
///
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepo<T>
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="connectionString"></param>
    protected BaseRepo(string connectionString)
    {
        DatabaseName = connectionString;
    }

    protected LiteDatabase Database { get; set; }

    protected string DatabaseName { get; set; } = String.Empty;

    /// <summary>
    /// Delete single document
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="documentId"></param>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected DbResult DeleteDocument(Guid documentId, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // Deletes document
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
    /// Currently NOT USED - connection closes after using statement
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected ILiteQueryableResult<T> GetCollection(string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var results = Database.GetCollection<T>(collectionName).Query();

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
    /// Gets all documents in a Collection or Creates a new collection if not exists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    protected List<T> GetCollectionList(string collectionName)
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
    protected T GetDocumentById(Guid documentId, string collectionName)
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
    protected DbResult InsertDocument(T document, string collectionName)
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
    protected DbResult UpdateDocument(T document, string collectionName)
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
    protected DbResult UpsertDocument(T document, string collectionName)
    {
        try
        {
            using (Database = new LiteDatabase(DatabaseName))
            {
                // Get a collection (or create, if doesn't exist)
                var col = Database.GetCollection<T>(collectionName);

                // TODO there is a bug here.  Upsert always returns false
                var result = col.Upsert(document);

                return new DbResult()
                {
                    Success = result,
                    Error = result == false ? $"Class:{nameof(BaseRepo<T>)} Method:{nameof(UpsertDocument)} - Upsert Failed" : ""
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
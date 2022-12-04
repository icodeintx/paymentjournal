using PaymentJournal.Web.Models;

namespace PaymentJournal.Web.Repositories;

public class CacheRepo : BaseRepo<AppState>
{
    private string DBCollection = "AppState";

    public CacheRepo(string connectionString) : base(connectionString)
    {
    }

    public AppState GetAppState()
    {
        //var result = base.GetDocumentById(budgetId, DBCollection);
        var dbResult = base.GetCollectionList(DBCollection)
            .Take(1).FirstOrDefault();

        if (dbResult != null)
        {
            return dbResult;
        }
        else
        {
            return new AppState();
        }
    }

    public bool SaveAppState(AppState appState)
    {
        if (appState.AppStateId == Guid.Empty)
        {
            appState.AppStateId = Guid.NewGuid();
        }

        DbResult dbResult = base.UpsertDocument(appState, DBCollection);

        return dbResult.Success;
    }
}
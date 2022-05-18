using PaymentJournal.Web.Models;

namespace Test;

public class UnitTest_LiteDbRepo
{
    /// <summary>
    ///
    /// </summary>
    [Fact]
    public void CanInsertDocument()
    {
        //arrange
        PaymentJournal.Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");
        PaymentItem item = new PaymentItem()
        {
            Note = "this is my note",
            Payees = new List<Payee> { new Payee()
            {
                Amount = 29.00m,
                Date = DateTime.Now.AddDays(-1),
                Name = "WalMart"
            }
            },
        };

        //act
        var result = repo.InsertDocument(item);

        //assert
        Assert.True(result.Success);
    }

    /// <summary>
    ///
    /// </summary>
    [Fact]
    public void CanReadAllItems()
    {
        //arrange
        PaymentJournal.Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

        //act
        var result = repo.GetAllItems();

        //assert
        Assert.True(result.Count > 0);
    }

    /// <summary>
    ///
    /// </summary>
    [Fact]
    public void CanReadItemsByDate()
    {
        //arrange
        PaymentJournal.Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

        //act
        var result = repo.GetItemsByDate(new DateTime(2022, 5, 17));

        Console.WriteLine(result[0].ToString());
        Console.WriteLine("test log");

        //assert
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void CanReadItemsByID()
    {
        //arrange
        PaymentJournal.Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

        //act
        var result = repo.GetItemsById(Guid.Parse("06257dc0-70cc-455c-9e06-886264905d2a"));

        Console.WriteLine(result[0].ToString());
        Console.WriteLine("test log");

        //assert
        Assert.True(result.Count > 0);
    }
}
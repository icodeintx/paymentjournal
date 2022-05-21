using PaymentJournal_Web.Models;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void CanInsertDocument()
        {
            //arrange
            PaymentJournal_Web.Repositories.LiteDbRepo repo = new("C:\\src\\PaymentJournal\\Web\\Data\\litedb.db");
            PaymentItem item = new PaymentItem()
            {
                Note = "this is my note",
                Payees = new List<Payee> {
                    new Payee()
                    {
                        Amount = 64.15m,
                        //Date = DateTime.Now.AddDays(-2),
                        Date = DateTime.Now.AddYears(1),
                        Name = "Target"
                    },
                    new Payee()
                    {
                        Amount = 12.94m,
                        //Date = DateTime.Now.AddDays(-1),
                        Date = DateTime.Now.AddYears(-2),
                        Name = "CircleK"
                    }
                }
            };

            //act
            var result = repo.InsertDocument(item);

            //assert
            Assert.IsTrue(result.Success);
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void CanReadAllItems()
        {
            //arrange
            PaymentJournal_Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

            //act
            var result = repo.GetAllItems();

            //assert
            Assert.IsTrue(result.Count > 0);
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void CanReadItemsByDate()
        {
            //arrange
            PaymentJournal_Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

            //act
            var result = repo.GetItemsByDate(new DateTime(2022, 5, 17));

            Console.WriteLine(result[0].ToString());
            Console.WriteLine("test log");

            //assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void CanReadItemsByID()
        {
            //arrange
            PaymentJournal_Web.Repositories.LiteDbRepo repo = new("C:\\temp\\litedb.db");

            //act
            var result = repo.GetItemsById(Guid.Parse("06257dc0-70cc-455c-9e06-886264905d2a"));

            Console.WriteLine(result[0].ToString());
            Console.WriteLine("test log");

            //assert
            Assert.IsTrue(result.Count > 0);
        }
    }
}
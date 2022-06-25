using PaymentJournal.Web.Models;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        private const string DBFile = "C:\\src\\PaymentJournal\\Web\\Data\\litedb.db";

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void CanInsertDocument()
        {
            //arrange
            PaymentJournal.Web.Repositories.PaymentRepo repo = new(DBFile);
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
            PaymentJournal.Web.Repositories.PaymentRepo repo = new(DBFile);
            //TODO - fix this ID
            Guid budgetId = Guid.Empty;
            //act
            var result = repo.GetAllItems(budgetId);

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
            PaymentJournal.Web.Repositories.PaymentRepo repo = new(DBFile);
            //TODO - fix this ID
            Guid budgetId = Guid.Empty;

            //act
            var result = repo.GetItemsByDate(budgetId, new DateTime(2022, 6, 3));

            Console.WriteLine(result[0].ToString());
            Console.WriteLine("test log");

            //assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void CanReadItemsByID()
        {
            //arrange
            PaymentJournal.Web.Repositories.PaymentRepo repo = new(DBFile);

            //act
            var result = repo.GetItemsById(Guid.Parse("3dac4311-8cde-4ffa-b545-cee39354d0e8"));

            Console.WriteLine(result);
            Console.WriteLine("test log");

            //assert
            Assert.IsTrue(result != null);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBEntry.Queries;
using DBEntry.Test.Models;

namespace DBEntry.Test
{
    [TestClass]
    public class QueryTests
    {
        #region Properties

        /// <summary>
        /// TestConnectionString
        /// </summary>
        public string TestConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = CustomerOrders_Test; Persist Security Info=True;Application Name = LP.Logging;";

        #endregion Properties

        /// <summary>
        /// Test the insert query builder
        /// </summary>
        [TestMethod]
        public void TestInsertQueryBuilder()
        {
            Entry TestEntry = new TestEntry("Val1", "Val2");
            InsertQuery TestEntryQuery = new InsertQuery(TestEntry);

            Assert.AreEqual("INSERT INTO dbo.TestTable (TestName,TestVal) VALUES (@TestName,@TestVal);", TestEntryQuery.QueryString);
        }

        /// <summary>
        /// Test the insert query builder
        /// </summary>
        [TestMethod]
        public void TestInsertQueryBuilderMultiple()
        {
            Entry[] TestEntries = {
                new TestEntry("Val1", "Val2"),
                new TestEntry("Val3", "Val4") };

            InsertQuery TestEntriesQuery = new InsertQuery(TestEntries);

            Assert.AreEqual("INSERT INTO dbo.TestTable (TestName,TestVal) VALUES (@TestName,@TestVal),(@TestName1,@TestVal1);", TestEntriesQuery.QueryString);
        }

        /// <summary>
        /// Tests the select query builder
        /// </summary>
        [TestMethod]
        public void TestSelectQueryBuilder()
        {
            Entry TestEntry = new TestEntry("Val1", "Val2");
            SelectQuery TestSelect = new SelectQuery(TestEntry);

            Assert.AreEqual(TestSelect.QueryString, "SELECT TestName,TestVal FROM dbo.TestTable");
        }

        /// <summary>
        /// Tests the select query builder with multiple parameters
        /// </summary>
        [TestMethod]
        public void TestSelectQueryWithParams()
        {
            Entry TestEntry = new TestEntry("Val1", "Val2");

            //Creates a select query that searches for where "TestCol2" = null
            SelectQuery TestSelect = new SelectQuery(TestEntry, 1, new EntryProperty("TestCol2", null));

            Assert.AreEqual(TestSelect.QueryString, "SELECT TOP(1) TestName,TestVal FROM dbo.TestTable WHERE TestCol2=@TestCol2");
        }

        /// <summary>
        /// Tests the update query builder
        /// </summary>
        [TestMethod]
        public void TestUpdateQueryBuilder()
        {
            Entry TestEntry = new TestEntry("Val1", "Val2");
            UpdateQuery Query = new UpdateQuery(TestEntry, new EntryProperty("ID", 5));

            Assert.AreEqual(Query.QueryString, "UPDATE dbo.TestTable SET TestName=@TestName,TestVal=@TestVal WHERE ID=@WhereID");
        }

        /// <summary>
        /// Tests the delete query builder
        /// </summary>
        [TestMethod]
        public void TestDeleteQuery()
        {
            Entry TestEntry = new TestEntry("Val1", "Val2");
            DeleteQuery Query = new DeleteQuery(TestEntry, new EntryProperty("ID"));

            Assert.AreEqual(Query.QueryString, "DELETE FROM dbo.TestTable WHERE ID=@ID");
        }
    }
}

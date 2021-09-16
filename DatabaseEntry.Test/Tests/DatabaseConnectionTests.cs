using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DatabaseEntry.Test.Models;

namespace DatabaseEntry.Test
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        #region Properties

        /// <summary>
        /// TestConnectionString
        /// </summary>
        public string TestConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = DBEntryTest; Persist Security Info=True;Application Name = DBEntry.Test;";

        #endregion Properties

        #region Insert Tests

        [TestMethod]
        public void TestInsertEntry()
        {
            TestEntry aTestEntry = new TestEntry("EntryTest", "SomeValue");
            Assert.IsNull(aTestEntry.PKValue);
            aTestEntry.Insert(TestConnectionString);
            Assert.IsNotNull(aTestEntry.PKValue);
        }

        #endregion Insert Tests

        #region Selecting Tests

        [TestMethod]
        public void TestSelectEntries()
        {
            Entry[] Entries = new TestEntry().Get(TestConnectionString);
            Assert.IsTrue(Entries.Length > 0);
        }

        #endregion Selecting Tests

        #region Updating Tests

        [TestMethod]
        public void TestUpdateAndDeleteEntry()
        {
            TestEntry aTestEntry = new TestEntry("EntryTest", "SomeValue");
            Assert.IsNull(aTestEntry.PKValue);
            Assert.AreEqual(aTestEntry.TestName, "EntryTest");
            aTestEntry.Insert(TestConnectionString);
            Assert.IsNotNull(aTestEntry.PKValue);
            aTestEntry.TestName = "DBConnection";
            aTestEntry.Update(TestConnectionString);

            TestEntry UpdatedEntry = new TestEntry(aTestEntry.PKValue, TestConnectionString);
            Assert.AreEqual(UpdatedEntry.TestName, "DBConnection");
            UpdatedEntry.Delete(TestConnectionString);
        }

        #endregion Updating Tests
    }
}

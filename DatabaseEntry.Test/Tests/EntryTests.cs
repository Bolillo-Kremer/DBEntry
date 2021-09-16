using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseEntry.Test.Models;

namespace DatabaseEntry.Test
{
    [TestClass]
    public class EntryTests
    {
        #region Property Tests

        /// <summary>
        /// Tests if <see cref="TestEntry"/> properties match those of its base class <see cref="Entry"/>
        /// </summary>
        [TestMethod]
        public void TestAppEntryProperties()
        {
            TestEntry aTestEntry = new TestEntry("Test", "dwutest");

            Assert.AreEqual((string)aTestEntry["TestName"].Value, aTestEntry.TestName);
            Assert.AreEqual((string)aTestEntry["TestVal"].Value, aTestEntry.TestVal);
        }

        #endregion Propert Tests

        #region Method Tests

        /// <summary>
        /// Tests the copy method of an <see cref="Entry"/>
        /// </summary>
        [TestMethod]
        public void TestEntryCopy()
        {
            TestEntry aTestEntry = new TestEntry("EntryTest", "SomeValue");
            aTestEntry.AddProperty("TestProp2", "TestVal2");

            Assert.AreEqual((string)aTestEntry["TestName"].Value, "EntryTest");
            Assert.AreEqual((string)aTestEntry["TestVal"].Value, "SomeValue");
            Assert.AreEqual((string)aTestEntry["TestProp2"].Value, "TestVal2");

            Entry TestEntryCopy = aTestEntry.Copy("Val1", "Val2", "Val3");
            Assert.AreEqual((string)TestEntryCopy["TestName"].Value, "Val1");
            Assert.AreEqual((string)TestEntryCopy["TestVal"].Value, "Val2");
            Assert.AreEqual((string)TestEntryCopy["TestProp2"].Value, "Val3");
        }

        /// <summary>
        /// Tests the <see cref="Entry"/> blank copy method
        /// </summary>
        [TestMethod]
        public void TestEntryBlankCopy()
        {
            TestEntry aTestEntry = new TestEntry("EntryTest", "SomeValue");
            Entry BlankCopy = aTestEntry.BlankCopy();

            Assert.AreEqual(aTestEntry.TableName, BlankCopy.TableName);
            Assert.AreEqual(aTestEntry.Properties.Length, BlankCopy.Properties.Length);

            foreach (EntryProperty aProp in aTestEntry.Properties)
            {
                Assert.IsTrue(BlankCopy.HasProperty(aProp.ColumnName));
                Assert.IsNull(BlankCopy[aProp.ColumnName].Value);
            }
        }

        /// <summary>
        /// Tests the <see cref="Entry"/> equals method
        /// </summary>
        [TestMethod]
        public void EntryEqualsTest()
        {
            TestEntry aTestEntry = new TestEntry("EntryTest", "SomeValue");
            TestEntry anotherTestEntry = new TestEntry("EntryTest2", "SomeValue2");

            //AnotherTestAppEntry.ChangeTableName("");

            Assert.IsTrue(aTestEntry.Equals(aTestEntry));
            Assert.IsFalse(aTestEntry.Equals(anotherTestEntry));
            Assert.IsTrue(aTestEntry.SameType(anotherTestEntry));
        }

        #endregion Method Tests
    }
}

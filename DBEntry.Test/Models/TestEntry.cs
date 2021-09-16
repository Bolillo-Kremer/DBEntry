namespace DBEntry.Test.Models
{
    class TestEntry : UniqueEntry<long>
    {
        #region Properties

        public const string Table = "dbo.TestTable";
        public const string PK = "ID";

        public string TestName
        {
            get => (string)this["TestName"].Value;
            set => this["TestName"].Value = value;
        }

        public string TestVal
        {
            get => (string)this["TestVal"].Value;
            set => this["TestVal"].Value = value;
        }

        #endregion Properties

        public TestEntry() : base(PK, Table,
            new EntryProperty("TestName"),
            new EntryProperty("TestVal"))
        { }

        public TestEntry(string TestName, string TestVal) : base(PK, Table,
            new EntryProperty("TestName", TestName),
            new EntryProperty("TestVal", TestVal))
        { }

        public TestEntry(long? ID, string Connection) : base(PK, Table,
            new EntryProperty("TestName"),
            new EntryProperty("TestVal"))
        {
            this.MatchDatabaseEntry(ID, Connection);
        }
    }
}

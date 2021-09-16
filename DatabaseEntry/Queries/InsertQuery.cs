using System.Linq;
using DatabaseEntry.Extensions;

namespace DatabaseEntry.Queries
{
    /// <summary>
    /// Easily builds an <see cref="InsertQuery"/>'s based on <see cref="Entry"/>'s
    /// </summary>
    public class InsertQuery : Query
    {
        #region Properties

        private Entry returnedEntry = null;
        /// <summary>
        /// A blank copy of the <see cref="Entry"/> type returned with the SCOPE_IDENTITY
        /// </summary>
        public Entry ReturnedEntry => returnedEntry;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Builds an insert query based on one or multiple <see cref="Entry"/>'s
        /// </summary>
        /// <param name="Entries">The <see cref="Entry"/>'s to build a query for</param>
        public InsertQuery(params Entry[] Entries)
        {
            CreateQuery(false, Entries);   
        }

        /// <summary>
        /// Builds an insert query based on a single <see cref="Entry"/>
        /// </summary>
        /// <param name="GetScopeIdentity">If true, Query will return the inserted primary key</param>
        /// <param name="aEntry">The <see cref="Entry"/> to build a query for</param>
        public InsertQuery(bool GetScopeIdentity, Entry aEntry)
        {
            CreateQuery(GetScopeIdentity, aEntry);
        }

        /// <summary>
        /// Creates an insert query that selects all the values of the ScopeIdentity
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to log</param>
        /// <param name="ScopeIdentity">The primary keys <see cref="EntryProperty"/></param>
        /// <param name="AdditionalProperties">Other <see cref="EntryProperty"/>'s that are auto-generated</param>
        public InsertQuery(Entry aEntry, EntryProperty ScopeIdentity, params EntryProperty[] AdditionalProperties)
        {
            CreateQuery(false, aEntry);

            this.returnedEntry = aEntry.BlankCopy();

            this.returnedEntry.AddProperty(ScopeIdentity);

            foreach (EntryProperty Prop in AdditionalProperties)
            {
                this.returnedEntry.AddProperty(Prop);
            }

            SelectQuery aQuery = new SelectQuery(this.returnedEntry, 1);

            this.command.CommandText += aQuery.Command.CommandText + $" WHERE {ScopeIdentity.ColumnName}=SCOPE_IDENTITY();";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates an insert query
        /// </summary>
        /// <param name="GetScopeIdentity"></param>
        /// <param name="Entries"></param>
        private void CreateQuery(bool GetScopeIdentity, params Entry[] Entries)
        {
            if (Entries.AreSameType(true) && Entries[0].HasTableName(true))
            {
                string Query = $"INSERT INTO {Entries[0].TableName} (";
                string Values = "";

                for (int i = 0; i < Entries.Length; i++)
                {
                    Values += "(";
                    foreach (EntryProperty Prop in Entries[i].Properties)
                    {
                        if (i == 0)
                        {
                            //Builds Query
                            Query += Prop.ColumnName + (Prop.Equals(Entries[0].Properties.Last()) ? ") VALUES " : ",");
                        }

                        //Builds param
                        string Param = $"{Prop.ColumnName}" + ((i == 0) ? "" : $"{i}");
                        //Builds Values for query
                        Values += $"@{Param}" + ((Entries[i].Properties.Last().Equals(Prop)) ? "" : ",");
                        this.AddParameter(Param, Prop.DataType, Prop.Value);
                    }
                    Values += ")" + ((i == Entries.Length - 1) ? ";" : ",");
                }

                this.command.CommandText = Query + Values + (GetScopeIdentity ? " SELECT SCOPE_IDENTITY();" : "");
            }
        }

        #endregion Methods
    }
}

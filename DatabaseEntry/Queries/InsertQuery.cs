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
        /// <param name="aEntries">The <see cref="Entry"/>'s to build a query for</param>
        public InsertQuery(params Entry[] aEntries)
        {
            CreateQuery(false, aEntries);   
        }

        /// <summary>
        /// Builds an insert query based on a single <see cref="Entry"/>
        /// </summary>
        /// <param name="aGetScopeIdentity">If true, Query will return the inserted primary key</param>
        /// <param name="aEntry">The <see cref="Entry"/> to build a query for</param>
        public InsertQuery(bool aGetScopeIdentity, Entry aEntry)
        {
            CreateQuery(aGetScopeIdentity, aEntry);
        }

        /// <summary>
        /// Creates an insert query that selects all the values of the ScopeIdentity
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to log</param>
        /// <param name="aScopeIdentity">The primary keys <see cref="EntryProperty"/></param>
        /// <param name="aAdditionalProperties">Other <see cref="EntryProperty"/>'s that are auto-generated</param>
        public InsertQuery(Entry aEntry, EntryProperty aScopeIdentity, params EntryProperty[] aAdditionalProperties)
        {
            CreateQuery(false, aEntry);

            this.returnedEntry = aEntry.BlankCopy();

            this.returnedEntry.AddProperty(aScopeIdentity);

            foreach (EntryProperty lProp in aAdditionalProperties)
            {
                this.returnedEntry.AddProperty(lProp);
            }

            SelectQuery lQuery = new SelectQuery(this.returnedEntry, 1);

            this.command.CommandText += lQuery.Command.CommandText + $" WHERE {aScopeIdentity.ColumnName}=SCOPE_IDENTITY();";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates an insert query
        /// </summary>
        /// <param name="aGetScopeIdentity"></param>
        /// <param name="aEntries"></param>
        private void CreateQuery(bool aGetScopeIdentity, params Entry[] aEntries)
        {
            if (aEntries.AreSameType(true) && aEntries[0].HasTableName(true))
            {
                string lQuery = $"INSERT INTO {aEntries[0].TableName} (";
                string lValues = "";

                for (int i = 0; i < aEntries.Length; i++)
                {
                    lValues += "(";
                    foreach (EntryProperty lProp in aEntries[i].Properties)
                    {
                        if (i == 0)
                        {
                            //Builds Query
                            lQuery += lProp.ColumnName + (lProp.Equals(aEntries[0].Properties.Last()) ? ") VALUES " : ",");
                        }

                        //Builds param
                        string lParam = $"{lProp.ColumnName}" + ((i == 0) ? "" : $"{i}");
                        //Builds Values for query
                        lValues += $"@{lParam}" + ((aEntries[i].Properties.Last().Equals(lProp)) ? "" : ",");
                        this.AddParameter(lParam, lProp.DataType, lProp.Value);
                    }
                    lValues += ")" + ((i == aEntries.Length - 1) ? ";" : ",");
                }

                this.command.CommandText = lQuery + lValues + (aGetScopeIdentity ? " SELECT SCOPE_IDENTITY();" : "");
            }
        }

        #endregion Methods
    }
}

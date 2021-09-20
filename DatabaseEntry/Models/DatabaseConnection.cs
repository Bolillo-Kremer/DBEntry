using DatabaseEntry.Queries;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseEntry
{
    /// <summary>
    /// Creates a connection to a specific table in a database
    /// </summary>
    public class DatabaseConnection
    {
        #region Properties

        private string connectionString { get; set; } = string.Empty;
        /// <summary>
        /// The connection string to the database
        /// </summary>
        public string ConnectionString => connectionString;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates new connection to the logging table
        /// </summary>
        /// <param name="aConnectionString">Connection string to the database</param>
        public DatabaseConnection(string aConnectionString)
        {
            this.connectionString = aConnectionString;
        }

        #endregion Constructors

        #region Database Methods

        /// <summary>
        /// Logs a single <see cref="Entry"/> to the database and returns its primary key value
        /// </summary>
        /// <typeparam name="T">The return type of the primary key in the database</typeparam>
        /// <param name="aEntry">The <see cref="Entry"/> to log to the database</param>
        /// <returns></returns>
        public T InsertEntry<T>(Entry aEntry)
        {
            return new InsertQuery(true, aEntry).ExecuteScalar<T>(this.connectionString);
        }

        /// <summary>
        /// Creates an insert query that selects all the values of the ScopeIdentity
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to log</param>
        /// <param name="aScopeIdentity">An <see cref="EntryProperty"/> based on the primary key</param>
        /// <param name="aAdditionalProperties">Other <see cref="EntryProperty"/>'s that are auto-generated</param>
        /// <returns>The Entry logged with auto-generated sql parameters</returns>
        public Entry InsertEntry(Entry aEntry, EntryProperty aScopeIdentity, params EntryProperty[] aAdditionalProperties)
        {
            InsertQuery lEntryQuery = new InsertQuery(aEntry, aScopeIdentity, aAdditionalProperties);
            Entry lReturnEntry = lEntryQuery.ReturnedEntry;

            lEntryQuery.ExecuteReader(this.connectionString, (SqlDataReader lReader) =>
            {
                foreach (EntryProperty lProp in lReturnEntry.Properties)
                {
                    lReturnEntry[lProp.ColumnName].Value = lReader[lProp.ColumnName];
                }
            });

            return lReturnEntry;
        }

        /// <summary>
        /// Logs all <see cref="Entry"/>'s in a given array of <see cref="Entry"/>'s
        /// </summary>
        /// <param name="aEntries">The <see cref="Entry"/>'s to log to the database</param>
        /// <returns>The number of rows inserted into the database</returns>
        public int InsertEntry(params Entry[] aEntries)
        {
            return new InsertQuery(aEntries).ExecuteNonQuery(this.connectionString); ;
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on a given template
        /// </summary>
        /// <param name="aTemplate">An <see cref="Entry"/> with the properties to select</param>
        /// <param name="aSearchProps">The <see cref="EntryProperty"/> to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] GetEntries(Entry aTemplate, params EntryProperty[] aSearchProps)
        {
            return GetEntries(aTemplate, -1, aSearchProps);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on a given template
        /// </summary>
        /// <param name="aTemplate">An <see cref="Entry"/> with the properties to select</param>
        /// <param name="aSearchProps">A <see cref="EntryProperty"/> array where the the value is the value to search for</param>
        /// <param name="aTop">The number of rows to select</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] GetEntries(Entry aTemplate, int aTop = -1, params EntryProperty[] aSearchProps)
        {
            List<Entry> lEntries = new List<Entry>();
            new SelectQuery(aTemplate, aTop, aSearchProps).ExecuteReader(this.connectionString, (SqlDataReader lReader) =>
            {
                Entry lRowEntry = aTemplate.BlankCopy();

                foreach (EntryProperty Prop in lRowEntry.Properties)
                {
                    lRowEntry[Prop.ColumnName].Value = lReader[Prop.ColumnName];
                }

                lEntries.Add(lRowEntry);
            });

            return lEntries.ToArray();
        }

        /// <summary>
        /// Updates a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to update</param>
        /// <param name="aSearchProps">The properties to search for when deleting</param>
        public void UpdateEntry(Entry aEntry, params EntryProperty[] aSearchProps)
        {
            new UpdateQuery(aEntry, aSearchProps).ExecuteNonQuery(this.connectionString);
        }

        /// <summary>
        /// Deletes a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to delete</param>
        /// <param name="aSearchProps">The properties to search for when deleting</param>
        public void DeleteEntry(Entry aEntry, params EntryProperty[] aSearchProps)
        {
            new DeleteQuery(aEntry, aSearchProps).ExecuteNonQuery(this.connectionString);
        }

        #endregion Database Methods
    }
}
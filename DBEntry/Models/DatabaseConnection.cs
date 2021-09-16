using System;
using System.Collections.Generic;
using DBEntry.Queries;
using System.Data.SqlClient;

namespace DBEntry
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
        /// <param name="ConnectionString">Connection string to the database</param>
        /// <param name="TableName">Table to log the <see cref="Entry"/> to</param>
        public DatabaseConnection(string ConnectionString)
        {
            this.connectionString = ConnectionString;
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
        /// <param name="ScopeIdentity">An <see cref="EntryProperty"/> based on the primary key</param>
        /// <param name="AdditionalProperties">Other <see cref="EntryProperty"/>'s that are auto-generated</param>
        /// <returns>The Entry logged with auto-generated sql parameters</returns>
        public Entry InsertEntry(Entry aEntry, EntryProperty ScopeIdentity, params EntryProperty[] AdditionalProperties)
        {
            InsertQuery EntryQuery = new InsertQuery(aEntry, ScopeIdentity, AdditionalProperties);
            Entry ReturnEntry = EntryQuery.ReturnedEntry;

            EntryQuery.ExecuteReader(this.connectionString, (SqlDataReader aReader) =>
            {
                foreach (EntryProperty Prop in ReturnEntry.Properties)
                {
                    ReturnEntry[Prop.ColumnName].Value = aReader[Prop.ColumnName];
                }
            });

            return ReturnEntry;
        }

        /// <summary>
        /// Logs all <see cref="Entry"/>'s in a given array of <see cref="Entry"/>'s
        /// </summary>
        /// <param name="Entries">The <see cref="Entry"/>'s to log to the database</param>
        /// <returns>The number of rows inserted into the database</returns>
        public int InsertEntry(params Entry[] Entries)
        {
            return new InsertQuery(Entries).ExecuteNonQuery(this.connectionString); ;
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on a given template
        /// </summary>
        /// <param name="Template">An <see cref="Entry"/> with the properties to select</param>
        /// <param name="Props">The <see cref="EntryProperty"/> to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] GetEntries(Entry Template, params EntryProperty[] Props)
        {
            return GetEntries(Template, -1, Props);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on a given template
        /// </summary>
        /// <param name="Template">An <see cref="Entry"/> with the properties to select</param>
        /// <param name="Props">A <see cref="EntryProperty"/> array where the the value is the value to search for</param>
        /// <param name="Top">The number of rows to select</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] GetEntries(Entry Template, int Top = -1, params EntryProperty[] Props)
        {
            List<Entry> Entries = new List<Entry>();
            var test = new SelectQuery(Template, Top, Props);
            new SelectQuery(Template, Top, Props).ExecuteReader(this.connectionString, (SqlDataReader aReader) =>
            {
                Entry RowEntry = Template.BlankCopy();

                foreach (EntryProperty Prop in RowEntry.Properties)
                {
                    RowEntry[Prop.ColumnName].Value = aReader[Prop.ColumnName];
                }

                Entries.Add(RowEntry);
            });

            return Entries.ToArray();
        }

        /// <summary>
        /// Updates a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to update</param>
        /// <param name="Props">The properties to search for when deleting</param>
        public void UpdateEntry(Entry aEntry, params EntryProperty[] Props)
        {
            new UpdateQuery(aEntry, Props).ExecuteNonQuery(this.connectionString);
        }

        /// <summary>
        /// Deletes a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to delete</param>
        /// <param name="Props">The properties to search for when deleting</param>
        public void DeleteEntry(Entry aEntry, params EntryProperty[] Props)
        {
            new DeleteQuery(aEntry, Props).ExecuteNonQuery(this.connectionString);
        }

        #endregion Database Methods
    }
}
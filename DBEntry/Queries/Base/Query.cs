using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;

namespace DBEntry.Queries
{
    public abstract class Query
    {
        #region Properties

        protected SqlCommand command { get; set; } = new SqlCommand();

        /// <summary>
        /// This SQL command
        /// </summary>
        public SqlCommand Command => command;

        /// <summary>
        /// This query string
        /// </summary>
        public string QueryString => this.command.CommandText;

        /// <summary>
        /// The SQL parameters
        /// </summary>
        public SqlParameterCollection Parameters => this.command.Parameters;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a SQL parameter to this <see cref="Query"/>
        /// </summary>
        /// <param name="aProp">The <see cref="EntryProperty"/> to add as a SQL parameter</param>
        public void AddParameter(EntryProperty aProp)
        {
            AddParameter(aProp.ColumnName, aProp.DataType, aProp.Value);
        }

        /// <summary>
        /// Adds a SQL parameter to this <see cref="Query"/>
        /// </summary>
        /// <param name="ColumnName">The name of the column in the database</param>
        /// <param name="DataType">The datatype of the column in the database</param>
        /// <param name="Value">The value of the parameter</param>
        public void AddParameter(string ColumnName, SqlDbType DataType, object Value)
        {
            string Param = $"@{ColumnName}";
            this.command.Parameters.Add(Param, DataType);
            this.command.Parameters[Param].Value = Value != null ? Value : DBNull.Value;
        }

        /// <summary>
        /// Builds a<see cref= "SqlDataReader" />
        /// </ summary >
        /// < param name= "Connection" > Connection to the database</param>
        /// <returns><see cref = "SqlDataReader" /> based on the <see cref = "Query" /></ returns >
        public void ExecuteReader(string Connection, Action<SqlDataReader> ReadAction)
        {
            this.command.Connection = new SqlConnection(Connection);
            this.command.Connection.Open();
            using (SqlDataReader aReader = this.command.ExecuteReader())
            {
                while(aReader.Read())
                {
                    ReadAction(aReader);
                }
            }
            this.command.Connection.Close();
        }

        /// <summary>
        /// Executes this <see cref="Query"/> and returns the number of rows affected
        /// </summary>
        /// <param name="Connection">Connection to the database</param>
        /// <returns>The number of rows affected by the <see cref="Query"/></returns>
        public int ExecuteNonQuery(string Connection)
        {
            this.command.Connection = new SqlConnection(Connection);
            this.command.Connection.Open();
            int Rows = this.command.ExecuteNonQuery();
            this.command.Connection.Close();
            return Rows;
        }

        /// <summary>
        /// Executes this <see cref="Query"/> and returns the identity of the first row
        /// </summary>
        /// <typeparam name="T">The return type of the identity in the database</typeparam>
        /// <param name="Connection"></param>
        /// <returns>The identity of the first row</returns>
        public T ExecuteScalar<T>(string Connection)
        {
            this.command.Connection = new SqlConnection(Connection);
            this.command.Connection.Open();
            T ScopeIdentity = (T)Convert.ChangeType(this.command.ExecuteScalar(), typeof(T));
            this.command.Connection.Close();
            return ScopeIdentity;
        }

        #endregion Methods
    }
}
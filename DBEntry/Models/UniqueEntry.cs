using System;
using System.Data;
using DBEntry.Exceptions;

namespace DBEntry
{
    /// <summary>
    /// An Entry Type for tables with a primary key
    /// </summary>
    /// <typeparam name="T">The type of primary key returned from the database</typeparam>
    [Serializable]
    public abstract class UniqueEntry<T> : Entry where T : struct
    {
        #region Properties

        protected T? pkValue { get; set; } = null;

        /// <summary>
        /// Value of the primary key
        /// </summary>
        public T? PKValue => pkValue;

        /// <summary>
        /// The PrimaryKey name
        /// </summary>
        protected string PrimaryKey = string.Empty;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// <param name="PrimaryKey">The name of the Primary Key column</param>
        /// </summary>
        public UniqueEntry(string PrimaryKey) : base() 
        {
            this.PrimaryKey = PrimaryKey;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="PrimaryKey">The name of the Primary Key column</param>
        /// <param name="TableName">The table to log to in the database</param>
        public UniqueEntry(string PrimaryKey, string TableName) : base(TableName)
        {
            this.PrimaryKey = PrimaryKey;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="PrimaryKey">The name of the Primary Key column</param>
        /// <param name="TableName">The table to log to in the database</param>
        /// <param name="Properties">An array of <see cref="EntryProperty"/>'s</param>
        public UniqueEntry(string PrimaryKey, string TableName, params EntryProperty[] Properties) : base(TableName, Properties)
        {
            this.PrimaryKey = PrimaryKey;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Links this <see cref="Entry"/> to an existing <see cref="Entry"/> in the database
        /// </summary>
        /// <param name="ID">The ID of the <see cref="Entry"/> in the database</param>
        public void LinkToDatabaseIdentity(T? ID)
        {
            if (this.pkValue == null)
            {
                this.pkValue = ID;
            }
            else
            {
                throw new AlreadyLinkedEntry("This Entry is already linked to an Entry in the database. Try the UnlinkFromDatabaseIdentity function first");
            }
        }

        /// <summary>
        /// Unlinks this <see cref="Entry"/> from the database
        /// </summary>
        public void UnlinkFromDatabaseIdentity()
        {
            this.pkValue = null;
        }

        protected Entry MatchDatabaseEntry(T? IDValue, string Connection)
        {
            //Add properties to get from the database
            Entry AllCols = this.Copy();
            AllCols.AddProperty(PrimaryKey);
            AllCols = AllCols.Get(Connection, 1, new EntryProperty(PrimaryKey, IDValue))[0];

            //Populate properties from database
            this.pkValue = (T)Convert.ChangeType(AllCols["ID"].Value, typeof(T));
            foreach(EntryProperty aProp in this.Properties)
            {
                this[aProp.ColumnName].Value = (AllCols[aProp.ColumnName].Value != DBNull.Value) ? (string)AllCols[aProp.ColumnName].Value : null;
            }

            return AllCols;
        }

        #endregion Methods

        #region Database Methods

        /// <summary>
        /// Inserts this <see cref="Entry"/> into the database and returns its SCOPE_IDENTITY()
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns>The identity of the newly inserted Entry</returns>
        public new void Insert(string Connection)
        {
            if (this.pkValue == null)
            {
                this.pkValue = new DatabaseConnection(Connection).InsertEntry<T>(this);
            }
            else
            {
                throw new AlreadyLinkedEntry("This Entry is already linked to an Entry in the database. Try using the Update function instead");
            }
        }

        /// <summary>
        /// Updates this <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">The connection to the database</param>
        public void Update(string Connection)
        {
            if (this.pkValue != null)
            {
                this.Update(Connection, new EntryProperty("ID", this.pkValue, SqlDbType.BigInt));
            }
            else
            {
                throw new UnlinkedEntry("This Entry isn't linked to an Entry in the database. Try using the Insert function instead");
            }
        }

        /// <summary>
        /// Deletes this <see cref="Entry"/> from the database
        /// </summary>
        /// <param name="Connection">The connection to the database</param>
        public void Delete(string Connection)
        {
            if (this.pkValue != null)
            {
                this.Delete(Connection, new EntryProperty("ID", this.pkValue, SqlDbType.BigInt));
            }
            else
            {
                throw new UnlinkedEntry();
            }
        }

        #endregion Database Methods
    }
}

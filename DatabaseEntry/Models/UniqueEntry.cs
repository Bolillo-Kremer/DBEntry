using System;
using System.Data;
using DatabaseEntry.Exceptions;

namespace DatabaseEntry
{
    /// <summary>
    /// An Entry Type for tables with a primary key
    /// </summary>
    /// <typeparam name="IdentityType">The type of primary key returned from the database</typeparam>
    [Serializable]
    public abstract class UniqueEntry<IdentityType> : Entry where IdentityType : struct
    {
        #region Properties

        /// <summary>
        /// The value of this <see cref="Entry"/>'s primary key
        /// </summary>
        protected IdentityType? pkValue { get; set; } = null;

        /// <summary>
        /// Value of the primary key
        /// </summary>
        public IdentityType? PKValue => pkValue;

        /// <summary>
        /// The PrimaryKey name
        /// </summary>
        protected string PKName = string.Empty;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// <param name="aPrimaryKeyName">The name of the Primary Key column</param>
        /// </summary>
        public UniqueEntry(string aPrimaryKeyName) : base()
        {
            this.PKName = aPrimaryKeyName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="aPrimaryKeyName">The name of the Primary Key column</param>
        /// <param name="aTableName">The table to log to in the database</param>
        public UniqueEntry(string aPrimaryKeyName, string aTableName) : base(aTableName)
        {
            this.PKName = aPrimaryKeyName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="aPrimaryKeyName">The name of the Primary Key column</param>
        /// <param name="aTableName">The table to log to in the database</param>
        /// <param name="aProperties">An array of <see cref="EntryProperty"/>'s</param>
        public UniqueEntry(string aPrimaryKeyName, string aTableName, params EntryProperty[] aProperties) : base(aTableName, aProperties)
        {
            this.PKName = aPrimaryKeyName;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Links this <see cref="Entry"/> to an existing <see cref="Entry"/> in the database
        /// </summary>
        /// <param name="aPrimaryKeyValue">The ID of the <see cref="Entry"/> in the database</param>
        public void LinkToDatabaseIdentity(IdentityType? aPrimaryKeyValue)
        {
            if (this.pkValue == null)
            {
                this.pkValue = aPrimaryKeyValue;
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

        /// <summary>
        /// Matches all values from this <see cref="Entry"/> to an Entry in the database
        /// </summary>
        /// <param name="aPrimaryKeyValue">The value of the primary key to get <see cref="Entry"/> values from</param>
        /// <param name="aConnection">Connection to the database</param>
        /// <param name="aAdditionalProperties">Any additional properties to retireve from the <see cref="Entry"/> in the database</param>
        /// <returns>An <see cref="Entry"/> with all the row data</returns>
        protected Entry MatchDatabaseEntry(IdentityType? aPrimaryKeyValue, string aConnection, params EntryProperty[] aAdditionalProperties)
        {
            //Add properties to get from the database
            Entry lAllCols = this.Copy();
            lAllCols.AddProperty(this.PKName);

            foreach (EntryProperty lProp in aAdditionalProperties)
            {
                lAllCols.AddProperty(lProp);
            }

            lAllCols = lAllCols.Get(aConnection, 1, new EntryProperty(this.PKName, aPrimaryKeyValue))[0];

            //Populate properties from database
            this.pkValue = (IdentityType)Convert.ChangeType(lAllCols[this.PKName].Value, typeof(IdentityType));
            foreach (EntryProperty aProp in this.Properties)
            {
                this[aProp.ColumnName].Value = (lAllCols[aProp.ColumnName].Value != DBNull.Value) ? lAllCols[aProp.ColumnName].Value : null;
            }

            return lAllCols;
        }

        #endregion Methods

        #region Database Methods

        /// <summary>
        /// Inserts this <see cref="Entry"/> into the database and returns its SCOPE_IDENTITY()
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        /// <returns>The identity of the newly inserted Entry</returns>
        public new void Insert(string aConnection)
        {
            if (this.pkValue == null)
            {
                this.pkValue = new DatabaseConnection(aConnection).InsertEntry<IdentityType>(this);
            }
            else
            {
                throw new AlreadyLinkedEntry("This Entry is already linked to an Entry in the database. Try using the Update function instead");
            }
        }

        /// <summary>
        /// Inserts this <see cref="Entry"/> and returns a new <see cref="Entry"/> with all of the values the inserted <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        /// <param name="aAdditionalProperties">Additional properties to insert into the database</param>
        /// <returns></returns>
        public Entry InsertAndGetScoped(string aConnection, params EntryProperty[] aAdditionalProperties)
        {
            Entry Scoped = new DatabaseConnection(aConnection).InsertEntry(this, new EntryProperty(this.PKName), aAdditionalProperties);
            this.pkValue = (IdentityType)Convert.ChangeType(Scoped[this.PKName].Value, typeof(IdentityType));
            return Scoped;
        }

        /// <summary>
        /// Updates this <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        public void Update(string aConnection)
        {
            if (this.pkValue != null)
            {
                this.Update(aConnection, new EntryProperty(this.PKName, this.pkValue));
            }
            else
            {
                throw new UnlinkedEntry("This Entry isn't linked to an Entry in the database. Try using the Insert function instead");
            }
        }

        /// <summary>
        /// Deletes this <see cref="Entry"/> from the database
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        public void Delete(string aConnection)
        {
            if (this.pkValue != null)
            {
                this.Delete(aConnection, new EntryProperty(this.PKName, this.pkValue));
            }
            else
            {
                throw new UnlinkedEntry();
            }
        }

        #endregion Database Methods
    }
}

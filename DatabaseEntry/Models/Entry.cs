using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DatabaseEntry.Exceptions;

namespace DatabaseEntry
{
    /// <summary>
    /// An Entry Type tables in the database
    /// </summary>
    [Serializable]
    public abstract class Entry
    {
        #region Properties

        #region Private/Protected

        protected Dictionary<string, EntryProperty> properties { get; set; } = new Dictionary<string, EntryProperty>();

        protected string tableName = string.Empty;

        #endregion Private/Protected

        #region Read only

        /// <summary>
        /// Gets all the <see cref="EntryProperties"/> of this <see cref="Entry"/>
        /// </summary>
        public EntryProperty[] Properties => this.properties.Values.ToArray();

        /// <summary>
        /// Gets all the propert names of this <see cref="Entry"/>
        /// </summary>
        public string[] PropertyNames => this.properties.Keys.ToArray();

        /// <summary>
        /// The table to log to in the database
        /// </summary>
        public string TableName => tableName;

        #endregion Read only

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        public Entry() { }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="TableName">The table to log to in the database</param>
        public Entry(string TableName)
        {
            this.tableName = TableName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="TableName">The table to log to in the database</param>
        /// <param name="Properties">An array of <see cref="EntryProperty"/>'s</param>
        public Entry(string TableName, params EntryProperty[] Properties)
        {
            this.tableName = TableName;

            foreach (EntryProperty Property in Properties)
            {
                this.AddProperty(Property);
            }
        }

        #endregion Constructors

        #region Indexers

        /// <summary>
        /// Finds a specified <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="PropertyName">The name of the property</param>
        /// <returns>An <see cref="EntryProperty"/></returns>
        public EntryProperty this[string PropertyName] => this.properties[PropertyName];

        #endregion Indexers

        #region Entry Methods

        /// <summary>
        /// Adds a property to this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="Property">The <see cref="EntryProperty"/> to add</param>
        public void AddProperty(EntryProperty Property)
        {
            try
            {
                properties.Add(Property.ColumnName, Property);
            }
            catch (Exception e)
            {
                throw new DuplicateEntryProperty(Property, e);
            }
        }

        /// <summary>
        /// Adds a property to this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="ColumnName">The name of the column in the database</param>
        /// <param name="Value">The value of the cell</param>
        /// <param name="DataType">The data type of the column</param>
        public void AddProperty(string ColumnName, object Value, SqlDbType DataType)
        {
            this.AddProperty(new EntryProperty(ColumnName, Value, DataType));
        }

        /// <summary>
        /// Adds a property to this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="ColumnName">The name of the column in the database</param>
        /// <param name="Value">The value of the cell</param>
        public void AddProperty(string ColumnName, object Value = null)
        {
            this.AddProperty(new EntryProperty(ColumnName, Value));
        }

        /// <summary>
        /// Removes a property from this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="ColumnName">The name of the column</param>
        public void RemoveProperty(string ColumnName)
        {
            properties.Remove(ColumnName);
        }

        /// <summary>
        /// Checks to see if two <see cref="Entry"/>'s are equal and have equal values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/> to check</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public bool Equals(Entry Compare, bool ThrowError = false)
        {
            if (this.SameType(Compare, ThrowError))
            {
                foreach (EntryProperty Prop in this.Properties)
                {
                    if(Compare[Prop.ColumnName].Value != Prop.Value)
                    {
                        return false;
                    }
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if two <see cref="Entry"/>'s are equal and have equal values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/>'s to check</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public bool Equals(Entry[] Compare, bool ThrowError = false)
        {
            foreach(Entry entry in Compare)
            {
                if (!this.Equals(entry, ThrowError))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/> to compare to</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public bool SameType(Entry Compare, bool ThrowError = false)
        {
            bool Same = this.Properties.Length == Compare.Properties.Length;
            if (Same)
            {
                foreach (EntryProperty Prop in this.Properties)
                {
                    Same = Compare.HasProperty(Prop.ColumnName) && Prop.DataType == Compare[Prop.ColumnName].DataType;

                    if (!Same)
                    {
                        if (ThrowError)
                        {
                            throw new IncomparableEntryTypes(this, Compare);
                        }
                        return false;                 
                    }
                }
            }
            return this.TableName == Compare.TableName;
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/>'s to compare to</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public bool SameType(Entry[] Compare, bool ThrowError = false)
        {
            bool Same = true;
            foreach (Entry entry in Compare)
            {
                Same = this.SameType(entry, ThrowError);
                if (!Same)
                {
                    break;              
                }
            }
            return Same;
        }

        /// <summary>
        /// Checks if this <see cref="Entry"/> has a given property name
        /// </summary>
        /// <param name="PropertyName">The property name to check for</param>
        /// <returns>True if this has property name</returns>
        public bool HasProperty(string PropertyName)
        {
            return this.PropertyNames.Contains(PropertyName);
        }

        /// <summary>
        /// Checks if this <see cref="Entry"/> has a TableName
        /// </summary>
        /// <param name="ThrowError">If set to true, this method will throw an error if this <see cref="Entry"/> doesn't have a TableName</param>
        /// <returns>True if this <see cref="Entry"/> has a TableName</returns>
        public bool HasTableName(bool ThrowError = false)
        {
            if (this.tableName != string.Empty)
            {
                return true;
            }
            throw new Exception("Entry does not have Table Name");
        }

        /// <summary>
        /// Creates a copy of the instance without values
        /// </summary>
        /// <returns>A new <see cref="Entry"/> with the same property names and new given values</returns>
        public Entry BlankCopy()
        {
            Entry rEntry = new EntryInstantiator(this.tableName);

            foreach (EntryProperty aProp in this.Properties)
            {
                rEntry.AddProperty(new EntryProperty(aProp.ColumnName, aProp.DataType));
            }

            return rEntry;
        }

        /// <summary>
        /// Creates a copy of the instance with new given values
        /// </summary>
        /// <param name="Values">A list of values in the same order as the properties of the instance</param>
        /// <returns>A new <see cref="Entry"/> with the same property names and new given values</returns>
        public Entry Copy(params object[] Values)
        {
            if (this.properties.Count == Values.Length)
            {
                Entry rEntry = new EntryInstantiator(this.tableName);

                for (int i = 0; i < this.properties.Keys.Count; i++)
                {
                    rEntry.AddProperty(this.properties.Keys.ElementAt(i), Values[i]);
                }
                return rEntry;
            }
            throw new IncomparableEntryTypes("The number of values given does not match the number of properties");
        }

        /// <summary>
        /// Creates a new instance of this Entry
        /// </summary>
        /// <returns>A new instance of this Entry</returns>
        public Entry Copy()
        {
            Entry rEntry = new EntryInstantiator(this.tableName);
            foreach(EntryProperty aProp in this.Properties)
            {
                rEntry.AddProperty(aProp);
            }
            return rEntry;
        }

        #endregion Entry Methods

        #region Database Methods

        /// <summary>
        /// Logs this <see cref="Entry"/> instance to the database
        /// </summary>
        /// <param name="Connection">Connection string to the database</param>
        public void Insert(string Connection)
        {
             new DatabaseConnection(Connection).InsertEntry(this);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">Connection string to the database</param>
        /// <param name="Top">The number of rows to select</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string Connection, int Top=-1)
        {
            return new DatabaseConnection(Connection).GetEntries(this, Top);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">Connection string to the database</param>
        /// <param name="Params">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string Connection, params EntryProperty[] Params)
        {
            return new DatabaseConnection(Connection).GetEntries(this, Params);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">Connection string to the database</param>
        /// <param name="Top">The number of rows to select</param>
        /// <param name="Params">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string Connection, int Top=-1, params EntryProperty[] Params)
        {
            return new DatabaseConnection(Connection).GetEntries(this, Top, Params);
        }

        /// <summary>
        /// Gets the first <see cref="Entry"/> based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">Connection string to the database</param>
        /// <param name="Params">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>An <see cref="Entry"/></returns>
        public Entry GetTop(string Connection, params EntryProperty[] Params)
        {
            return this.Get(Connection, 1, Params)[0];
        }

        /// <summary>
        /// Updates this <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">The connection to the database</param>
        /// <param name="Props">The properties to search for when updating</param>
        public void Update(string Connection, params EntryProperty[] Props)
        {
            new DatabaseConnection(Connection).UpdateEntry(this, Props);
        }

        /// <summary>
        /// Deletes this <see cref="Entry"/>
        /// </summary>
        /// <param name="Connection">The connection to the database</param>
        /// <param name="Props">The properties to search for when deleting</param>
        public void Delete(string Connection, params EntryProperty[] Props)
        {
            new DatabaseConnection(Connection).DeleteEntry(this, Props);
        }

        #endregion Database Methods

        #region Overrides

        override
        public string ToString()
        {
            return $"Entry({TableName}, {this.Properties.TypesToString()})";
        }

        #endregion Overrides

        #region Entry Modifier
        private class EntryInstantiator : Entry
        {
            /// <summary>
            /// Creates a new instance of <see cref="Entry"/>
            /// </summary>
            public EntryInstantiator() : base()
            { }

            /// <summary>
            /// Creates a new instance of <see cref="Entry"/>
            /// </summary>
            /// <param name="TableName">The table to log to in the database</param>
            public EntryInstantiator(string TableName) : base(TableName)
            { }

            /// <summary>
            /// Creates a new instance of <see cref="Entry"/>
            /// </summary>
            /// <param name="TableName">The table to log to in the database</param>
            /// <param name="Properties">An array of <see cref="EntryProperty"/>'s</param>
            public EntryInstantiator(string TableName, params EntryProperty[] Properties) : base(TableName, Properties)
            { }
        }

        #endregion Entry Modifier
    }
}

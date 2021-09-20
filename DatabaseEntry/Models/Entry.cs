using DatabaseEntry.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        /// <summary>
        /// The properties of this <see cref="Entry"/>
        /// </summary>
        protected Dictionary<string, EntryProperty> properties { get; set; } = new Dictionary<string, EntryProperty>();

        /// <summary>
        /// The table name of this <see cref="Entry"/> in the database
        /// </summary>
        protected string tableName = string.Empty;

        #endregion Private/Protected

        #region Read only

        /// <summary>
        /// Gets all the <see cref="EntryProperty"/>'s of this <see cref="Entry"/>
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
        /// <param name="aTableName">The table to log to in the database</param>
        public Entry(string aTableName)
        {
            this.tableName = aTableName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Entry"/>
        /// </summary>
        /// <param name="aTableName">The table to log to in the database</param>
        /// <param name="aProperties">An array of <see cref="EntryProperty"/>'s</param>
        public Entry(string aTableName, params EntryProperty[] aProperties)
        {
            this.tableName = aTableName;

            foreach (EntryProperty Property in aProperties)
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
        /// <param name="aProperty">The <see cref="EntryProperty"/> to add</param>
        public void AddProperty(EntryProperty aProperty)
        {
            try
            {
                properties.Add(aProperty.ColumnName, aProperty);
            }
            catch (Exception e)
            {
                throw new DuplicateEntryProperty(aProperty, e);
            }
        }

        /// <summary>
        /// Adds a property to this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="aColumnName">The name of the column in the database</param>
        /// <param name="aValue">The value of the cell</param>
        /// <param name="aDataType">The data type of the column</param>
        public void AddProperty(string aColumnName, object aValue, SqlDbType aDataType)
        {
            this.AddProperty(new EntryProperty(aColumnName, aValue, aDataType));
        }

        /// <summary>
        /// Adds a property to this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="aColumnName">The name of the column in the database</param>
        /// <param name="aValue">The value of the cell</param>
        public void AddProperty(string aColumnName, object aValue = null)
        {
            this.AddProperty(new EntryProperty(aColumnName, aValue));
        }

        /// <summary>
        /// Removes a property from this <see cref="Entry"/> instance
        /// </summary>
        /// <param name="aColumnName">The name of the column</param>
        public void RemoveProperty(string aColumnName)
        {
            properties.Remove(aColumnName);
        }

        /// <summary>
        /// Checks to see if two <see cref="Entry"/>'s are equal and have equal values
        /// </summary>
        /// <param name="aCompareTo">The <see cref="Entry"/> to check</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public bool Equals(Entry aCompareTo, bool aThrowError = false)
        {
            if (this.SameType(aCompareTo, aThrowError))
            {
                foreach (EntryProperty lProp in this.Properties)
                {
                    if(aCompareTo[lProp.ColumnName].Value != lProp.Value)
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
        /// <param name="aCompareTo">The <see cref="Entry"/>'s to check</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public bool Equals(Entry[] aCompareTo, bool aThrowError = false)
        {
            foreach(Entry lEntry in aCompareTo)
            {
                if (!this.Equals(lEntry, aThrowError))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="aCompareTo">The <see cref="Entry"/> to compare to</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public bool SameType(Entry aCompareTo, bool aThrowError = false)
        {
            bool lSame = this.Properties.Length == aCompareTo.Properties.Length;
            if (lSame)
            {
                foreach (EntryProperty Prop in this.Properties)
                {
                    lSame = aCompareTo.HasProperty(Prop.ColumnName) && Prop.DataType == aCompareTo[Prop.ColumnName].DataType;

                    if (!lSame)
                    {
                        if (aThrowError)
                        {
                            throw new IncomparableEntryTypes(this, aCompareTo);
                        }
                        return false;                 
                    }
                }
            }
            return this.TableName == aCompareTo.TableName;
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="aCompareTo">The <see cref="Entry"/>'s to compare to</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public bool SameType(Entry[] aCompareTo, bool aThrowError = false)
        {
            bool lSame = true;
            foreach (Entry lEntry in aCompareTo)
            {
                lSame = this.SameType(lEntry, aThrowError);
                if (!lSame)
                {
                    break;              
                }
            }
            return lSame;
        }

        /// <summary>
        /// Checks if this <see cref="Entry"/> has a given property name
        /// </summary>
        /// <param name="aPropertyName">The property name to check for</param>
        /// <returns>True if this has property name</returns>
        public bool HasProperty(string aPropertyName)
        {
            return this.PropertyNames.Contains(aPropertyName);
        }

        /// <summary>
        /// Checks if this <see cref="Entry"/> has a TableName
        /// </summary>
        /// <param name="aThrowError">If true, throws an error if this <see cref="Entry"/> does not have a table name</param>
        /// <returns>True if this <see cref="Entry"/> has a TableName</returns>
        public bool HasTableName(bool aThrowError = false)
        {         
            bool lHasTableName = this.tableName != string.Empty;
            if ((!lHasTableName) && aThrowError)
            {
                throw new Exception("Entry does not have Table Name");
            }
            return lHasTableName;
        }

        /// <summary>
        /// Creates a copy of the instance without values
        /// </summary>
        /// <returns>A new <see cref="Entry"/> with the same property names and new given values</returns>
        public Entry BlankCopy()
        {
            Entry lEntry = new EntryInstantiator(this.tableName);

            foreach (EntryProperty aProp in this.Properties)
            {
                lEntry.AddProperty(new EntryProperty(aProp.ColumnName, aProp.DataType));
            }

            return lEntry;
        }

        /// <summary>
        /// Creates a copy of the instance with new given values
        /// </summary>
        /// <param name="aValues">A list of values in the same order as the properties of the instance</param>
        /// <returns>A new <see cref="Entry"/> with the same property names and new given values</returns>
        public Entry Copy(params object[] aValues)
        {
            if (this.properties.Count == aValues.Length)
            {
                Entry lEntry = new EntryInstantiator(this.tableName);

                for (int i = 0; i < this.properties.Keys.Count; i++)
                {
                    lEntry.AddProperty(this.properties.Keys.ElementAt(i), aValues[i]);
                }
                return lEntry;
            }
            throw new IncomparableEntryTypes("The number of values given does not match the number of properties");
        }

        /// <summary>
        /// Creates a new instance of this Entry
        /// </summary>
        /// <returns>A new instance of this Entry</returns>
        public Entry Copy()
        {
            Entry lEntry = new EntryInstantiator(this.tableName);
            foreach(EntryProperty lProp in this.Properties)
            {
                lEntry.AddProperty(lProp);
            }
            return lEntry;
        }

        #endregion Entry Methods

        #region Database Methods

        /// <summary>
        /// Logs this <see cref="Entry"/> instance to the database
        /// </summary>
        /// <param name="aConnection">Connection string to the database</param>
        public void Insert(string aConnection)
        {
             new DatabaseConnection(aConnection).InsertEntry(this);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">Connection string to the database</param>
        /// <param name="aTop">The number of rows to select</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string aConnection, int aTop=-1)
        {
            return new DatabaseConnection(aConnection).GetEntries(this, aTop);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">Connection string to the database</param>
        /// <param name="aSearchProps">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string aConnection, params EntryProperty[] aSearchProps)
        {
            return new DatabaseConnection(aConnection).GetEntries(this, aSearchProps);
        }

        /// <summary>
        /// Gets a list of <see cref="Entry"/>'s based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">Connection string to the database</param>
        /// <param name="aTop">The number of rows to select</param>
        /// <param name="aSearchProps">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>A list of <see cref="Entry"/>'s</returns>
        public Entry[] Get(string aConnection, int aTop=-1, params EntryProperty[] aSearchProps)
        {
            return new DatabaseConnection(aConnection).GetEntries(this, aTop, aSearchProps);
        }

        /// <summary>
        /// Gets the first <see cref="Entry"/> based on the properties of the current <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">Connection string to the database</param>
        /// <param name="aSearchProps">A list of <see cref="EntryProperty"/>'s to search for</param>
        /// <returns>An <see cref="Entry"/></returns>
        public Entry GetTop(string aConnection, params EntryProperty[] aSearchProps)
        {
            return this.Get(aConnection, 1, aSearchProps)[0];
        }

        /// <summary>
        /// Updates this <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        /// <param name="aSearchProps">The properties to search for when updating</param>
        public void Update(string aConnection, params EntryProperty[] aSearchProps)
        {
            new DatabaseConnection(aConnection).UpdateEntry(this, aSearchProps);
        }

        /// <summary>
        /// Deletes this <see cref="Entry"/>
        /// </summary>
        /// <param name="aConnection">The connection to the database</param>
        /// <param name="aSearchProps">The properties to search for when deleting</param>
        public void Delete(string aConnection, params EntryProperty[] aSearchProps)
        {
            new DatabaseConnection(aConnection).DeleteEntry(this, aSearchProps);
        }

        #endregion Database Methods

        #region Overrides

        /// <summary>
        /// Converts this <see cref="Entry"/> into a string
        /// </summary>
        /// <returns></returns>
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
            /// <param name="aTableName">The table to log to in the database</param>
            public EntryInstantiator(string aTableName) : base(aTableName)
            { }

            /// <summary>
            /// Creates a new instance of <see cref="Entry"/>
            /// </summary>
            /// <param name="aTableName">The table to log to in the database</param>
            /// <param name="aProperties">An array of <see cref="EntryProperty"/>'s</param>
            public EntryInstantiator(string aTableName, params EntryProperty[] aProperties) : base(aTableName, aProperties)
            { }
        }

        #endregion Entry Modifier
    }
}

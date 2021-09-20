using System;
using System.Data;
using System.Linq;

namespace DatabaseEntry
{
    /// <summary>
    /// Class for storing <see cref="EntryProperty"/> data
    /// </summary>
    [Serializable]
    public class EntryProperty
    {
        #region Properties

        /// <summary>
        /// The name of the column in the database
        /// </summary>
        protected string columnName { get; set; } = string.Empty;
        /// <summary>
        /// The name of the column in the database
        /// </summary>
        public string ColumnName => columnName;

        /// <summary>
        /// The column type in the database
        /// Default: <see cref="SqlDbType.NVarChar"/>
        /// </summary>
        public SqlDbType DataType { get; set; } = SqlDbType.NVarChar;

        /// <summary>
        /// The value of the cell
        /// </summary>
        public object Value { get; set; } = null;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initiates a new <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="aColumnName">The name of the column in the database</param>
        /// <param name="aValue">The value of the cell</param>
        /// <param name="aDataType">The column type in the database. Default: <see cref="DbType.String"/></param>
        public EntryProperty(string aColumnName, object aValue, SqlDbType aDataType = SqlDbType.NVarChar)
        {
            this.columnName = aColumnName;
            this.Value = aValue;
            this.DataType = aDataType;
        }

        /// <summary>
        /// Initiates a new <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="aColumnName">The name of the column in the database</param>
        /// <param name="aDataType">The column type in the database. Default: <see cref="DbType.String"/></param>
        public EntryProperty(string aColumnName, SqlDbType aDataType = SqlDbType.NVarChar)
        {
            this.columnName = aColumnName;
            this.DataType = aDataType;
        }

        /// <summary>
        /// Initiates a new <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="aColumnName"></param>
        public EntryProperty(string aColumnName)
        {
            this.columnName = aColumnName;
        }

        #endregion Constructors

        #region Modifier Methods

        /// <summary>
        /// Changes the <see cref="DbType"/>
        /// </summary>
        /// <param name="aDataType">The new <see cref="DbType"/></param>
        public void ChangeDataType(SqlDbType aDataType)
        {
            this.DataType = aDataType;
        }

        #endregion Modifier Methods

        #region Methods

        /// <summary>
        /// Copies the current <see cref="EntryProperty"/>
        /// </summary>
        /// <returns>New <see cref="EntryProperty"/></returns>
        public EntryProperty Copy()
        {
            return new EntryProperty(this.columnName, this.Value, this.DataType);
        }

        /// <summary>
        /// Checks to see if this <see cref="EntryProperty"/> has a non-null value
        /// </summary>
        /// <returns>True if this <see cref="EntryProperty"/> has a non-null value</returns>
        public bool HasValue()
        {
            return !(this.Value == null);
        }

        /// <summary>
        /// Checks to see if this <see cref="EntryProperty"/> has a specified value
        /// </summary>
        /// <param name="aValue">The value to check against</param>
        /// <returns>True if this <see cref="EntryProperty"/> has a specified value</returns>
        public bool HasValue(object aValue)
        {
            return (aValue.Equals(this.Value));
        }

        /// <summary>
        /// Checks if this <see cref="EntryProperty"/> equals a given <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="aProperty">The <see cref="EntryProperty"/> to check against</param>
        /// <returns>True if <see cref="EntryProperty"/>'s are equal</returns>
        public bool Equals(EntryProperty aProperty)
        {
            if (this.ColumnName == aProperty.ColumnName)
            {
                if (this.Value == aProperty.Value)
                {
                    return this.DataType == aProperty.DataType;
                }
            }
            return false;
        }

        #endregion Methods
    }

    #region Extensions

    /// <summary>
    /// Extension methods for <see cref="EntryProperty"/>
    /// </summary>
    public static class EntryPropertyExtensions
    {
        #region General Extensions

        /// <summary>
        /// Converts an <see cref="EntryProperty"/> array to a string
        /// </summary>
        /// <param name="aProperties">The <see cref="EntryProperty"/> array to convert</param>
        /// <returns>A string representation of the <see cref="EntryProperty"/> array</returns>
        public static string TypesToString(this EntryProperty[] aProperties)
        {
            string ArrayString = "Array<EntryProperty>{ ";

            foreach (EntryProperty Prop in aProperties)
            {
                ArrayString += $"(ColumnName: {Prop.ColumnName}, DataType: {Prop.DataType})" + (Prop.Equals(aProperties.Last()) ? " " : ", ");
            }

            return ArrayString + "}";
        }

        #endregion General Extensions
    }

    #endregion Extensions
}

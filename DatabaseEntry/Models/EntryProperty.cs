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
        /// <param name="ColumnName">The name of the column in the database</param>
        /// <param name="Value">The value of the cell</param>
        /// <param name="DataType">The column type in the database. Default: <see cref="DbType.String"/></param>
        public EntryProperty(string ColumnName, object Value, SqlDbType DataType = SqlDbType.NVarChar)
        {
            this.columnName = ColumnName;
            this.Value = Value;
            this.DataType = DataType;
        }

        /// <summary>
        /// Initiates a new <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="ColumnName">The name of the column in the database</param>
        /// <param name="DataType">The column type in the database. Default: <see cref="DbType.String"/></param>
        public EntryProperty(string ColumnName, SqlDbType DataType = SqlDbType.NVarChar)
        {
            this.columnName = ColumnName;
            this.DataType = DataType;
        }

        /// <summary>
        /// Initiates a new <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="ColumnName"></param>
        public EntryProperty(string ColumnName)
        {
            this.columnName = ColumnName;
        }

        #endregion Constructors

        #region Modifier Methods

        /// <summary>
        /// Changes the <see cref="DbType"/>
        /// </summary>
        /// <param name="DataType">The new <see cref="DbType"/></param>
        public void ChangeDataType(SqlDbType DataType)
        {
            this.DataType = DataType;
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
        /// <param name="Value">The value to check against</param>
        /// <returns>True if this <see cref="EntryProperty"/> has a specified value</returns>
        public bool HasValue(object Value)
        {
            return (Value.Equals(this.Value));
        }

        /// <summary>
        /// Checks if this <see cref="EntryProperty"/> equals a given <see cref="EntryProperty"/>
        /// </summary>
        /// <param name="Property">The <see cref="EntryProperty"/> to check against</param>
        /// <returns>True if <see cref="EntryProperty"/>'s are equal</returns>
        public bool Equals(EntryProperty Property)
        {
            if (this.ColumnName == Property.ColumnName)
            {
                if (this.Value == Property.Value)
                {
                    return this.DataType == Property.DataType;
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
        /// <param name="Properties">The <see cref="EntryProperty"/> array to convert</param>
        /// <returns>A string representation of the <see cref="EntryProperty"/> array</returns>
        public static string TypesToString(this EntryProperty[] Properties)
        {
            string ArrayString = "Array<EntryProperty>{ ";

            foreach (EntryProperty Prop in Properties)
            {
                ArrayString += $"(ColumnName: {Prop.ColumnName}, DataType: {Prop.DataType})" + (Prop.Equals(Properties.Last()) ? " " : ", ");
            }

            return ArrayString + "}";
        }

        #endregion General Extensions
    }

    #endregion Extensions
}

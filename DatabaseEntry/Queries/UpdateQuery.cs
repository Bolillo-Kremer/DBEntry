using System.Linq;

namespace DatabaseEntry.Queries
{
    /// <summary>
    /// Creates an update query based on an <see cref="Entry"/>
    /// </summary>
    public class UpdateQuery : Query
    {
        #region Constructors

        /// <summary>
        /// Creates an <see cref="UpdateQuery"/> instance
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to update</param>
        /// <param name="aSearchProps">The properties to look for when updating</param>
        public UpdateQuery(Entry aEntry, params EntryProperty[] aSearchProps)
        {
            string lQuery = $"UPDATE {aEntry.TableName} SET ";
            foreach (EntryProperty lProp in aEntry.Properties)
            {
                this.AddParameter(lProp);
                lQuery += $"{lProp.ColumnName}=@{lProp.ColumnName}{(aEntry.Properties.Last().Equals(lProp) ? " WHERE " : ",")}";
            }
            foreach (EntryProperty lProp in aSearchProps)
            {
                this.AddParameter($"Where{lProp.ColumnName}", lProp.DataType, lProp.Value);
                lQuery += $"{lProp.ColumnName}=@Where{lProp.ColumnName}{(aSearchProps.Last().Equals(lProp) ? "" : " AND ")}";
            }
            this.command.CommandText = lQuery;
        }

        #endregion Constructors
    }
}

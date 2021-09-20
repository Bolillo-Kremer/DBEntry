using System.Linq;

namespace DatabaseEntry.Queries
{
    /// <summary>
    /// Creates a DELETE query for an <see cref="Entry"/>
    /// </summary>
    public class DeleteQuery : Query
    {
        #region Constructors

        /// <summary>
        /// Creates a DELETE query for an <see cref="Entry"/>
        /// </summary>
        /// <param name="aEntry"></param>
        /// <param name="aSearchProps"></param>
        public DeleteQuery(Entry aEntry, params EntryProperty[] aSearchProps)
        {
            string Query = $"DELETE FROM {aEntry.TableName} WHERE ";

            foreach (EntryProperty aProp in aSearchProps)
            {
                this.AddParameter(aProp);
                Query += $"{aProp.ColumnName}=@{aProp.ColumnName}{(aSearchProps.Last().Equals(aProp) ? "" : " AND ")}";
            }

            this.command.CommandText = Query;
        }

        #endregion Constructors
    }
}

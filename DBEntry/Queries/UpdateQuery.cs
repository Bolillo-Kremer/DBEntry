using System.Linq;

namespace DBEntry.Queries
{
    public class UpdateQuery : Query
    {
        #region Constructors

        /// <summary>
        /// Creates an <see cref="UpdateQuery"/> instance
        /// </summary>
        /// <param name="aEntry">The <see cref="Entry"/> to update</param>
        /// <param name="WhereProp">The properties to look for</param>
        public UpdateQuery(Entry aEntry, params EntryProperty[] WhereProp)
        {
            string Query = $"UPDATE {aEntry.TableName} SET ";
            foreach (EntryProperty aProp in aEntry.Properties)
            {
                this.AddParameter(aProp);
                Query += $"{aProp.ColumnName}=@{aProp.ColumnName}{(aEntry.Properties.Last().Equals(aProp) ? " WHERE " : ",")}";
            }
            foreach (EntryProperty Prop in WhereProp)
            {
                this.AddParameter($"Where{Prop.ColumnName}", Prop.DataType, Prop.Value);
                Query += $"{Prop.ColumnName}=@Where{Prop.ColumnName}{(WhereProp.Last().Equals(Prop) ? "" : " AND ")}";
            }
            this.command.CommandText = Query;
        }

        #endregion Constructors
    }
}

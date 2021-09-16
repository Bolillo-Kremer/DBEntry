using System.Linq;

namespace DBEntry.Queries
{
    public class DeleteQuery : Query
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aCommand">The command to build parameters for</param>
        /// <param name="aEntry"></param>
        /// <param name="WhereProp"></param>
        public DeleteQuery(Entry aEntry, params EntryProperty[] WhereProp)
        {
            string Query = $"DELETE FROM {aEntry.TableName} WHERE ";

            foreach (EntryProperty aProp in WhereProp)
            {
                this.AddParameter(aProp);
                Query += $"{aProp.ColumnName}=@{aProp.ColumnName}{(WhereProp.Last().Equals(aProp) ? "" : " AND ")}";
            }

            this.command.CommandText = Query;
        }

        #endregion Constructors
    }
}

using System.Linq;

namespace DatabaseEntry.Queries
{
    public class SelectQuery : Query
    {
        #region Constructors
        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="Template">An <see cref="Entry"/> with all of the properties to select</param>
        /// <param name="Top">The number of rows to select</param>
        public SelectQuery(Entry Template, int Top=-1)
        {
            BuildQuery(Template, Top);
        }

        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="Template">An <see cref="Entry"/> with all of the properties to select</param>
        /// <param name="Top">The number of rows to select</param>
        public SelectQuery(Entry Template, int Top=-1, params EntryProperty[] Props)
        {
            BuildQuery(Template, Top, Props);
        }

        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="Template">An <see cref="Entry"/> with all of the properties to select</param>
        public SelectQuery(Entry Template, params EntryProperty[] Props)
        {
            BuildQuery(Template, -1, Props);
        }

        #endregion Constructors

        #region Methods

        private void BuildQuery(Entry Template, int Top = -1, params EntryProperty[] Props)
        {
            if (Template.HasTableName(true))
            {
                string Query = "SELECT " + (Top > 0 ? $"TOP({Top}) " : "");
                foreach (string Col in Template.PropertyNames)
                {
                    Query += Col + (Col.Equals(Template.PropertyNames.Last()) ? "" : ",");
                }

                Query += $" FROM {Template.TableName}" + (Props.Length > 0 ? " WHERE " : "");

                foreach (EntryProperty aProp in Props)
                {
                    Query += $"{aProp.ColumnName}=@{aProp.ColumnName}" + (aProp.Equals(Props.Last()) ? "" : " AND ");
                    this.AddParameter(aProp);
                }
                this.command.CommandText = Query;
            }
        }

        #endregion Methods
    }
}

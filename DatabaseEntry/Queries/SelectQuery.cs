using System.Linq;

namespace DatabaseEntry.Queries
{
    /// <summary>
    /// Creates a SELECT query based on an <see cref="Entry"/>
    /// </summary>
    public class SelectQuery : Query
    {
        #region Constructors
        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aTemplate">An <see cref="Entry"/> with all of the properties to select</param>
        /// <param name="aTop">The number of rows to select</param>
        public SelectQuery(Entry aTemplate, int aTop=-1)
        {
            BuildQuery(aTemplate, aTop);
        }

        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aTemplate">An <see cref="Entry"/> with all of the properties to select</param>
        /// <param name="aTop">The number of rows to select</param>
        /// <param name="aSearchProps">The properties to search for when selecting</param>
        public SelectQuery(Entry aTemplate, int aTop=-1, params EntryProperty[] aSearchProps)
        {
            BuildQuery(aTemplate, aTop, aSearchProps);
        }

        /// <summary>
        /// Creates a SELECT query based on the properties of a given <see cref="Entry"/>
        /// </summary>
        /// <param name="aTemplate">An <see cref="Entry"/> with all of the properties to select</param>
        /// <param name="aSearchProps">The properties to search for when selecting</param>
        public SelectQuery(Entry aTemplate, params EntryProperty[] aSearchProps)
        {
            BuildQuery(aTemplate, -1, aSearchProps);
        }

        #endregion Constructors

        #region Methods

        private void BuildQuery(Entry aTemplate, int aTop = -1, params EntryProperty[] aSearchProps)
        {
            if (aTemplate.HasTableName(true))
            {
                string lQuery = "SELECT " + (aTop > 0 ? $"TOP({aTop}) " : "");
                foreach (string lCol in aTemplate.PropertyNames)
                {
                    lQuery += lCol + (lCol.Equals(aTemplate.PropertyNames.Last()) ? "" : ",");
                }

                lQuery += $" FROM {aTemplate.TableName}" + (aSearchProps.Length > 0 ? " WHERE " : "");

                foreach (EntryProperty lProp in aSearchProps)
                {
                    lQuery += $"{lProp.ColumnName}=@{lProp.ColumnName}" + (lProp.Equals(aSearchProps.Last()) ? "" : " AND ");
                    this.AddParameter(lProp);
                }
                this.command.CommandText = lQuery;
            }
        }

        #endregion Methods
    }
}

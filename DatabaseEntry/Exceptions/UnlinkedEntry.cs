using System;

namespace DatabaseEntry.Exceptions
{
    /// <summary>
    /// Exception thrown when a <see cref="UniqueEntry{T}"/> isn't linked to a <see cref="Entry"/> in the database
    /// </summary>
    public class UnlinkedEntry : BaseException
    {
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        public UnlinkedEntry() : base("This Entry is not linked to an Entry in the database")
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public UnlinkedEntry(string aMessage) : base(aMessage)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public UnlinkedEntry(string aContextName, string aMessage) : base(aContextName, aMessage)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public UnlinkedEntry(string aContextName, string aMessage, Exception aInnerException) : base(aContextName, aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public UnlinkedEntry(string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        { }

        #endregion Constructors
    }
}

using System;

namespace DatabaseEntry.Exceptions
{
    /// <summary>
    /// Exception thrown when a <see cref="UniqueEntry{T}"/> is already linked to an <see cref="Entry"/> in the database
    /// </summary>
    public class AlreadyLinkedEntry : BaseException
    {
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        public AlreadyLinkedEntry() : base("This Entry is already linked to an Entry in the database")
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public AlreadyLinkedEntry(string aMessage) : base(aMessage)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public AlreadyLinkedEntry(string aContextName, string aMessage) : base(aContextName, aMessage)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public AlreadyLinkedEntry(string aContextName, string aMessage, Exception aInnerException) : base(aContextName, aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public AlreadyLinkedEntry(string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        { }

        #endregion Constructors
    }
}

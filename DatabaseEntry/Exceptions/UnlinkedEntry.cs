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
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public UnlinkedEntry(string Message) : base(Message)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public UnlinkedEntry(string ContextName, string Message) : base(ContextName, Message)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public UnlinkedEntry(string ContextName, string Message, Exception InnerException) : base(ContextName, Message, InnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public UnlinkedEntry(string Message, Exception InnerException) : base(Message, InnerException)
        { }

        #endregion Constructors
    }
}

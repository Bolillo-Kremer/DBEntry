using System;

namespace DBEntry.Exceptions
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
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public AlreadyLinkedEntry(string Message) : base(Message)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public AlreadyLinkedEntry(string ContextName, string Message) : base(ContextName, Message)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public AlreadyLinkedEntry(string ContextName, string Message, Exception InnerException) : base(ContextName, Message, InnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public AlreadyLinkedEntry(string Message, Exception InnerException) : base(Message, InnerException)
        { }

        #endregion Constructors
    }
}

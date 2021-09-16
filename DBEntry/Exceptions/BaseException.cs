using System;
using System.Runtime.Serialization;

namespace DBEntry.Exceptions
{
    /// <summary>
    /// The base exception for LP.Logger
    /// </summary>
    public class BaseException : Exception
    {
        #region Properties

        private string context { get; set; } = null;
        /// <summary>
        /// The Context of the query where this error occured if applicable.
        /// </summary>
        public string Context => context;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        public BaseException()
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public BaseException(string Message) : base(Message)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public BaseException(string ContextName, string Message) : base(Message)
        {
            this.context = ContextName;
        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public BaseException(string ContextName, string Message, Exception InnerException) : base(Message, InnerException)
        {
            this.context = ContextName;
        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public BaseException(string Message, Exception InnerException) : base(Message, InnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="Info">Info about the object serialized</param>
        /// <param name="Context">Context about the source and destination of the serialized info</param>
        public BaseException(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        { }

        #endregion Constructors
    }
}

using System;
using System.Runtime.Serialization;

namespace DatabaseEntry.Exceptions
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
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public BaseException(string aMessage) : base(aMessage)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public BaseException(string aContextName, string aMessage) : base(aMessage)
        {
            this.context = aContextName;
        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public BaseException(string aContextName, string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        {
            this.context = aContextName;
        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public BaseException(string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates a new <see cref="BaseException"/>
        /// </summary>
        /// <param name="aInfo">Info about the object serialized</param>
        /// <param name="aContext">Context about the source and destination of the serialized info</param>
        public BaseException(SerializationInfo aInfo, StreamingContext aContext) : base(aInfo, aContext)
        { }

        #endregion Constructors
    }
}

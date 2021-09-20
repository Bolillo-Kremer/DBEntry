using System;
using System.Runtime.Serialization;

namespace DatabaseEntry.Exceptions
{
    /// <summary>
    /// <see cref="Exception"/> thrown when two <see cref="Entry"/>'s properties don't match
    /// </summary>
    class IncomparableEntryTypes : BaseException
    {
        #region Properties

        private Entry expected { get; set; } = null;
        /// <summary>
        /// The expected <see cref="Entry"/> type
        /// </summary>
        public Entry Expected => expected;

        private Entry actual { get; set; } = null;
        /// <summary>
        /// The actual <see cref="Entry"/> type
        /// </summary>
        public Entry Actual => actual;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        public IncomparableEntryTypes()
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(string aMessage) : base(aMessage)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aExpected">The expected <see cref="Entry"/> type</param>
        /// <param name="aActual">The actual <see cref="Entry"/> type</param>
        public IncomparableEntryTypes(Entry aExpected, Entry aActual) : base(MessageBuilder(aExpected, aActual))
        {
            this.expected = aExpected;
            this.actual = aActual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aExpected">The expected <see cref="Entry"/> type</param>
        /// <param name="aActual">The actual <see cref="Entry"/> type</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(Entry aExpected, Entry aActual, string aMessage) : base(aMessage)
        {
            this.expected = aExpected;
            this.actual = aActual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aExpected">The expected <see cref="Entry"/> type</param>
        /// <param name="aActual">The actual <see cref="Entry"/> type</param>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(string aContextName, Entry aExpected, Entry aActual, string aMessage) : base(aContextName, aMessage)
        {
            this.expected = aExpected;
            this.actual = aActual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aExpected">The expected <see cref="Entry"/> type</param>
        /// <param name="aActual">The actual <see cref="Entry"/> type</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(string aContextName, Entry aExpected, Entry aActual, Exception aInnerException) : base(aContextName, MessageBuilder(aExpected, aActual), aInnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aExpected">The expected <see cref="Entry"/> type</param>
        /// <param name="aActual">The actual <see cref="Entry"/> type</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(Entry aExpected, Entry aActual, Exception aInnerException) : base(MessageBuilder(aExpected, aActual), aInnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aMessage">An <see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="aInfo">Info about the object serialized</param>
        /// <param name="aContext">Context about the source and destination of the serialized info</param>
        public IncomparableEntryTypes(SerializationInfo aInfo, StreamingContext aContext) : base(aInfo, aContext)
        { }

        #endregion Constructors

        #region Methods

        private static string MessageBuilder(Entry aExpected, Entry aActual)
        {
            return $"{aActual} did not match the expected Entry type {aExpected}";
        }

        #endregion Methods
    }
}

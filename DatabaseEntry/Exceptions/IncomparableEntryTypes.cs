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
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(string Message) : base(Message)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="Expected">The expected <see cref="Entry"/> type</param>
        /// <param name="Actual">The actual <see cref="Entry"/> type</param>
        public IncomparableEntryTypes(Entry Expected, Entry Actual) : base(MessageBuilder(Expected, Actual))
        {
            this.expected = Expected;
            this.actual = Actual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="Expected">The expected <see cref="Entry"/> type</param>
        /// <param name="Actual">The actual <see cref="Entry"/> type</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(Entry Expected, Entry Actual, string Message) : base(Message)
        {
            this.expected = Expected;
            this.actual = Actual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Expected">The expected <see cref="Entry"/> type</param>
        /// <param name="Actual">The actual <see cref="Entry"/> type</param>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        public IncomparableEntryTypes(string ContextName, Entry Expected, Entry Actual, string Message) : base(ContextName, Message)
        {
            this.expected = Expected;
            this.actual = Actual;
        }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Expected">The expected <see cref="Entry"/> type</param>
        /// <param name="Actual">The actual <see cref="Entry"/> type</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(string ContextName, Entry Expected, Entry Actual, Exception InnerException) : base(ContextName, MessageBuilder(Expected, Actual), InnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="Expected">The expected <see cref="Entry"/> type</param>
        /// <param name="Actual">The actual <see cref="Entry"/> type</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(Entry Expected, Entry Actual, Exception InnerException) : base(MessageBuilder(Expected, Actual), InnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="Message">An <see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner <see cref="Exception"/></param>
        public IncomparableEntryTypes(string Message, Exception InnerException) : base(Message, InnerException)
        { }

        /// <summary>
        /// Creates new <see cref="IncomparableEntryTypes"/>
        /// </summary>
        /// <param name="Info">Info about the object serialized</param>
        /// <param name="Context">Context about the source and destination of the serialized info</param>
        public IncomparableEntryTypes(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        { }

        #endregion Constructors

        #region Methods

        private static string MessageBuilder(Entry Expected, Entry Actual)
        {
            return $"{Actual} did not match the expected Entry type {Expected}";
        }

        #endregion Methods
    }
}

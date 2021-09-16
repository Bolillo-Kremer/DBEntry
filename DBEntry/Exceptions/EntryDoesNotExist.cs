using System; 
using System.Runtime.Serialization;

namespace DBEntry.Exceptions
{
    /// <summary>
    /// Exception thrown when an <see cref="Entry"/> has a duplicate <see cref="EntryProperty"/>
    /// </summary>
    public class DuplicateEntryProperty : BaseException
    {
        #region Properties

        private EntryProperty property { get; set; } = null;
        public EntryProperty Property { get; set; } = null;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        public DuplicateEntryProperty()
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        public DuplicateEntryProperty(EntryProperty DuplicateProperty): base(MessageBuilder(DuplicateProperty))
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="Message"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string Message): base(Message)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(EntryProperty DuplicateProperty, string Message) : base(Message)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string ContextName, string Message): base(ContextName, Message)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        public DuplicateEntryProperty(string ContextName, EntryProperty DuplicateProperty) : base(ContextName, MessageBuilder(DuplicateProperty))
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string ContextName, EntryProperty DuplicateProperty, string Message): base(ContextName, Message)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string ContextName, string Message, Exception InnerException): base(ContextName, Message, InnerException)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string ContextName, EntryProperty DuplicateProperty, string Message, Exception InnerException) : base(ContextName, Message, InnerException)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="ContextName">The Context of the query where this error occured</param>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string ContextName, EntryProperty DuplicateProperty, Exception InnerException) : base(ContextName, MessageBuilder(DuplicateProperty), InnerException)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="Message"><see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(EntryProperty DuplicateProperty, string Message, Exception InnerException) : base(Message, InnerException)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="DuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(EntryProperty DuplicateProperty, Exception InnerException) : base(MessageBuilder(DuplicateProperty), InnerException)
        {
            this.property = DuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="Message"><see cref="Exception"/> message</param>
        /// <param name="InnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string Message, Exception InnerException): base(Message, InnerException)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="Info">Info about the object serialized</param>
        /// <param name="Context">Context about the source and destination of the serialized info</param>
        public DuplicateEntryProperty(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        { }

        #endregion Constructors

        #region Methods

        private static string MessageBuilder(EntryProperty DuplicateProperty)
        {
            return $"Cannot add property \"{DuplicateProperty.ColumnName}\" to Entry because Entry because a property named \"{DuplicateProperty.ColumnName}\" already exists";
        }

        #endregion Methods
    }
}

using System; 
using System.Runtime.Serialization;

namespace DatabaseEntry.Exceptions
{
    /// <summary>
    /// Exception thrown when an <see cref="Entry"/> has a duplicate <see cref="EntryProperty"/>
    /// </summary>
    public class DuplicateEntryProperty : BaseException
    {
        #region Properties

        private EntryProperty property { get; set; } = null;
        /// <summary>
        /// The duplicate property
        /// </summary>
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
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        public DuplicateEntryProperty(EntryProperty aDuplicateProperty): base(MessageBuilder(aDuplicateProperty))
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string aMessage): base(aMessage)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(EntryProperty aDuplicateProperty, string aMessage) : base(aMessage)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string aContextName, string aMessage): base(aContextName, aMessage)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        public DuplicateEntryProperty(string aContextName, EntryProperty aDuplicateProperty) : base(aContextName, MessageBuilder(aDuplicateProperty))
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        public DuplicateEntryProperty(string aContextName, EntryProperty aDuplicateProperty, string aMessage): base(aContextName, aMessage)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string aContextName, string aMessage, Exception aInnerException): base(aContextName, aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string aContextName, EntryProperty aDuplicateProperty, string aMessage, Exception aInnerException) : base(aContextName, aMessage, aInnerException)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aContextName">The Context of the query where this error occured</param>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string aContextName, EntryProperty aDuplicateProperty, Exception aInnerException) : base(aContextName, MessageBuilder(aDuplicateProperty), aInnerException)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(EntryProperty aDuplicateProperty, string aMessage, Exception aInnerException) : base(aMessage, aInnerException)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aDuplicateProperty">The duplicate <see cref="EntryProperty"/> in an <see cref="Entry"/></param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(EntryProperty aDuplicateProperty, Exception aInnerException) : base(MessageBuilder(aDuplicateProperty), aInnerException)
        {
            this.property = aDuplicateProperty;
        }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        /// <param name="aInnerException">The inner<see cref="Exception"/></param>
        public DuplicateEntryProperty(string aMessage, Exception aInnerException): base(aMessage, aInnerException)
        { }

        /// <summary>
        /// Creates new <see cref="DuplicateEntryProperty"/>
        /// </summary>
        /// <param name="aInfo">Info about the object serialized</param>
        /// <param name="aContext">Context about the source and destination of the serialized info</param>
        public DuplicateEntryProperty(SerializationInfo aInfo, StreamingContext aContext) : base(aInfo, aContext)
        { }

        #endregion Constructors

        #region Methods

        private static string MessageBuilder(EntryProperty aDuplicateProperty)
        {
            return $"Cannot add property \"{aDuplicateProperty.ColumnName}\" to Entry because Entry because a property named \"{aDuplicateProperty.ColumnName}\" already exists";
        }

        #endregion Methods
    }
}

using System; 
using System.Runtime.Serialization;

namespace DatabaseEntry.Exceptions
{
    /// <summary>
    /// Exception thrown when an <see cref="Entry"/> was not found in the database
    /// </summary>
    public class EntryDoesNotExist : BaseException
    {
        #region Constructors

        /// <summary>
        /// Creates new <see cref="EntryDoesNotExist"/>
        /// </summary>
        public EntryDoesNotExist() : base ("Entry does not exist in the database")
        { }

        /// <summary>
        /// Creates new <see cref="EntryDoesNotExist"/>
        /// </summary>
        /// <param name="aMessage"><see cref="Exception"/> message</param>
        public EntryDoesNotExist(string aMessage): base(aMessage)
        { }

        /// <summary>
        /// Creates new <see cref="EntryDoesNotExist"/>
        /// </summary>
        /// <param name="aInfo">Info about the object serialized</param>
        /// <param name="aContext">Context about the source and destination of the serialized info</param>
        public EntryDoesNotExist(SerializationInfo aInfo, StreamingContext aContext) : base(aInfo, aContext)
        { }

        #endregion Constructors
    }
}

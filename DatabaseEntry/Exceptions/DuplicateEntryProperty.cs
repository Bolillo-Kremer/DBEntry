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
        /// <param name="Message"><see cref="Exception"/> message</param>
        public EntryDoesNotExist(string Message): base(Message)
        { }

        /// <summary>
        /// Creates new <see cref="EntryDoesNotExist"/>
        /// </summary>
        /// <param name="Info">Info about the object serialized</param>
        /// <param name="Context">Context about the source and destination of the serialized info</param>
        public EntryDoesNotExist(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        { }

        #endregion Constructors
    }
}

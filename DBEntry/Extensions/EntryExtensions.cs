using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntry.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Entry"/>
    /// </summary>
    public static class EntryExtensions
    {
        /// <summary>
        /// Checks to see if two <see cref="Entry"/>'s are equal and have equal values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/>'s to check</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public static bool AreEqual(this Entry[] Entries, bool ThrowError = false)
        {
            return Entries[0].Equals(Entries, ThrowError);
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="Compare">The <see cref="Entry"/>'s to compare to</param>
        /// <param name="ThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public static bool AreSameType(this Entry[] Entries, bool ThrowError = false)
        {
            return Entries[0].SameType(Entries, ThrowError);
        }
    }
}

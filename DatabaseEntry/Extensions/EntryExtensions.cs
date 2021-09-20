using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseEntry.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Entry"/>
    /// </summary>
    public static class EntryExtensions
    {
        /// <summary>
        /// Checks to see if two <see cref="Entry"/>'s are equal and have equal values
        /// </summary>
        /// <param name="aEntries">The <see cref="Entry"/>'s to compare this <see cref="Entry"/> to</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if equal</returns>
        public static bool AreEqual(this Entry[] aEntries, bool aThrowError = false)
        {
            return aEntries[0].Equals(aEntries, aThrowError);
        }

        /// <summary>
        /// Checks if two <see cref="Entry"/>'s are the same type without checking values
        /// </summary>
        /// <param name="aEntries">The <see cref="Entry"/>'s to compare this <see cref="Entry"/> to</param>
        /// <param name="aThrowError">If set to true, this method will thrown an error if <see cref="Entry"/>'s are not the same type</param>
        /// <returns>True if the same type</returns>
        public static bool AreSameType(this Entry[] aEntries, bool aThrowError = false)
        {
            return aEntries[0].SameType(aEntries, aThrowError);
        }
    }
}

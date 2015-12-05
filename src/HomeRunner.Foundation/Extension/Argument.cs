
using System;

namespace HomeRunner.Foundation.Extension
{
    public static class Argument
    {
        /// <summary>
        /// Checks if a given argument object is null. If so throws an ArgumentNullException.
        /// </summary>
        /// <param name="argument">The argument object to check.</param>
        /// <param name="argumentName">The argument name to be included in the exception message.</param>
        /// <exception cref="ArgumentNullException">The <paramref argumentName="argumentName"/> [is|are] not provided.</exception>
        public static void InstanceIsRequired(object argument, string argumentName)
        {
            if (argument == null)
            {
                string verb = argumentName.EndsWith("s") && !argumentName.EndsWith("is") ? "are" : "is";
                string message = string.Format("The {0} {1} not provided.", argumentName, verb);

                throw new ArgumentNullException(argumentName, message);
            }
        }

        /// <summary>
        /// Checks if a given argument string is empty. If so throws an ArgumentException.
        /// The null argument is not considered empty.
        /// </summary>
        /// <param name="argument">The argument string to check.</param>
        /// <param name="argumentName">The argument name to be included in the exception message.</param>
        /// <exception cref="ArgumentNullException">The <paramref argumentName="argumentName"/> [is|are] empty.</exception>
        public static void StringIsRequired(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                string verb = argumentName.EndsWith("s") && !argumentName.EndsWith("is") ? "are" : "is";
                string message = string.Format("The {0} {1} empty.", argumentName, verb);

                throw new ArgumentException(message, argumentName);
            }
        }
    }
}

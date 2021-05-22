//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore
{
    /// <summary>
    ///     Extends the <see cref="object" /> with some helper methods.
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        ///     Checks if the object is null or an empty string.
        /// </summary>
        /// <param name="element">The object to check.</param>
        /// <returns>True if the object is null or an empty string; otherwise false.</returns>
        public static bool IsNullOrEmpty(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrEmpty(element.ToString());
        }

        /// <summary>
        ///     Checks if the object is null, an empty string or a string which consists of whitespace (or tabs) only.
        /// </summary>
        /// <param name="element">The object to check.</param>
        /// <returns>True if the object is null, empty or consists only of whitespace (or tabs); otherwise false.</returns>
        public static bool IsNullOrWhiteSpace(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrWhiteSpace(element.ToString());
        }
    }
}
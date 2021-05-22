//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore
{
    /// <summary>
    ///     Indicates that another method is still executing.
    ///     The work is active till the working indicator is disposed.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// private WorkingIndicator _locker;
    /// 
    /// private void DoAnything()
    /// {
    ///     using (_locker = new WorkingIndicator())
    ///     {
    ///     }
    /// }
    /// 
    /// private void AnybodyElse()
    /// {
    ///     if (WorkingIndicator.IsActive(_locker))
    ///         return;
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class WorkingIndicator : IDisposable
    {
        private bool _flag;

        /// <summary>
        ///     Creates a new instance of <see cref="WorkingIndicator" />.
        /// </summary>
        public WorkingIndicator()
        {
            _flag = true;
        }

        /// <summary>
        ///     Releases the current working indicator.
        /// </summary>
        public void Dispose()
        {
            _flag = false;
        }

        /// <summary>
        ///     Checks if the work is still ongoing.
        /// </summary>
        /// <param name="indicator">The working indicator to check.</param>
        /// <returns>True if the work is still ongoing; otherwise false.</returns>
        public static bool IsActive(WorkingIndicator indicator)
        {
            return indicator?._flag == true;
        }
    }
}
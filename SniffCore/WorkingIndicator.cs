//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore
{
    /// <summary>
    /// Indicates that another method is still executing.
    /// </summary>
    /// <example>
    /// <code lang="csharp">
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
    ///     if (_locker?.IsActive == true)
    ///         return;
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class WorkingIndicator : IDisposable
    {
        private bool _flag;

        public WorkingIndicator()
        {
            _flag = true;
        }

        public void Dispose()
        {
            _flag = false;
        }

        public static bool IsActive(WorkingIndicator indicator)
        {
            return indicator?._flag == true;
        }
    }
}
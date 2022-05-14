// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace SniffCore
{
    /// <summary>
    ///     Extends a task with useful methods.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     public ViewModel()
    ///     {
    ///         LoadDataAsync().FireAndForget();
    ///     }
    ///
    ///     private async Task LoadDataAsync()
    ///     {
    ///         // Show progress
    ///         // Load Data Async
    ///
    ///         await Task.CompletedTask;
    ///     }
    /// }
    /// ]]>
    ///     </code>
    /// </example>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Executes a task.
        ///     Use this to show that you want to execute a task without to wait for its result. (async void)
        /// </summary>
        /// <param name="task">The task to execute.</param>
        public static async void FireAndForget(this Task task)
        {
            await task;
        }
    }
}
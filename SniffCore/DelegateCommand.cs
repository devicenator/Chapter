//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore
{
    /// <summary>
    ///     Provides a way to call a callback from an ICommand.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <Window>
    ///     <StackPanel>
    ///         <Button Content="Command 1" Command="{Binding TheCommand1}" />
    ///         <Button Content="Command 2" Command="{Binding TheCommand2}" />
    ///     </StackPanel>
    /// </Window>
    /// ]]>
    ///     </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     public void ViewModel()
    ///     {
    ///         TheCommand1 = new DelegateCommand(Execute);
    ///         TheCommand2 = new DelegateCommand(CanExecute, Execute);
    ///     }
    /// 
    ///     public IDelegateCommand TheCommand1 { get; }
    ///     public IDelegateCommand TheCommand2 { get; }
    /// 
    ///     private bool CanExecute()
    ///     {
    ///         // Check if can execute
    ///         return true;
    ///     }
    /// 
    ///     private void Execute()
    ///     {
    ///         // Execute
    /// 
    ///         TheCommand1.RaiseCanExecuteChanged(); // Checks the CanExecute again
    ///         TheCommand2.RaiseCanExecuteChanged();
    ///     }
    /// }
    /// ]]>
    ///     </code>
    /// </example>
    public sealed class DelegateCommand : IDelegateCommand
    {
        private readonly Func<bool> _canExecuteCallback;
        private readonly Action _executeCallback;

        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
        /// <exception cref="ArgumentNullException">executeCallback is null</exception>
        public DelegateCommand(Action executeCallback)
            : this(() => true, executeCallback)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
        /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
        /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
        /// <exception cref="ArgumentNullException">executeCallback is null</exception>
        public DelegateCommand(Func<bool> canExecuteCallback, Action executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        /// <summary>
        ///     Checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">unused</param>
        /// <returns>True if the command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback();
        }

        /// <summary>
        ///     Executes the callback.
        /// </summary>
        /// <param name="parameter">unused</param>
        public void Execute(object parameter)
        {
            _executeCallback();
        }

        /// <summary>
        ///     Raised if the <see cref="CanExecute" /> needs to be checked again.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> to have the <see cref="CanExecute" /> checked again.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Provides a way to call a callback from an ICommand.
    /// </summary>
    /// <typeparam name="T">The command parameter type.</typeparam>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <Window>
    ///     <StackPanel>
    ///         <Button Content="Command 1" Command="{Binding TheCommand1}" />
    ///         <Button Content="Command 2" Command="{Binding TheCommand2}" />
    ///     </StackPanel>
    /// </Window>
    /// ]]>
    ///     </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     public void ViewModel()
    ///     {
    ///         TheCommand1 = new DelegateCommand<string>(Execute);
    ///         TheCommand2 = new DelegateCommand<string>(CanExecute, Execute);
    ///     }
    /// 
    ///     public IDelegateCommand TheCommand1 { get; }
    ///     public IDelegateCommand TheCommand2 { get; }
    /// 
    ///     private bool CanExecute(string argument)
    ///     {
    ///         // Check if can execute
    ///         return true;
    ///     }
    /// 
    ///     private void Execute(string argument)
    ///     {
    ///         // Execute
    /// 
    ///         TheCommand1.RaiseCanExecuteChanged(); // Checks the CanExecute again
    ///         TheCommand2.RaiseCanExecuteChanged();
    ///     }
    /// }
    /// ]]>
    ///     </code>
    /// </example>
    public sealed class DelegateCommand<T> : IDelegateCommand
    {
        private readonly Func<T, bool> _canExecuteCallback;
        private readonly Action<T> _executeCallback;

        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
        /// <exception cref="ArgumentNullException">executeCallback is null</exception>
        public DelegateCommand(Action<T> executeCallback)
            : this(o => true, executeCallback)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
        /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
        /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
        /// <exception cref="ArgumentNullException">executeCallback is null</exception>
        public DelegateCommand(Func<T, bool> canExecuteCallback, Action<T> executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        /// <summary>
        ///     Checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter cast to the parameter type.</param>
        /// <returns>True if the command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback((T) parameter);
        }

        /// <summary>
        ///     Executes the callback.
        /// </summary>
        /// <param name="parameter">The command parameter cast to the parameter type.</param>
        public void Execute(object parameter)
        {
            _executeCallback((T) parameter);
        }

        /// <summary>
        ///     Raised if the <see cref="CanExecute" /> needs to be checked again.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> to have the <see cref="CanExecute" /> checked again.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
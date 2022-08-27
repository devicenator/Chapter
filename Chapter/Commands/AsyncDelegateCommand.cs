// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter;

/// <summary>
///     Provides a way to call an async callback from an ICommand.
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
///         TheCommand1 = new AsyncDelegateCommand(ExecuteAsync);
///         TheCommand2 = new AsyncDelegateCommand(CanExecute, ExecuteAsync);
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
///     private async Task ExecuteAsync()
///     {
///         // Execute Async
///         await Task.CompletedTask;
/// 
///         TheCommand1.RaiseCanExecuteChanged(); // Checks the CanExecute again
///         TheCommand2.RaiseCanExecuteChanged();
///     }
/// }
/// ]]>
///     </code>
/// </example>
public sealed class AsyncDelegateCommand : IDelegateCommand
{
    private readonly Func<bool> _canExecuteCallback;
    private readonly Func<Task> _executeCallback;
    private bool _isBusy;

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand" />.
    /// </summary>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<Task> executeCallback)
        : this(() => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<bool> canExecuteCallback, Func<Task> executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the async command can be executed.
    /// </summary>
    /// <param name="parameter">unused</param>
    /// <returns>True if the async command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return !_isBusy && _canExecuteCallback();
    }

    /// <summary>
    ///     Executes the async callback.
    /// </summary>
    /// <param name="parameter">unused</param>
    public void Execute(object parameter)
    {
        ExecuteAsync();
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

    private async void ExecuteAsync()
    {
        _isBusy = true;
        RaiseCanExecuteChanged();
        await _executeCallback();
        _isBusy = false;
        RaiseCanExecuteChanged();
    }
}

/// <summary>
///     Provides a way to call an async callback from an ICommand.
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
///         TheCommand1 = new AsyncDelegateCommand<string>(ExecuteAsync);
///         TheCommand2 = new AsyncDelegateCommand<string>(CanExecute, ExecuteAsync);
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
///     private async Task ExecuteAsync(string argument)
///     {
///         // Execute Async
///         await Task.CompletedTask;
/// 
///         TheCommand1.RaiseCanExecuteChanged(); // Checks the CanExecute again
///         TheCommand2.RaiseCanExecuteChanged();
///     }
/// }
/// ]]>
///     </code>
/// </example>
public sealed class AsyncDelegateCommand<T> : IDelegateCommand
{
    private readonly Func<T, bool> _canExecuteCallback;
    private readonly Func<T, Task> _executeCallback;
    private bool _isBusy;

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand{T}" />.
    /// </summary>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<T, Task> executeCallback)
        : this(o => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand{T}" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<T, bool> canExecuteCallback, Func<T, Task> executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the async command can be executed.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    /// <returns>True if the async command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return !_isBusy && _canExecuteCallback((T)parameter);
    }

    /// <summary>
    ///     Executes the async callback.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    public void Execute(object parameter)
    {
        ExecuteAsync(parameter);
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

    private async void ExecuteAsync(object parameter)
    {
        _isBusy = true;
        RaiseCanExecuteChanged();
        await _executeCallback((T)parameter);
        _isBusy = false;
        RaiseCanExecuteChanged();
    }
}
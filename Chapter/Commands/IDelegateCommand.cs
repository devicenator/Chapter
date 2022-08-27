// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter;

/// <summary>
///     Extends the ICommand with an <see cref="RaiseCanExecuteChanged" />.
/// </summary>
public interface IDelegateCommand : ICommand
{
    /// <summary>
    ///     Raises the <see cref="ICommand.CanExecuteChanged" /> to have the <see cref="ICommand.CanExecute" /> checked again.
    /// </summary>
    void RaiseCanExecuteChanged();
}
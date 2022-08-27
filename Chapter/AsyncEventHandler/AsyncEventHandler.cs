// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter;

/// <summary>
///     Provides async events.
/// </summary>
/// <param name="sender">The event sender.</param>
/// <param name="e">The event args.</param>
/// <returns>The task to await.</returns>
public delegate Task AsyncEventHandler(object sender, EventArgs e);

/// <summary>
///     Provides async events.
/// </summary>
/// <typeparam name="TEventArgs">The type of event args.</typeparam>
/// <param name="sender">The event sender.</param>
/// <param name="e">The event args.</param>
/// <returns>The task to await.</returns>
public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace SniffCore
{
    /// <summary>
    ///     Brings helper for an easy maintain of property input validations.
    /// </summary>
    /// <example>
    ///     <code lang="xaml">
    /// <![CDATA[
    /// <Window>
    ///     <StackPanel>
    ///         <TextBlock Text="Name:" />
    ///         <TextBox Text="{Binding Name, ValidatesOnNotifyDataErrors=True}" />
    ///     </StackPanel>
    /// </Window>
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class PersonViewModel : ObservableObject, INotifyDataErrorInfo
    /// {
    ///     private NotifyDataErrorInfo _errors;
    ///     private string _name;
    /// 
    ///     public PersonViewModel()
    ///     {
    ///         _errors = new NotifyDataErrorInfo();
    ///     }
    /// 
    ///     public bool HasErrors => _errors.HasErrors;
    /// 
    ///     public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
    ///     {
    ///         add => _errors.ErrorsChanged += value;
    ///         remove => _errors.ErrorsChanged -= value;
    ///     }
    /// 
    ///     public string Name
    ///     {
    ///         get => _name;
    ///         set
    ///         {
    ///             NotifyAndSetIfChanged(ref _name, value);
    ///             Validate();
    ///         }
    ///     }
    /// 
    ///     private void Validate()
    ///     {
    ///         var isValid = !string.IsNullOrWhiteSpace(Name);
    ///         _errors.Evaluate(isValid, new [] { "The user name cannot be empty" }, nameof(Name));
    ///     }
    ///
    ///     public IEnumerable GetErrors(string propertyName)
    ///     {
    ///         return _errors.GetErrors(propertyName);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class NotifyDataErrorInfo : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors;

        /// <summary>
        ///     Creates a new instance of <see cref="NotifyDataErrorInfo" />.
        /// </summary>
        public NotifyDataErrorInfo()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        /// <summary>
        ///     Gets the recorded errors for a given property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The recorded errors for a given property if any; otherwise an empty list.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return Array.Empty<string>();
        }

        /// <summary>
        ///     Gets a value indicating if there are any errors.
        /// </summary>
        public bool HasErrors => _errors.Any();

        /// <summary>
        ///     Raised if the errors for a property has been changed.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Updates the state of the given error.
        /// </summary>
        /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
        /// <param name="errors">The errors to add in the case isValid is false.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void Evaluate(bool isValid, IEnumerable<string> errors, string propertyName)
        {
            if (isValid)
                _errors.Remove(propertyName);
            _errors[propertyName] = errors.ToList();
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Updates the state of the given error.
        /// </summary>
        /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
        /// <param name="error">The error to add in the case isValid is false.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void Evaluate(bool isValid, string error, string propertyName)
        {
            if (isValid)
                _errors.Remove(propertyName);
            _errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Adds a list of errors to the given property.
        /// </summary>
        /// <param name="errors">The errors to add.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void AddErrors(IEnumerable<string> errors, string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();
            foreach (var error in errors)
                if (!_errors[propertyName].Contains(error))
                    _errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Adds an error to the given property.
        /// </summary>
        /// <param name="error">The error to add.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void AddError(string error, string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();
            if (!_errors[propertyName].Contains(error))
                _errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Removes all given errors from the given property.
        /// </summary>
        /// <param name="errors">The errors to remove.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void RemoveErrors(IEnumerable<string> errors, string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
                return;

            foreach (var error in errors)
                _errors[propertyName].Remove(error);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Removes the given error from the given property.
        /// </summary>
        /// <param name="error">The error to remove.</param>
        /// <param name="propertyName">The name of the property.</param>
        public void RemoveError(string error, string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
                return;

            _errors[propertyName].Remove(error);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Clears all errors for the given property.
        /// </summary>
        /// <param name="propertyName">The name of the validated property.</param>
        public void ResetErrors(string propertyName)
        {
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Clears all errors for all properties.
        /// </summary>
        public void ResetAllErrors()
        {
            var propertyNames = _errors.Keys.ToList();
            _errors.Clear();
            foreach (var propertyName in propertyNames)
                OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
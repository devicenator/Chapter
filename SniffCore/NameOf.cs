//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore
{
    /// <summary>
    ///     Provides methods for an easy work with namespace and type names in combination with nameof.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// namespace Application.ViewModels
    /// {
    ///     public class ViewModel : ObservableObject
    ///     {
    ///         public string Name { get; set; }
    /// 
    ///         public void WriteLog()
    ///         {
    ///             Log.Write("Name set on " + NameOf.FullName<ViewModel>(nameof(Name));
    ///             // Shows: "Name set on Application.ViewModels.ViewModel.Name"
    ///         }
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public static class NameOf
    {
        /// <summary>
        ///     Returns the name of the given type. Including the property name if given.
        ///     E.g. "MainViewModel" or "MainViewModel.Name"
        /// </summary>
        /// <typeparam name="T">The type which name to read.</typeparam>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The name of the given type. Including the property name if given.</returns>
        public static string Name<T>(string propertyName = null)
        {
            return Name(typeof(T), propertyName);
        }

        /// <summary>
        ///     Returns the name of the given type. Including the property name if given.
        ///     E.g. "MainViewModel" or "MainViewModel.Name"
        /// </summary>
        /// <param name="type">The type which name to read.</param>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The name of the given type. Including the property name if given.</returns>
        public static string Name(Type type, string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                return type.Name + "." + propertyName;
            return type.Name;
        }

        /// <summary>
        ///     Returns the namespace of the given type without the type name itself.
        ///     E.g. "Application.ViewModels"
        /// </summary>
        /// <typeparam name="T">The type which namespace to read.</typeparam>
        /// <returns>The namespace of the given type without the type name itself.</returns>
        public static string Namespace<T>()
        {
            return Namespace(typeof(T));
        }

        /// <summary>
        ///     Returns the namespace of the given type without the type name itself.
        ///     E.g. "Application.ViewModels"
        /// </summary>
        /// <param name="type">The type which namespace to read.</param>
        /// <returns>The namespace of the given type without the type name itself.</returns>
        public static string Namespace(Type type)
        {
            return type.Namespace;
        }

        /// <summary>
        ///     Returns the relative path of one type namespace to the other.
        ///     E.g. "Application.ViewModels" + "Application.ViewModels.Pages" = "Pages"
        ///     If both types are in the same namespace or have no namespace in common, the result will be empty.
        /// </summary>
        /// <remarks>The order does not matter.</remarks>
        /// <typeparam name="T1">The first type which namespace to read.</typeparam>
        /// <typeparam name="T2">The second type which namespace to read.</typeparam>
        /// <returns>The relative path of one type namespace to the other.</returns>
        public static string Namespace<T1, T2>()
        {
            return Namespace(typeof(T1), typeof(T2));
        }

        /// <summary>
        ///     Returns the relative path of one type namespace to the other.
        ///     E.g. "Application.ViewModels" + "Application.ViewModels.Pages" = "Pages"
        ///     If both types are in the same namespace or have no namespace in common, the result will be empty.
        /// </summary>
        /// <remarks>The order does not matter.</remarks>
        /// <param name="type1">The first type which namespace to read.</param>
        /// <param name="type2">The second type which namespace to read.</param>
        /// <returns>The relative path of one type namespace to the other.</returns>
        public static string Namespace(Type type1, Type type2)
        {
            var first = type1.Namespace;
            var second = type2.Namespace;

            if (first == null || second == null)
                return string.Empty;
            if (first == second)
                return string.Empty;
            if (first.StartsWith(second))
                return first.Substring(second.Length + 1);
            if (second.StartsWith(first))
                return second.Substring(first.Length + 1);
            return string.Empty;
        }

        /// <summary>
        ///     Returns the namespace and name of the given type. Including the property name if given.
        ///     E.g. "Application.ViewModels.MainViewModel" or "Application.ViewModels.MainViewModel.Name"
        /// </summary>
        /// <typeparam name="T">The type which namespace and name to read.</typeparam>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The namespace and name of the given type. Including the property name if given.</returns>
        public static string FullName<T>(string propertyName = null)
        {
            return FullName(typeof(T), propertyName);
        }

        /// <summary>
        ///     Returns the namespace and name of the given type. Including the property name if given.
        ///     E.g. "Application.ViewModels.MainViewModel" or "Application.ViewModels.MainViewModel.Name"
        /// </summary>
        /// <param name="type">The type which namespace and name to read.</param>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The namespace and name of the given type. Including the property name if given.</returns>
        public static string FullName(Type type, string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                return type.FullName + "." + propertyName;
            return type.FullName;
        }
    }
}
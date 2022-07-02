// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace SniffCore;

/// <summary>
///     Extends the <see cref="object" /> with some helper methods.
/// </summary>
/// <example>
///     <code lang="csharp">
/// <![CDATA[
/// public class Converter : IValueConverter
/// {
///     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
///     {
///         if (value.IsNullOrWhitespace())
///             return string.Empty;
///     
///         return value.ToString();
///     }
///     
///     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
///     {
///         throw new NotImplementedException();
///     }
/// }
/// ]]>
///     </code>
/// </example>
public static class ObjectEx
{
    /// <summary>
    ///     Checks if the object is null or an empty string.
    /// </summary>
    /// <param name="element">The object to check.</param>
    /// <returns>True if the object is null or an empty string; otherwise false.</returns>
    public static bool IsNullOrEmpty(this object element)
    {
        return element == null || string.IsNullOrEmpty(element.ToString());
    }

    /// <summary>
    ///     Checks if the object is null, an empty string or a string which consists of whitespace (or tabs) only.
    /// </summary>
    /// <param name="element">The object to check.</param>
    /// <returns>True if the object is null, empty or consists only of whitespace (or tabs); otherwise false.</returns>
    public static bool IsNullOrWhiteSpace(this object element)
    {
        return element == null || string.IsNullOrWhiteSpace(element.ToString());
    }
}
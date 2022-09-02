// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;

namespace Chapter.ServiceLocator;

/// <summary>
///     Helper class to keep the IServiceProvider for a manual resolve of objects.
/// </summary>
/// <example>
/// <code lang="CSharp">
/// <![CDATA[
/// public static App Main()
/// {
///     var builder = CreateBuilder();
///     builder.Services.AddSingleton<IMyService, MyService>();
///
///     var app = builder.Build();
///     app.Services.UseServiceLocator();
///
///     return app
/// }
///
/// public class ViewModel
/// {
///     public void OnSomething()
///     {
///         var service = ServiceLocator.Resolve<IMyService>();
///     }
/// }
///
/// public class ViewModelTests
/// {
///     private Mock<IMyService> _service;
///     private Mock<IServiceProvider> _serviceProvider;
///
///     [SetUp]
///     public void Setup()
///     {
///         _service = new Mock<IMyService>();
///         _serviceProvider = new Mock<IServiceProvider>();
///
///         _serviceProvider.Setup(x => x.GetService(typeof(IMyService))).Returns(_service.Object);
///         ServiceLocator.Register(_serviceProvider.Object);
///
///         _target = new ViewModel();
///     }
///
///     [Test]
///     public void OnSomething_Called_ResolvesTheService()
///     {
///         _target.OnSomething();
///
///         _serviceProvider.Verify(x => x.GetService(typeof(IMyService)), Times.Once);
///     }
/// }
/// ]]>
/// </code>
/// </example>
public static class ServiceLocator
{
    private static IServiceProvider _serviceProvider;

    /// <summary>
    ///     Registers the service provider to use on object resolve.
    /// </summary>
    /// <param name="sp">The service provider to use on object resolve.</param>
    public static void UseServiceLocator(this IServiceProvider sp)
    {
        Register(sp);
    }

    /// <summary>
    ///     Registers the service provider to use on object resolve.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use on object resolve.</param>
    public static void Register(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Resolves the object by the given type.
    /// </summary>
    /// <typeparam name="T">The object type to resolve.</typeparam>
    /// <returns>The resolved object type.</returns>
    public static T Resolve<T>() where T : class
    {
        return (T)_serviceProvider.GetService(typeof(T));
    }
}
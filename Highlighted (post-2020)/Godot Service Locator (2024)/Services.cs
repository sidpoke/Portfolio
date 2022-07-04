using System;
using System.Collections.Generic;
using Godot;

namespace SidUtils.ServiceLocator;

/// <summary>
/// A globally unique service locator with a static API.
/// <remarks>Do not call the service locator in a Node's _init function as it might not yet be initialized.</remarks>
/// </summary>
[GlobalClass]
public partial class Services : Node
{
    // Stores references to services.
    private static readonly Dictionary<Type, Node> RegisteredServices = new ();
    
    /// <inheritdoc cref="IServiceLocator"/>
    public static void Register<T>(T node) where T : Node
    {
        var type = typeof(T);

        if (!RegisteredServices.TryAdd(type, node))
        {
            GD.PrintErr($"Service of type {type.Name} is already registered.");
            return;
        }

        GD.Print($"Service of type {type.Name} registered.");
    }
    
    /// <summary>
    /// Unregisters a service of type T.
    /// </summary>
    /// <typeparam name="T">The type of the node to unregister.</typeparam>
    public static void Unregister<T>() where T : Node
    {
        var type = typeof(T);

        if (RegisteredServices.ContainsKey(type))
        {
            RegisteredServices.Remove(type);
            GD.Print($"Service of type {type.Name} unregistered.");
        }
        else
        {
            GD.PrintErr($"Service of type {type.Name} is not registered.");
        }
    }
    
    /// <summary>
    /// Retrieves a service of type T.
    /// </summary>
    /// <typeparam name="T">The type of the service to retrieve.</typeparam>
    /// <returns>The node registered as the service, or null if not found.</returns>
    /// <remarks>Do <b>NOT</b> fetch services during _EnterTree. Use _Ready instead.</remarks>
    public static T Get<T>() where T : Node
    {
        var type = typeof(T);

        if (RegisteredServices.TryGetValue(type, out var service))
        {
            return service as T;
        }

        GD.PrintErr($"Service of type {type.Name} is not registered.");
        return null;
    }
}
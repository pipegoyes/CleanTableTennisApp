// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;

namespace CleanTableTennisApp.WebUI.Endpoints;

public static class EndpointExtensions
{
    public static void AddEndpoints<TMarker>(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddEndpoints(services, typeof(TMarker), configuration);
    }

    public static void AddEndpoints(this IServiceCollection services,
        Type typeMarker, IConfiguration configuration)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.AddServices))!
                .Invoke(null, [services, configuration]);
        }
    }

    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TMarker));
    }

    public static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, [app]);
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x => x is { IsAbstract: false, IsInterface: false } &&
                        typeof(IEndpoints).IsAssignableFrom(x));
        return endpointTypes;
    }
}

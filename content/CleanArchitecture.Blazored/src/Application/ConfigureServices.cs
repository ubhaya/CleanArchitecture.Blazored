

using CleanArchitecture.Blazored.Application.Common.Behaviours;
using CleanArchitecture.Blazored.Application.TodoItems.Commands;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;
using FluentValidation;
using CleanArchitecture.Blazored.Application.TodoLists.Commands;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTodoListRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTodoListCommandValidator>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<CreateTodoItemCommand>();
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}

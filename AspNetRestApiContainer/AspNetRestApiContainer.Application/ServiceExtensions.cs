using AspNetRestApiContainer.Application.Helpers;
using AspNetRestApiContainer.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspNetRestApiContainer.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}
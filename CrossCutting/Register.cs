using System;
using Adapter;
using Data;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace CrossCutting
{
    /// <summary>
    /// This layer is CrossCutting of DDD concepts. It is responsible for configuring dependencies (SoC and IoC concepts).
    /// </summary>
    public static class Register
    {
        public static void Set(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonDtoAdapter, PersonDtoAdapter>();
        }
    }
}

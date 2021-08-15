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

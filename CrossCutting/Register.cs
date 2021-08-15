using System;
using Data;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Visitors;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Visitor;

namespace CrossCutting
{
    public static class Register
    {
        public static void Set(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonJsonVisitor, PersonJsonVisitor>();
        }
    }
}

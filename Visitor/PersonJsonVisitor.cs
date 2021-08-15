using System;
using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces.Visitors;
using Domain.Results;
using Microsoft.Extensions.Logging;

namespace Visitor
{
    public class PersonJsonVisitor : IPersonJsonVisitor
    {
        readonly ILogger<PersonJsonVisitor> _logger;
        string _json;

        public PersonJsonVisitor(ILogger<PersonJsonVisitor> logger)
        {
            this._logger = logger;
        }

        public string GetJson()
        {
            return this._json;
        }

        public Result Visit(Person instance)
        {
            this._logger.LogTrace("Initializing Visit(); class: PersonJsonVisitor; layer: visitor.");

            var result = new Result();

            try
            {
                this._json = JsonSerializer.Serialize(instance, instance.GetType(), new JsonSerializerOptions()
                {
                    WriteIndented = false
                });
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while visit Person object", ex);
                result.AddError("As error occurred while formating persons registers, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Visit(); class: PersonJsonVisitor; layer: visitor.");
            }

            return result;
        }
    }
}

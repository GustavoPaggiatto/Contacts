using System.Collections.Generic;
using Domain.Results;

namespace Domain.Interfaces.Visitors
{
    public interface IVisitor<T>
        where T : class
    {
        Result Visit(T instance);
    }
}
using System.Collections.Generic;

namespace Domain.Interfaces.Adapters
{
    public interface IAdapter<I,O>
        where O : class, new()
    {
         IEnumerable<O> Adaptee(IEnumerable<I> inputs);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Interfaces.Adapters;

namespace Adapter
{
    public abstract class BaseAdapter<I, O> : IAdapter<I, O>
        where O : class, new()
    {
        public virtual IEnumerable<O> Adaptee(IEnumerable<I> inputs)
        {
            var oS = new List<O>();

            foreach (I input in inputs)
            {
                O output = new O();
                this.SetCommon(input, output);

                oS.Add(output);
            }

            return oS;
        }

        protected void SetCommon(I input, O output)
        {
            Type iType = input.GetType();
            Type oType = output.GetType();

            var iProps = iType.GetProperties();
            var oProps = oType.GetProperties();

            foreach (var prop in oProps)
            {
                var iProp = iProps.FirstOrDefault(i => i.PropertyType.Equals(prop.PropertyType) &&
                    i.Name.Equals(prop.Name));

                if (iProp != null)
                {
                    object iValue = iProp.GetValue(input);
                    prop.SetValue(output, iValue);
                }
            }
        }
    }
}

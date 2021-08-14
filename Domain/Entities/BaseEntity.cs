using System;
using Domain.Results;

namespace Domain.Entities
{
    public abstract class BaseEntity : ICloneable
    {
        public int Id { get; set; }

        public abstract object Clone();

        public abstract Result<bool> IsValid();
    }
}

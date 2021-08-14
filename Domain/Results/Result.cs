using System.Collections.Generic;

namespace Domain.Results
{
    public class Result
    {
        public ICollection<string> Errors { get; private set; }
        public bool HasError { get; private set; }

        public void AddError(string message)
        {
            if (this.Errors == null)
                this.Errors = new List<string>();

            this.Errors.Add(message);
            this.HasError = true;
        }
    }

    public class Result<T> : Result
    {
        public T Content { get; set; }
    }
}
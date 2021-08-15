namespace Domain.Interfaces.Visitors
{
    public interface IJsonVisitor<T> : IVisitor<T>
        where T : class
    {
         string GetJson();
    }
}
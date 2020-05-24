namespace NLedger.API.Types
{
    public class QueryResult : QueryResult<string>
    {
    }

    public interface IQueryResult<T>
    {
        T      Result    { get; set; }
        string Exception { get; set; }
    }

    public class QueryResult<T> : IQueryResult<T>
    {
        public T      Result    { get; set; }
        public string Exception { get; set; }
    }
}
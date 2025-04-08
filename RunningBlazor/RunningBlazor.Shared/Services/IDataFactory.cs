namespace RunningBlazor.Shared.Services;

public interface IDataFactory<T> where T : class
{
    public bool Upsert(T data);

    public IReadOnlyList<T> Get(Func<T, bool> predicate);

    public T? Get(int ID);

    public IReadOnlyList<T> GetAll();
}

using Microsoft.Data.Sqlite;
using RunningBlazor.Shared.Services;


namespace RunningBlazor.Services;

public class DataFactory<T> : IDataFactory<T> where T : class
{

    private SqliteConnection _connection;
    public DataFactory()
    {
        _connection = new SqliteConnection($"Data Source={nameof(T)}.db");
        _connection.Open();
    }

    public T Get(int ID)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<T> Get(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public bool Upsert(T data)
    {
        throw new NotImplementedException();
    }
}

﻿using Microsoft.Data.Sqlite;
using RunningBlazor.Shared.Services;

namespace RunningBlazor.Services;

public class DataFactory<T> : IDataFactory<T> where T : class
{

    private readonly string _connectionString = $"Data Source={Directory.GetCurrentDirectory()}\\{typeof(T).Name}.db;Pooling=False";
    private readonly int _propCount;

    public DataFactory()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string tableInfo = "";
        _propCount = 0;
        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.Name.Equals("ID"))
            {
                tableInfo += "ID INTEGER PRIMARY KEY AUTOINCREMENT,";
                _propCount++;
            }
            else if (prop.CanWrite)
            {
                if (prop.PropertyType == typeof(int))
                    tableInfo += $"{prop.Name} INTEGER,";
                else if (prop.PropertyType == typeof(string))
                    tableInfo += $"{prop.Name} varchar,";
                else if (prop.PropertyType == typeof(DateTime))
                    tableInfo += $"{prop.Name} DateTime,";

                _propCount++;
            }

        }
        tableInfo = tableInfo.Substring(0, tableInfo.Length - 1);

        using var command = new SqliteCommand($"CREATE TABLE IF NOT EXISTS {typeof(T).Name} ({tableInfo})", connection);
        command.ExecuteReader();
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
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = new SqliteCommand($"SELECT * FROM {typeof(T).Name}", connection);
        var reader = command.ExecuteReader();

        List<T> result = new List<T>();
        while (reader.Read())
        {
            object[] values = new object[_propCount];
            reader.GetValues(values);
            T dataObject = (T)Activator.CreateInstance(typeof(T), []);
            var props = typeof(T).GetProperties().Where(x => x.CanWrite).ToArray();
            for (int i = 0; i < _propCount; i++)
            {
                var prop = props[i];

                if (prop.PropertyType == typeof(int))
                    prop.SetValue(dataObject, Int32.Parse(values[i].ToString()));
                else if (prop.PropertyType == typeof(DateTime))
                    prop.SetValue(dataObject, DateTime.Parse((string)values[i]));
                else
                    prop.SetValue(dataObject, values[i]);
            }
            result.Add(dataObject);
        }

        return result;
    }

    public bool Upsert(T data)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string tableInfo = "";
        string valueInfo = "";
        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.Name.Equals("ID"))
            {

            }
            else if (prop.CanWrite)
            {
                if (prop.PropertyType == typeof(int))
                {
                    tableInfo += prop.Name + ",";
                    valueInfo += prop.GetValue(data) + ",";
                }
                else if (prop.PropertyType == typeof(string))
                {
                    tableInfo += prop.Name + ",";
                    valueInfo += $"\"{prop.GetValue(data)}\",";
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    tableInfo += prop.Name + ",";
                    valueInfo += $"\"{((DateTime)prop.GetValue(data)).ToString("MM/dd/yyyy HH:mm")}\",";
                }
            }
        }
        tableInfo = tableInfo.Substring(0, tableInfo.Length - 1);
        valueInfo = valueInfo.Substring(0, valueInfo.Length - 1);

        using var command = new SqliteCommand($"INSERT INTO {typeof(T).Name} ({tableInfo}) values ({valueInfo})", connection);
        var reader = command.ExecuteNonQuery();

        return true;
    }
}

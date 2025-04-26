

using Npgsql;
using System.Data;

namespace Infrastructure.Data;

internal sealed class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateOpenConnection()
    {

        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}

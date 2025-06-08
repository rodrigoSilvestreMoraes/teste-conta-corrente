using Npgsql;

namespace bank.system.Infrastructure.Repository.Connection;
public class ConnectionFactory : IConnectionFactory
{
	readonly NpgsqlDataSource _dataSource;
	public ConnectionFactory(NpgsqlDataSource dataSource)
	{
		_dataSource = dataSource;
	}
	public async Task<NpgsqlConnection> GetConnection() => await _dataSource.OpenConnectionAsync();		
}

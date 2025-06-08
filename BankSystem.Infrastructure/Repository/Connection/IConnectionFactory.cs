using Npgsql;

namespace bank.system.Infrastructure.Repository.Connection;
public interface IConnectionFactory
{
	Task<NpgsqlConnection> GetConnection();
}

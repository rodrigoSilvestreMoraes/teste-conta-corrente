using Entities =  bank.system.Application.Domain.Entities;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Infrastructure.Repository.Connection;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Repository.Account
{
	[ExcludeFromCodeCoverage]
	public class AccountRepository : IAccountReposity
	{
		readonly IConnectionFactory _connectionFactory; 

		public AccountRepository(IConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<bool> Insert(Entities.Account account, Entities.Balance balance, CancellationToken cancellationToken)
		{
			await using var connection =  await _connectionFactory.GetConnection();
			await connection.OpenAsync();
			await using var transaction = await connection.BeginTransactionAsync();
			try
			{
				using var command = new NpgsqlCommand(AccountCommands.CommandInsert, connection);
				command.Parameters.AddWithValue("@Name", account.Name);
				command.Parameters.AddWithValue("@Document", account.Document);
				command.Parameters.AddWithValue("@StatusAccount", account.Status); 
				command.Parameters.AddWithValue("@OpeningDate", account.OpeningDate);
				command.Parameters.AddWithValue("@UpdateDate", account.UpdateDate); 
				var accountId = (long)await command.ExecuteScalarAsync(cancellationToken);


				await using var balanceCommand = new NpgsqlCommand(AccountCommands.CommandInsertBalance, connection, transaction);
				balanceCommand.Parameters.AddWithValue("@AccountId", accountId);
				balanceCommand.Parameters.AddWithValue("@CurrentBalance", balance.CurrentBalance);
				balanceCommand.Parameters.AddWithValue("@UpdateDate", balance.UpdateDate);
				await balanceCommand.ExecuteNonQueryAsync(cancellationToken);

				await transaction.CommitAsync();
			}
			catch 
			{
				await transaction.RollbackAsync();
				throw;
			}

			return false;
		}
	}
}

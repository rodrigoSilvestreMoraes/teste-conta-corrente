using Entities = bank.system.Application.Domain.Entities;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Infrastructure.Repository.Connection;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using bank.system.Application.Features.Account.List.Model;

namespace bank.system.Infrastructure.Repository.Account;

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
		await using var transaction = await connection.BeginTransactionAsync();
		try
		{
			var parametersAccount = new 
			{
				name = account.Name,
				document = account.Document,
				statusAccount = account.Status,
				openingDate = account.OpeningDate,
				updateDate = account.UpdateDate
			};

			var accountId = await connection.ExecuteScalarAsync<long>(
				new CommandDefinition(commandText: AccountCommands.CommandInsert, parameters: parametersAccount, transaction: transaction));

			var parametersBalance = new
			{
				accountId = accountId,
				currentBalance = balance.CurrentBalance,
				updateDate = balance.UpdateDate
			};

			 await connection.ExecuteScalarAsync(
				new CommandDefinition(commandText: AccountCommands.CommandInsertBalance, parameters: parametersBalance, transaction: transaction));

			await transaction.CommitAsync();
			return true;
		}
		catch 
		{
			await transaction.RollbackAsync();
			throw;
		}			
	}
	public async Task<List<AccountListResponse>> List(string? document, string? name, CancellationToken cancellationToken)
	{
		await using var connection = await _connectionFactory.GetConnection();

		var parameters = new
		{
			document = document,
			name = name,
		};
		var data = await connection.QueryAsync<AccountListResponse>(new CommandDefinition(
			commandText: AccountCommands.CommandSelectList,
			parameters: parameters,
			cancellationToken: cancellationToken));

		return  data.ToList();
	}
	public async Task<AccountListResponse> Select(long? id, string? document, CancellationToken cancellationToken)
	{
		await using var connection = await _connectionFactory.GetConnection();

		var parameters = new
		{
			id = id,
			document = document,
		};
		var data = await connection.QueryAsync<AccountListResponse>(new CommandDefinition(
			commandText: AccountCommands.CommandSelect,
			parameters: parameters,
			cancellationToken: cancellationToken));

		var account = data.FirstOrDefault();
		return account;
	}
	public async Task<bool> UpdateStatus(long id, int status, string userName, CancellationToken cancellationToken)
	{
		await using var connection = await _connectionFactory.GetConnection();
		var parametersAccount = new
		{
			id = id,
			status = status,
			userName = userName
		};

		var rowsAffects = await connection.ExecuteAsync(new CommandDefinition(commandText: AccountCommands.CommandUpdateStatus, parameters: parametersAccount));
		return rowsAffects > 0;
	}
}

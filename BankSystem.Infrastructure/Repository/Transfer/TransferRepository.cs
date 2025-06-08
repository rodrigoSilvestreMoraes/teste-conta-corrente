using bank.system.Application.Domain.Entities;
using bank.system.Application.Domain.Repository.Transfer;
using bank.system.Application.Features.Transfer.Model;
using bank.system.Infrastructure.Repository.Connection;
using Dapper;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Repository.Transfer;

[ExcludeFromCodeCoverage]
public class TransferRepository : ITransferRepository
{
	readonly IConnectionFactory _connectionFactory;

	public TransferRepository(IConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<TransferAccountResponse> ExecuteTransfer(TransferAgregate transferAgregate)
	{
		await using var connection = await _connectionFactory.GetConnection();
		await using var transaction = await connection.BeginTransactionAsync();

		try
		{
			var accountFrom = transferAgregate.BankTransferFrom.AccountId;
			var accountTo = transferAgregate.BankTransferTo.AccountId;


			var fromBalance = await connection.QuerySingleAsync<Balance>(TransferCommands.CommandSelectBlockBalance, new { id = accountFrom }, transaction);
			var toBalance = await connection.QuerySingleAsync<Balance>(TransferCommands.CommandSelectBlockBalance, new { id = accountTo }, transaction );

			// Garantir o saldo a nível transacional 
			if (fromBalance.CurrentBalance < transferAgregate.ValueTransfer)
				throw new InvalidOperationException("Saldo insuficiente.");

			var valueBalanceFrom = (fromBalance.CurrentBalance - transferAgregate.ValueTransfer);
			transferAgregate.BalanceFrom.CurrentBalance = valueBalanceFrom;

			var valueBalanceFromTo = (toBalance.CurrentBalance + transferAgregate.ValueTransfer);
			transferAgregate.BalanceTo.CurrentBalance = valueBalanceFromTo;


			var transactionFromBank = await connection.ExecuteScalarAsync<long>(
				new CommandDefinition(commandText: TransferCommands.CommandInsertBankTransfer,
				parameters: new 
				{
					id = transferAgregate.BankTransferFrom.Id,
					accountId =  transferAgregate.BankTransferFrom.AccountId,
					releaseDate = transferAgregate.BankTransferFrom.ReleaseDate,
					operation = transferAgregate.BankTransferFrom.Operation,
					value = transferAgregate.BankTransferFrom.Value,

				}, transaction: transaction));

			var transactionToBank = await connection.ExecuteScalarAsync<long>(
				new CommandDefinition(commandText: TransferCommands.CommandInsertBankTransfer,
				parameters: new
				{
					id = transferAgregate.BankTransferTo.Id,
					accountId = transferAgregate.BankTransferTo.AccountId,
					releaseDate = transferAgregate.BankTransferTo.ReleaseDate,
					operation = transferAgregate.BankTransferTo.Operation,
					value = transferAgregate.BankTransferTo.Value,

				}, transaction: transaction));


			var balanceFromUpdate = await connection.ExecuteScalarAsync<long>(
				new CommandDefinition(commandText: TransferCommands.CommandUpdateBalanceTransfer,
				parameters: new
				{
					currentBalance = valueBalanceFrom,
					updateDate = transferAgregate.BalanceFrom.UpdateDate,
					accountId = transferAgregate.BalanceFrom.AccountId

				}, transaction: transaction));

			var balanceToUpdate = await connection.ExecuteScalarAsync<long>(
				new CommandDefinition(commandText: TransferCommands.CommandUpdateBalanceTransfer,
				parameters: new
				{
					currentBalance = valueBalanceFromTo,
					updateDate = transferAgregate.BalanceTo.UpdateDate,
					accountId = transferAgregate.BalanceTo.AccountId

				}, transaction: transaction));


			await transaction.CommitAsync();

			return new TransferAccountResponse { TransactionId = transferAgregate.BankTransferFrom.Id.ToString() };
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}
}

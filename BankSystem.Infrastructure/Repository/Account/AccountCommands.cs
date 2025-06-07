using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Repository.Account
{
	[ExcludeFromCodeCoverage]
	internal static class AccountCommands
	{
		internal static string CommandInsert => @"INSERT INTO account (name, document, status_account, opening_date, update_date)
												VALUES (@Name, @Document, @StatusAccount, @OpeningDate, @UpdateDate)
												RETURNING id;";

		internal static string CommandInsertBalance => @"INSERT INTO balance (account_id, current_balance, update_date)
														VALUES (@AccountId, @CurrentBalance, @UpdateDate);";

	}
}

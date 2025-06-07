using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Repository.Account
{
	[ExcludeFromCodeCoverage]
	internal static class AccountCommands
	{
		internal static string CommandInsert => @"INSERT INTO account (name, document, status, openingDate, updateDate)
												VALUES (@name, @document, @statusAccount, @openingDate, @updateDate)
												RETURNING id;";

		internal static string CommandInsertBalance => @"INSERT INTO balance (accountId, currentBalance, updateDate)
														VALUES (@accountId, @currentBalance, @updateDate);";

		internal static string CommandSelect => @"
					SELECT * FROM account 
					WHERE (@id IS NULL OR id = @id) 
					AND (@document IS NULL OR document = @document)";

		internal static string CommandSelectList => @"
					SELECT 
						a.name,
						a.document,
						a.status,
						a.openingDate,
						b.currentBalance
					FROM account a
					INNER JOIN balance b ON b.accountId = a.id
					WHERE (@document IS NULL OR a.document = @document)
					  AND (@name IS NULL OR a.name ILIKE CONCAT('%', @name, '%'))";

		internal static string CommandUpdateStatus => @"
							UPDATE account
								SET 
								status = @status,
								updateDate = NOW(),
								userName = @userName
								WHERE id = @id";
	}
}

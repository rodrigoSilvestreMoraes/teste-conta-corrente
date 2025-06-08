using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Repository.Transfer;

[ExcludeFromCodeCoverage]
internal static class TransferCommands
{
    internal static string CommandInsertBankTransfer => @"
                    INSERT INTO bank_transfer (
                        accountId,
                        releaseDate,
                        operation,
                        value,
                        id
                    ) VALUES (
                        @accountId,
                        @releaseDate,
                        @operation,
                        @value,
                        @id)";


    internal static string CommandUpdateBalanceTransfer => @"UPDATE balance
                                                SET  currentBalance = @currentBalance,
                                                updateDate = @updateDate
                                                WHERE accountId = @accountId;";

    internal static string CommandSelectBlockBalance => @"SELECT * FROM balance WHERE accountId = @id FOR UPDATE";

}

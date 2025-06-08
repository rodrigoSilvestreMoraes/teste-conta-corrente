using bank.system.Application.Features.BankTransfer.TransferAccount.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.BankTransfer.TransferAccount;

public interface ITransferAccountUseCase
{
	Task<AppResponse<TransferAccountResponse>> ExecuteTransfer(TransferAccountRequest transferAccountRequest, CancellationToken cancellationToken);
}

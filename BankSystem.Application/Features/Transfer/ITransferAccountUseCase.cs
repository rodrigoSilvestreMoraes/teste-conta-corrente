using bank.system.Application.Features.Transfer.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Transfer;

public interface ITransferAccountUseCase
{
	Task<AppResponse<TransferAccountResponse>> ExecuteTransfer(TransferAccountRequest transferAccountRequest, CancellationToken cancellationToken);
}

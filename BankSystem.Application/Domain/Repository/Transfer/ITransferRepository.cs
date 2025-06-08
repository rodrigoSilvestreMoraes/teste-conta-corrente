using bank.system.Application.Features.Transfer.Model;

namespace bank.system.Application.Domain.Repository.Transfer;
public interface ITransferRepository
{
	Task<TransferAccountResponse> ExecuteTransfer(TransferAgregate transferAgregate, CancellationToken cancellationToken);
}

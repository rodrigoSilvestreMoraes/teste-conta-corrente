using bank.system.Application.Domain.Entities;
using bank.system.Application.Features.Transfer.Model;

namespace bank.system.Application.Domain.Repository.Transfer;
public interface ITransferRepository
{
	Task<TransferAccountResponse> ExecuteTransfer(TransferAgregate transferAgregate);
}

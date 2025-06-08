using bank.system.Application.Features.Account.List.Model;

namespace bank.system.Application.Domain.Repository.Account;
public interface IAccountReposity
{
	Task<bool> Insert(Entities.Account account, Entities.Balance balance, CancellationToken cancellationToken);
	Task<bool> UpdateStatus(long id, int status, string userName, CancellationToken cancellationToken);
	Task<AccountListResponse> Select(long? id, string? document, CancellationToken cancellationToken);
	Task<List<AccountListResponse>> List(string? document, string? name, CancellationToken cancellationToken);
}

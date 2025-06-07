using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Account.List
{
	public interface IListAccountUseCase
	{
		Task<AppResponse<List<AccountListResponse>>> ListAccountAsync(string? document, string? name, CancellationToken cancellationToken);
	}
}

using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Account.Create
{
	public interface ICreateAccountUseCase
	{
		Task<AppResponse<AccountCreateResponse>> CreateAccountAsync(AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken);
	}
}

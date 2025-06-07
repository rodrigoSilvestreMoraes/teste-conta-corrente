using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Account.Create
{
	public class CreateAccountUseCase : ICreateAccountUseCase
	{
		readonly IAccountReposity _accountReposity;
		public CreateAccountUseCase(IAccountReposity accountReposity)
		{
			_accountReposity = accountReposity;
		}

		public Task<AppResponse<bool>> CreateAccountAsync(AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

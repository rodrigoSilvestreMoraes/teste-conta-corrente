using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Shared.Extension;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Account.List;
public class ListAccountUseCase : IListAccountUseCase
{
	readonly IAccountReposity _accountReposity;	
	public ListAccountUseCase(IAccountReposity accountReposity)
	{
		_accountReposity = accountReposity;
	}

	public async Task<AppResponse<List<AccountListResponse>>> ListAccountAsync(string? document, string? name, CancellationToken cancellationToken)
	{
		var response = new AppResponse<List<AccountListResponse>>();

		if(!string.IsNullOrEmpty(document))
		{
			if(!ValidatorExtension.IsValidCpfOrCnpj(document))
			{
				response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = AccountShared._errorCode, Message = "O documento informado não é um CPF ou CNPJ válido." });
				return response;
			}

			var data = await _accountReposity.List(document: document, name: name, cancellationToken: cancellationToken);
			response.Response = data;
			return response;
		}

		if (!string.IsNullOrEmpty(name))
		{
			var data = await _accountReposity.List(document: document, name: name, cancellationToken: cancellationToken);
			response.Response = data;
			return response;
		}

		response.Response = new List<AccountListResponse>();
		return response;
	}
}

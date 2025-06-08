using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Shared.Extension;
using bank.system.Application.Shared.Options;
using bank.system.Application.Shared.Results;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace bank.system.Application.Features.Account.Create;
public class CreateAccountUseCase : ICreateAccountUseCase
{
	readonly IAccountReposity _accountReposity;
	readonly IValidator<AccountCreateRequest> _validator;
	readonly AccountRulesOption _accountRulesOption;

	public CreateAccountUseCase(
		IAccountReposity accountReposity,
		IOptions<AccountRulesOption> accountRulesOption,
		IValidator<AccountCreateRequest> validator)
	{
		_accountReposity = accountReposity;
		_accountRulesOption = accountRulesOption.Value;
		_validator = validator;
	}

	public async Task<AppResponse<AccountCreateResponse>> CreateAccountAsync(AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken)
	{
		var response = new AppResponse<AccountCreateResponse>();
		var validationResult = await _validator.ValidateAsync(accountCreateRequest, cancellationToken);

		if (!validationResult.IsValid)
		{
			response.Validation.VndErrors.Errors.AddRange(CustomValidators.GetVndErros(validationResult.Errors));
			return response;
		}

		var existeAccount = await _accountReposity.Select(id: null, document: ValidatorExtension.CleanDocument(accountCreateRequest.Document), cancellationToken);
		if (existeAccount != null)
		{
			response.Response = new AccountCreateResponse { Result = false } ;
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = AccountShared._errorCode, Message = "Conta já se encontra cadastrada." });
			return response;
		}

		//Criar os objetos relacionados ao negócio
		var account = new Domain.Entities.Account
		{
			Name = accountCreateRequest.Name,
			Document = ValidatorExtension.CleanDocument(accountCreateRequest.Document),
			OpeningDate = DateTime.Now,
			Status = Domain.Entities.Enums.StatusAccount.Active
		};

		var initialBalance = new Domain.Entities.Balance
		{
			CurrentBalance = _accountRulesOption.InitialBalance,
			UpdateDate = DateTime.Now
		};

		var result = await _accountReposity.Insert(account, initialBalance, cancellationToken);
		response.Response = new AccountCreateResponse { Result = result };
		return response;
	}
}

using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Shared.Extension;
using bank.system.Application.Shared.Results;
using FluentValidation;

namespace bank.system.Application.Features.Account.Update;

public class DesactiveAccountUseCase : IDesactiveAccountUseCase
{
	readonly IAccountReposity _accountReposity;
	readonly IValidator<DesactiveAccountRequest> _validator;
	public DesactiveAccountUseCase(IAccountReposity accountReposity,
		IValidator<DesactiveAccountRequest> validator)
	{
		_accountReposity = accountReposity;
		_validator = validator;
	}

	public async Task<AppResponse<bool>> DesactiveAccountAsync(DesactiveAccountRequest desactiveAccountRequest, CancellationToken cancellationToken)
	{
		var response = new AppResponse<bool>();

		var validationResult = await _validator.ValidateAsync(desactiveAccountRequest, cancellationToken);
		if (!validationResult.IsValid)
		{
			response.Validation.VndErrors.Errors.AddRange(CustomValidators.GetVndErros(validationResult.Errors));
			return response;
		}

		var accountExist = await _accountReposity.Select(id: null, 
			document: ValidatorExtension.CleanDocument(desactiveAccountRequest.Document), 
			cancellationToken: cancellationToken);

		if (accountExist == null)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = AccountShared._errorCode, Message = "Conta não encontrada." });
			return response;
		}

		if(accountExist.Status == StatusAccount.Inactive)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = AccountShared._errorCode, Message = "A conta já se encontra desativada." });
			return response;
		}

		response.Response =  await _accountReposity.UpdateStatus(accountExist.Id, (int)StatusAccount.Inactive, desactiveAccountRequest.UserName, cancellationToken);

		if(!response.Response)
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = AccountShared._errorCode, Message = "Status não foi atualizado." });

		return response;
	}
}

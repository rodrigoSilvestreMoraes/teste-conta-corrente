using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.BankTransfer.TransferAccount.Model;
using bank.system.Application.Shared.Results;
using FluentValidation;

namespace bank.system.Application.Features.BankTransfer.TransferAccount;
public class TransferAccountUseCase : ITransferAccountUseCase
{
	readonly IAccountReposity _accountReposity;
	readonly IValidator<TransferAccountRequest> _validator;
	public TransferAccountUseCase(
		IAccountReposity accountReposity,
		IValidator<TransferAccountRequest> validator)
	{
		_accountReposity = accountReposity;
		_validator = validator;
	}

	public async Task<AppResponse<TransferAccountResponse>> ExecuteTransfer(TransferAccountRequest transferAccountRequest, CancellationToken cancellationToken)
	{
		var response = new AppResponse<TransferAccountResponse>();
		var validationResult = await _validator.ValidateAsync(transferAccountRequest, cancellationToken);

		//validar dados de entrada
		if (!validationResult.IsValid)
		{
			response.Validation.VndErrors.Errors.AddRange(CustomValidators.GetVndErros(validationResult.Errors));
			return response;
		}

		var fromAccount = await _accountReposity.Select(id: transferAccountRequest.AccountSource, document: null, cancellationToken);
		var toAccount = await _accountReposity.Select(id: transferAccountRequest.AccountDestination, document: null, cancellationToken);

		if(fromAccount != null && fromAccount.Status != StatusAccount.Active)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = TransferShared._errorCode, Message = "Conta origem inválida." });
			return response;
		}

		//Validação preliminar com o saldo
		if (fromAccount?.CurrentBalance < transferAccountRequest.Value)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = TransferShared._errorCode, Message = "Conta origem não possui saldo suficiente." });
			return response;
		}

		if (toAccount != null && toAccount.Status != StatusAccount.Active)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = TransferShared._errorCode, Message = "Conta destino inválida." });
			return response;
		}
		
		//preparar objeto de transferencia 
		//realizar transferencia

		return response;
	}
}

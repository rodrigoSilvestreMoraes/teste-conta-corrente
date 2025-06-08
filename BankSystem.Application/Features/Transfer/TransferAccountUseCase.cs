using bank.system.Application.Domain.Entities;
using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Domain.Repository.Transfer;
using bank.system.Application.Features.Transfer.Model;
using bank.system.Application.Shared.Results;
using FluentValidation;

namespace bank.system.Application.Features.Transfer;
public class TransferAccountUseCase : ITransferAccountUseCase
{
	readonly IAccountReposity _accountReposity;
	readonly ITransferRepository _transferRepository;

	readonly IValidator<TransferAccountRequest> _validator;
	public TransferAccountUseCase(
		ITransferRepository transferRepository,
		IAccountReposity accountReposity,
		IValidator<TransferAccountRequest> validator)
	{
		_transferRepository = transferRepository;
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

		if(fromAccount == null)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = TransferShared._errorCode, Message = "Conta origem não encontrada." });
			return response;
		}
		if (toAccount == null)
		{
			response.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = TransferShared._errorCode, Message = "Conta destino não encontrada." });
			return response;
		}

		if (fromAccount != null && fromAccount.Status != StatusAccount.Active)
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


		//Preparando objetos para transferência
		var transacionId = Guid.NewGuid();
		var releaseDate = DateTime.Now;
		var bankTransferAccountFrom = new BankTransfer
		{
			Id = transacionId,
			AccountId = transferAccountRequest.AccountSource,
			Operation = TypeOperation.Debit,
			ReleaseDate = releaseDate,
			Value = transferAccountRequest.Value,
		};

		var bankTransferAccountTo = new BankTransfer
		{
			Id = transacionId,
			AccountId = transferAccountRequest.AccountDestination,
			Operation = TypeOperation.Credit,
			ReleaseDate = releaseDate,
			Value = transferAccountRequest.Value,
		};

		var balanceUpdateFrom = new Balance { AccountId = transferAccountRequest.AccountSource, UpdateDate = releaseDate };
		var balanceUpdateTo = new Balance { AccountId = transferAccountRequest.AccountDestination, UpdateDate = releaseDate };

		var aggregateTransfer = new TransferAgregate(bankTransferAccountFrom, bankTransferAccountTo, balanceUpdateFrom, balanceUpdateTo, transferAccountRequest.Value);
		var result = await _transferRepository.ExecuteTransfer(aggregateTransfer);

		response.Response = result;

		return response;
	}
}

using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Domain.Repository.Transfer;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Features.Transfer;
using bank.system.Application.Features.Transfer.Model;
using bank.system.Application.Features.Transfer.Validation;
using bank.system.Infrastructure.Repository.Transfer;
using bank.system.Tests.Stub;
using FluentValidation;
using Moq;

namespace bank.system.Tests.Features.Transfer;
public class TransferAccountUseCaseTest
{
	Mock<IAccountReposity> _accountReposity;
	Mock<ITransferRepository> _transferRepository;
	IValidator<TransferAccountRequest> _validator;

	public TransferAccountUseCaseTest()
	{
		_accountReposity = new Mock<IAccountReposity>();
		_transferRepository = new Mock<ITransferRepository>();
		_validator = new TransferAccountRequestValidator();
	}

	[Fact]
	public async void Should_ExecuteTransfer()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		var toAccount = AccountListResponseStub.GetMock();
		toAccount.Id = 44;

		var request = new TransferAccountRequest { AccountSource = fromAccount.Id, AccountDestination = toAccount.Id, Value = 10 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var mockResultTransaction = new TransferAccountResponse { TransactionId = Guid.NewGuid().ToString() };

		_transferRepository.Setup(x => x.ExecuteTransfer(It.IsAny<TransferAgregate>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResultTransaction));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.NotNull(result.Response);
		Assert.NotNull(result.Response.TransactionId);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_fromAccount_Desactive()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		fromAccount.Status = StatusAccount.Inactive;
		var toAccount = AccountListResponseStub.GetMock();
		toAccount.Id = 44;

		var request = new TransferAccountRequest { AccountSource = fromAccount.Id, AccountDestination = toAccount.Id, Value = 10 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());

		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_toAccount_Desactive()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		var toAccount = AccountListResponseStub.GetMock();
		toAccount.Id = 44;
		toAccount.Status = StatusAccount.Inactive;

		var request = new TransferAccountRequest { AccountSource = fromAccount.Id, AccountDestination = toAccount.Id, Value = 10 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());

		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_fromAccount_Not_Balance()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		var toAccount = AccountListResponseStub.GetMock();
		toAccount.Id = 44;

		var request = new TransferAccountRequest { AccountSource = fromAccount.Id, AccountDestination = toAccount.Id, Value = 5000 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());

		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_FromAccount_Null()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		AccountListResponse toAccount = null;
		var request = new TransferAccountRequest { AccountSource = 12, AccountDestination = 13, Value = 10 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());
		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_toAccount_Null()
	{
		AccountListResponse fromAccount = null;
		var toAccount = AccountListResponseStub.GetMock();
		toAccount.Id = 44;
		var request = new TransferAccountRequest { AccountSource = 12, AccountDestination = toAccount.Id, Value = 10 };

		_accountReposity.SetupSequence(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(fromAccount))
			.Returns(Task.FromResult(toAccount));

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());
		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_ExecuteTransfer_Input_Invalid()
	{
		var fromAccount = AccountListResponseStub.GetMock();
		var toAccount = AccountListResponseStub.GetMock();

		var request = new TransferAccountRequest { AccountSource = fromAccount.Id, AccountDestination = toAccount.Id, Value = 10 };

		var useCase = GetUseCase();
		var result = await useCase.ExecuteTransfer(request, new CancellationToken());

		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	ITransferAccountUseCase GetUseCase() => new TransferAccountUseCase(_transferRepository.Object, _accountReposity.Object, _validator);
}

using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Features.Account.Update;
using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Features.Account.Update.Validation;
using bank.system.Tests.Stub;
using FluentValidation;
using Moq;

namespace bank.system.Tests.Features.Account.Update;

public class DesactiveAccountUseCaseTest
{
	Mock<IAccountReposity> _accountReposity;
	IValidator<DesactiveAccountRequest> _validator;

	public DesactiveAccountUseCaseTest()
	{
		_accountReposity = new Mock<IAccountReposity>();
		_validator = new DesactiveAccountRequestValidator();
	}

	[Fact]
	public async void Should_DesativeAccount()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "teste" };
		
		var mockAccountResult = AccountListResponseStub.GetMock();
		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));

		_accountReposity.Setup(x => x.UpdateStatus(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(true));

		var useCase = GetUseCase();
		var result = await useCase.DesactiveAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.NotNull(result.Response);
		Assert.True(result.Response);
	}

	[Fact]
	public async void Should_DesativeAccount_Update_Failed()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "teste" };

		var mockAccountResult = AccountListResponseStub.GetMock();
		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));

		_accountReposity.Setup(x => x.UpdateStatus(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(false));

		var useCase = GetUseCase();
		var result = await useCase.DesactiveAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());
	}

	[Fact]
	public async void ShouldNot_DesativeAccount_Status_Account_Invalid()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "teste" };

		var mockAccountResult = AccountListResponseStub.GetMock();
		mockAccountResult.Status = StatusAccount.Inactive;

		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));
		
		var useCase = GetUseCase();
		var result = await useCase.DesactiveAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());
	}

	[Fact]
	public async void ShouldNot_DesativeAccount_Account_Not_Exist()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "teste" };

		AccountListResponse mockAccountResult = null;
		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));

		
		var useCase = GetUseCase();
		var result = await useCase.DesactiveAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());
	}

	[Fact]
	public async void ShouldNot_DesativeAccount_Document_Invalid()
	{
		var request = new DesactiveAccountRequest { Document = "985454545", UserName = "teste" };

		var useCase = GetUseCase();
		var result = await useCase.DesactiveAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());

	}

	IDesactiveAccountUseCase GetUseCase() => new DesactiveAccountUseCase(_accountReposity.Object, _validator);
}

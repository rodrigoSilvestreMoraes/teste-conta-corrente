using Entities = bank.system.Application.Domain.Entities;
using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.Create;
using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Features.Account.Create.Validation;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Shared.Options;
using bank.system.Tests.Stub;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;

namespace bank.system.Tests.Features.Account.Create;

public class CreateAccountUseCaseTest
{
	Mock<IAccountReposity> _accountReposity;
	IValidator<AccountCreateRequest> _validator;
	IOptions<AccountRulesOption> _accountRulesOption;

	public CreateAccountUseCaseTest()
	{
		_accountReposity = new Mock<IAccountReposity>();
		_validator = new AccountCreateRequestValidator();
		_accountRulesOption = new AccountRulesOptionStub();
	}

	[Fact]
	public async void Should_CreateAccount()
	{
		var request = new AccountCreateRequest
		{
			Document = "666.127.640-15",
			Name = "Teste"
		};

		AccountListResponse mockAccountResult = null;
		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));

		_accountReposity.Setup(x => x.Insert(It.IsAny<Entities.Account>(), It.IsAny<Entities.Balance>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(true));

		var useCase = GetUseCase();
		var result = await useCase.CreateAccountAsync(request, new CancellationToken());

		Assert.NotNull(result);
		Assert.NotNull(result.Response);
		Assert.True(result.Response.Result);
	}

	[Fact]
	public async void ShouldNot_CreateAccount_Input_Invalid()
	{
		var request = new AccountCreateRequest
		{
			Document = "268587877",
			Name = "Teste"
		};

		AccountListResponse mockAccountResult = null;

		var useCase = GetUseCase();
		var result = await useCase.CreateAccountAsync(request, new CancellationToken());
		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	[Fact]
	public async void ShouldNot_CreateAccount_AccountExist()
	{
		var request = new AccountCreateRequest
		{
			Document = "666.127.640-15",
			Name = "Teste"
		};

		AccountListResponse mockAccountResult = AccountListResponseStub.GetMock();
		_accountReposity.Setup(x => x.Select(It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockAccountResult));

		var useCase = GetUseCase();
		var result = await useCase.CreateAccountAsync(request, new CancellationToken());

		UseCaseAssertShared.ValidateErrorDefault(result);
	}

	ICreateAccountUseCase GetUseCase() => new CreateAccountUseCase(_accountReposity.Object, _accountRulesOption, _validator);

}

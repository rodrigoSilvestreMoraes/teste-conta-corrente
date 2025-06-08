using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Features.Account.List;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Tests.Stub;
using Moq;

namespace bank.system.Tests.Features.Account.List;
public  class ListAccountUseCaseTest
{
	Mock<IAccountReposity> _accountReposity;

	public ListAccountUseCaseTest()
	{
		_accountReposity = new Mock<IAccountReposity>();
	}

	[Fact]
	public async void Should_ListAccounts_For_Document()
	{
		var mockResponse = new List<AccountListResponse>();
		mockResponse.Add(AccountListResponseStub.GetMock());

		_accountReposity.Setup(x => x.List(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResponse));

		var useCase = GetUseCase();
		var result = await useCase.ListAccountAsync(document: "666.127.640-15", name: null, new CancellationToken());

		Assert.NotNull(result);
		Assert.NotNull(result.Response);
		Assert.True(result.Response.Any());
	}

	[Fact]
	public async void Should_ListAccounts_For_Name()
	{
		var mockResponse = new List<AccountListResponse>();
		mockResponse.Add(AccountListResponseStub.GetMock());

		_accountReposity.Setup(x => x.List(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResponse));

		var useCase = GetUseCase();
		var result = await useCase.ListAccountAsync(document: null, name: "Teste", new CancellationToken());

		Assert.NotNull(result);
		Assert.NotNull(result.Response);
		Assert.True(result.Response.Any());
	}

	[Fact]
	public async void ShouldNot_ListAccounts_Document_Invalid()
	{
		var mockResponse = new List<AccountListResponse>();
		mockResponse.Add(AccountListResponseStub.GetMock());

		_accountReposity.Setup(x => x.List(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResponse));

		var useCase = GetUseCase();
		var result = await useCase.ListAccountAsync(document: "23565889", name: null, new CancellationToken());

		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());
	}

	[Fact]
	public async void ShouldNot_ListAccounts_Empty_Result()
	{
		var mockResponse = new List<AccountListResponse>();
		mockResponse.Add(AccountListResponseStub.GetMock());

		_accountReposity.Setup(x => x.List(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResponse));

		var useCase = GetUseCase();
		var result = await useCase.ListAccountAsync(document: null, name: null, new CancellationToken());

		Assert.NotNull(result);
		Assert.False(result.Validation.VndErrors.Errors.Any());
	}



	IListAccountUseCase GetUseCase() => new ListAccountUseCase(_accountReposity.Object);
}

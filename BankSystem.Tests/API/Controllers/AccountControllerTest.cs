using bank.system.API.Controllers;
using bank.system.Application.Features.Account.Create;
using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Features.Account.List;
using bank.system.Application.Features.Account.List.Model;
using bank.system.Application.Features.Account.Update;
using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Shared.Results;
using bank.system.Tests.Stub;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace bank.system.Tests.API.Controllers;
public class AccountControllerTest
{
	Mock<ICreateAccountUseCase> _createAccountUseCase;
	Mock<IListAccountUseCase> _listAccountUseCase;
	Mock<IDesactiveAccountUseCase> _desactiveAccountUseCase;

	public AccountControllerTest()
	{
		_createAccountUseCase = new Mock<ICreateAccountUseCase>();
		_desactiveAccountUseCase = new Mock<IDesactiveAccountUseCase>();
		_listAccountUseCase = new Mock<IListAccountUseCase>();
	}

	[Fact]
	public async void Should_CreateAccount()
	{
		var request = new AccountCreateRequest { Document = "666.127.640-15", Name = "Teste unitário Controller" };
		var mockResult = new AppResponse<AccountCreateResponse>();
		mockResult.Response = new AccountCreateResponse { Result = true };

		_createAccountUseCase.Setup(x => x.CreateAccountAsync(It.IsAny<AccountCreateRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().Create(request, new CancellationToken());

		Assert.NotNull(result);
		var okResult = Assert.IsType<ObjectResult>(result);
		Assert.Equal(201, okResult.StatusCode ?? 201);

		var responseObject = Assert.IsType<AccountCreateResponse>(okResult.Value);
		Assert.True(responseObject.Result);
	}

	[Fact]
	public async void ShouldNot_CreateAccount_Input_Invalid()
	{
		var request = new AccountCreateRequest { Document = "23568971", Name = "Teste unitário Controller" };
		var mockResult = new AppResponse<AccountCreateResponse>();
		mockResult.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = "teste", Message = "error" });

		_createAccountUseCase.Setup(x => x.CreateAccountAsync(It.IsAny<AccountCreateRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().Create(request, new CancellationToken());
		ControllerAssertShared.InvalidInputAsserts(result);
	}

	[Fact]
	public async void ShouldNot_CreateAccount_Exception()
	{
		var request = new AccountCreateRequest { Document = "23568971", Name = "Teste unitário Controller" };
		var mockResult = new AppResponse<AccountCreateResponse>();

		_createAccountUseCase.Setup(x => x.CreateAccountAsync(It.IsAny<AccountCreateRequest>(), It.IsAny<CancellationToken>()))
			.Throws(new Exception("ERROR"));

		var result = await GetController().Create(request, new CancellationToken());
		ControllerAssertShared.InternalServerErrorAsserts(result);
	}
	[Fact]
	public async void Should_DesactiveAccount()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		var mockResult = new AppResponse<bool>();
		mockResult.Response = true;

		_desactiveAccountUseCase.Setup(x => x.DesactiveAccountAsync(It.IsAny<DesactiveAccountRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));			

		var result = await GetController().Desactive(request, new CancellationToken());

		Assert.NotNull(result);
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(200, okResult.StatusCode ?? 200);

		var responseObject = Assert.IsType<bool>(okResult.Value);
		Assert.True(responseObject);
	}
	[Fact]
	public async void ShouldNot_DesactiveAccount_Input_Invalid()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		var mockResult = new AppResponse<bool>();
		mockResult.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = "teste", Message = "error" });

		_desactiveAccountUseCase.Setup(x => x.DesactiveAccountAsync(It.IsAny<DesactiveAccountRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().Desactive(request, new CancellationToken());
		ControllerAssertShared.InvalidInputAsserts(result);
	}
	[Fact]
	public async void ShouldNot_DesactiveAccount_Exception()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		
		_desactiveAccountUseCase.Setup(x => x.DesactiveAccountAsync(It.IsAny<DesactiveAccountRequest>(), It.IsAny<CancellationToken>()))
			.Throws(new Exception("ERROR"));

		var result = await GetController().Desactive(request, new CancellationToken());
		ControllerAssertShared.InternalServerErrorAsserts(result);
	}
	[Fact]
	public async void Should_ListAccount()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		var mockResult = new AppResponse<List<AccountListResponse>>();
		mockResult.Response = new List<AccountListResponse>();
		mockResult.Response.Add(AccountListResponseStub.GetMock());

		_listAccountUseCase.Setup(x => x.ListAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().List("666.127.640-15", name: null, new CancellationToken());

		Assert.NotNull(result);
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(200, okResult.StatusCode ?? 200);

		var responseObject = Assert.IsType<List<AccountListResponse>>(okResult.Value);
		Assert.True(responseObject.Any());
	}

	[Fact]
	public async void ShouldNot_ListAccount_NotFound()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		var mockResult = new AppResponse<List<AccountListResponse>>();
		mockResult.Response = new List<AccountListResponse>();

		_listAccountUseCase.Setup(x => x.ListAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().List("666.127.640-15", name: null, new CancellationToken());

		Assert.NotNull(result);
		var okResult = Assert.IsType<ObjectResult>(result);
		Assert.Equal(404, okResult.StatusCode ?? 404);		
	}
	[Fact]
	public async void ShouldNot_ListAccount_Input_Invalid()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };
		var mockResult = new AppResponse<List<AccountListResponse>>();
		mockResult.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = "teste", Message = "error" });

		_listAccountUseCase.Setup(x => x.ListAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().List("666.127.640-15", name: null, new CancellationToken());
		ControllerAssertShared.InvalidInputAsserts(result);
	}
	[Fact]
	public async void ShouldNot_ListAccount_Exception()
	{
		var request = new DesactiveAccountRequest { Document = "666.127.640-15", UserName = "Teste unitário Controller" };

		_listAccountUseCase.Setup(x => x.ListAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.Throws(new Exception("ERROR"));

		var result = await GetController().List("666.127.640-15", name: null, new CancellationToken());
		ControllerAssertShared.InternalServerErrorAsserts(result);
	}
	AccountController GetController() => new AccountController(_createAccountUseCase.Object, _listAccountUseCase.Object, _desactiveAccountUseCase.Object);
}
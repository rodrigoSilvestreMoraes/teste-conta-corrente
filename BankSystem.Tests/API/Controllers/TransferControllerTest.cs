using bank.system.API.Controllers;
using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Features.Transfer;
using bank.system.Application.Features.Transfer.Model;
using bank.system.Application.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace bank.system.Tests.API.Controllers;

public class TransferControllerTest
{
	Mock<ITransferAccountUseCase> _transferAccountUseCase;

	public TransferControllerTest()
	{
		_transferAccountUseCase = new Mock<ITransferAccountUseCase>();
	}

	[Fact]
	public async void Should_TransferAccount()
	{
		var request = new TransferAccountRequest { AccountDestination = 1, AccountSource = 2, Value = 50 };

		var idTransaction = Guid.NewGuid().ToString();
		var mockResult = new AppResponse<TransferAccountResponse>();
		mockResult.Response = new TransferAccountResponse {  TransactionId = idTransaction };

		_transferAccountUseCase.Setup(x => x.ExecuteTransfer(It.IsAny<TransferAccountRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));	

		var result = await GetController().Create(request, new CancellationToken());

		Assert.NotNull(result);
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(200, okResult.StatusCode ?? 200);

		var responseObject = Assert.IsType<TransferAccountResponse>(okResult.Value);
		Assert.Equal(idTransaction, responseObject.TransactionId);
	}
	[Fact]
	public async void ShouldNot_TransferAccount_Input_Invalid()
	{
		var request = new TransferAccountRequest { AccountDestination = 1, AccountSource = 2, Value = 50 };

		var idTransaction = Guid.NewGuid().ToString();
		var mockResult = new AppResponse<TransferAccountResponse>();
		mockResult.Validation.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = "teste", Message = "error" });

		_transferAccountUseCase.Setup(x => x.ExecuteTransfer(It.IsAny<TransferAccountRequest>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockResult));

		var result = await GetController().Create(request, new CancellationToken());
		ControllerAssertShared.InvalidInputAsserts(result);	
	}
	[Fact]
	public async void ShouldNot_TransferAccount_Exception()
	{
		var request = new TransferAccountRequest { AccountDestination = 1, AccountSource = 2, Value = 50 };

		_transferAccountUseCase.Setup(x => x.ExecuteTransfer(It.IsAny<TransferAccountRequest>(), It.IsAny<CancellationToken>()))
			.Throws(new Exception("ERROR"));

		var result = await GetController().Create(request, new CancellationToken());
		ControllerAssertShared.InternalServerErrorAsserts(result);
	}

	TransferController GetController() => new TransferController(_transferAccountUseCase.Object);
}

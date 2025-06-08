using Microsoft.AspNetCore.Mvc;
using bank.system.Application.Features.Transfer;
using bank.system.Application.Shared.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System.Threading;
using System;
using bank.system.Application.Features.Transfer.Model;

namespace bank.system.API.Controllers
{
	[Route("api/transfer")]
	[ApiController]
	public class TransferController : ControllerBase
	{
		const string _tag = "Transferêcias entre contas";

		readonly ITransferAccountUseCase _transferAccountUseCase;
		public TransferController(ITransferAccountUseCase transferAccountUseCase)
		{
			_transferAccountUseCase = transferAccountUseCase;
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Realiza transferência entre contas.", Tags = new[] { _tag })]
		[ProducesResponseType<TransferAccountResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] TransferAccountRequest transferAccountRequest, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _transferAccountUseCase.ExecuteTransfer(transferAccountRequest, cancellationToken);
				if (!result.Invalid)
					return Ok(result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				//_ = _customLog.GravarLog(CustomLogRequest.Create(Global.API, ControllerContext.ActionDescriptor.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, CustomValidators.GetDefaultInternalServerError());
			}
		}
	}
}

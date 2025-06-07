using bank.system.Application.Features.Account.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using bank.system.Application.Features.Account.Create.Model;
using System.Threading;
using bank.system.Application.Shared.Results;

namespace bank.system.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		readonly ICreateAccountUseCase _createAccountUseCase;
		const string _tag = "Gestão de Contas";

		public AccountController(ICreateAccountUseCase createAccountUseCase)
		{
			_createAccountUseCase = createAccountUseCase;
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Cria uma conta.", Tags = new[] { _tag })]
		[ProducesResponseType<bool>(StatusCodes.Status201Created)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody]AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken) 
		{
			try
			{
				var result = await _createAccountUseCase.CreateAccountAsync(accountCreateRequest, cancellationToken);
				if (!result.Invalid) return Ok(result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				//_ = _customLog.GravarLog(CustomLogRequest.Create(Global.API, ControllerContext.ActionDescriptor.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

	}
}

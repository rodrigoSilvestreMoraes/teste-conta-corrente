using bank.system.Application.Features.Account.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using bank.system.Application.Features.Account.Create.Model;
using System.Threading;
using bank.system.Application.Shared.Results;
using bank.system.Application.Features.Account.List.Model;
using System.Collections.Generic;
using bank.system.Application.Features.Account.List;
using System.Linq;
using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Features.Account.Update;

namespace bank.system.API.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		readonly ICreateAccountUseCase _createAccountUseCase;
		readonly IListAccountUseCase _listAccountUseCase;
		readonly IDesactiveAccountUseCase _desactiveAccountUseCase;

		const string _tag = "Gestão de Contas";

		public AccountController(
			ICreateAccountUseCase createAccountUseCase,
			IListAccountUseCase listAccountUseCase,
			IDesactiveAccountUseCase desactiveAccountUseCase)
		{
			_createAccountUseCase = createAccountUseCase;
			_listAccountUseCase  = listAccountUseCase;
			_desactiveAccountUseCase = desactiveAccountUseCase;
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Cria uma conta.", Tags = new[] { _tag })]
		[ProducesResponseType<AccountCreateResponse>(StatusCodes.Status201Created)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody]AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken) 
		{
			try
			{
				var result = await _createAccountUseCase.CreateAccountAsync(accountCreateRequest, cancellationToken);
				if (!result.Invalid) 
					return StatusCode(StatusCodes.Status201Created,result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, CustomValidators.GetDefaultInternalServerError());
			}
		}

		[HttpPut("desactive")]
		[SwaggerOperation(Summary = "Desativa uma determinada conta.", Tags = new[] { _tag })]
		[ProducesResponseType<AccountCreateResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Desactive([FromBody] DesactiveAccountRequest desactiveAccountRequest, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _desactiveAccountUseCase.DesactiveAccountAsync(desactiveAccountRequest, cancellationToken);
				if (!result.Invalid)
					return Ok(result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, CustomValidators.GetDefaultInternalServerError());
			}
		}

		[HttpGet("list")]
		[SwaggerOperation(Summary = "Pesquisa contas.", Tags = new[] { _tag })]
		[ProducesResponseType<List<AccountListResponse>>(StatusCodes.Status200OK)]
		[ProducesResponseType<List<AccountListResponse>>(StatusCodes.Status404NotFound)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType<RestClientVndErrors>(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> List([FromQuery]string? document, [FromQuery]string? name, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _listAccountUseCase.ListAccountAsync(document, name, cancellationToken);
				if (!result.Invalid)
				{
					if (result.Response.Any())
						return Ok(result.Response);

					return StatusCode(StatusCodes.Status404NotFound, result.Response);
				}
				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, CustomValidators.GetDefaultInternalServerError());
			}
		}
	}
}

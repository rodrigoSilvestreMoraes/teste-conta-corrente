using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Application.Shared.Results
{
	[ExcludeFromCodeCoverage]
	public static class CustomValidators
	{
		public static RestClientVndErrors GetDefaultInternalServerError()
		{
			var vndError = new RestClientVndErrors();
			vndError.VndErrors.Errors.Add(new ErrorDetail { ErrorCode = "InternalServerError", Message = "Ocorreu um erro inesperado, se o erro persistir contate o suporte." });
			return vndError;
		}
		public static List<ErrorDetail> GetVndErros(List<ValidationFailure> validationResult)
			=> validationResult.Select(x => new ErrorDetail { ErrorCode = x.PropertyName, Message = x.ErrorMessage }).ToList();
	}
}

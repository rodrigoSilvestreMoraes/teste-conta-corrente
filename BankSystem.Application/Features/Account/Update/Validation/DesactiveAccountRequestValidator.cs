using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Shared.Extension;
using FluentValidation;

namespace bank.system.Application.Features.Account.Update.Validation;

public class DesactiveAccountRequestValidator : AbstractValidator<DesactiveAccountRequest>
{
	public DesactiveAccountRequestValidator()
	{
		RuleFor(x => x.UserName)
			.NotEmpty().WithMessage("O nome é obrigatório.");

		RuleFor(x => x.Document)
			.NotEmpty().WithMessage("O documento é obrigatório.")
			.Must(ValidatorExtension.IsValidCpfOrCnpj)
			.WithMessage("O documento informado não é um CPF ou CNPJ válido.");
	}
}

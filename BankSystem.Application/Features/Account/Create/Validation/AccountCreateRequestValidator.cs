using bank.system.Application.Features.Account.Create.Model;
using bank.system.Application.Shared.Extension;
using FluentValidation;

namespace bank.system.Application.Features.Account.Create.Validation;
public class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest>
{
	public AccountCreateRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("O nome é obrigatório.");

		RuleFor(x => x.Document)
			.NotEmpty().WithMessage("O documento é obrigatório.")
			.Must(ValidatorExtension.IsValidCpfOrCnpj)
			.WithMessage("O documento informado não é um CPF ou CNPJ válido.");
	}
}

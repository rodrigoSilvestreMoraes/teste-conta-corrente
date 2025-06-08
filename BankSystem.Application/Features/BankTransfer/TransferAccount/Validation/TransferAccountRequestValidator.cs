using bank.system.Application.Features.BankTransfer.TransferAccount.Model;
using FluentValidation;

namespace bank.system.Application.Features.BankTransfer.TransferAccount.Validation;
public class TransferAccountRequestValidator : AbstractValidator<TransferAccountRequest>
{
	public TransferAccountRequestValidator()
	{
		RuleFor(x => x.AccountSource)
			.NotEmpty().WithMessage("Conta origem é obrigatório.");

		RuleFor(x => x.AccountDestination)
			.NotEmpty().WithMessage("Conta destino é obrigatório.");

		RuleFor(x => x.Value)
			.NotEmpty().WithMessage("Valor da transferência é obrigatório.")
			.GreaterThan(0).WithMessage("Valor da transferência deve ser maior que zero.");

		RuleFor(x => x)
			.Must(x => x.AccountSource != x.AccountDestination)
			.WithMessage("A conta de origem e destino não podem ser iguais.");
	}
}

using bank.system.Application.Domain.Entities;

namespace bank.system.Application.Features.Transfer.Model;

public class TransferAgregate
{
	public TransferAgregate(
		BankTransfer bankTransferFrom, 
		BankTransfer bankTransferTo,
		Balance balanceFrom,
		Balance balanceTo,
		decimal valueTransfer)
	{
		BankTransferFrom = bankTransferFrom;
		BalanceFrom = balanceFrom;

		BankTransferTo = bankTransferTo;
		BalanceTo = balanceTo;

		ValueTransfer = valueTransfer;
	}

	public BankTransfer BankTransferFrom {  get; set; }
	public Balance BalanceFrom { get; set; }

	public BankTransfer BankTransferTo { get; set; }
	public Balance BalanceTo { get; set; }

	public decimal ValueTransfer {  get; set; }
}

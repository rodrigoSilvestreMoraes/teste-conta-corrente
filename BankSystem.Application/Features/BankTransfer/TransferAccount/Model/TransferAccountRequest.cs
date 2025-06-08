namespace bank.system.Application.Features.BankTransfer.TransferAccount.Model;
public class TransferAccountRequest
{
	public long AccountSource {  get; set; }
	public long AccountDestination { get; set; }
	public decimal Value { get; set; }
}

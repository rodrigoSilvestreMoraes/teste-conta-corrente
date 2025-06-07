namespace bank.system.Application.Domain.Entities
{
	public class BankTransferP2P
	{
		public Guid TransferId { get; set; } 
		public long SourceAccount {  get; set; } 
		public long DestinationAccount { get; set ; } 
	}
}

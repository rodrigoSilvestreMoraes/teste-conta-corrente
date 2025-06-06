namespace bank_system.Application.Domain.Entities
{
	public class BankTransferP2P
	{
		public Guid TransferId { get; set; } //Chave primaria e chave estrangeria da tabela BankTranfer
		public long SourceAccount {  get; set; } // Não aceita nulo, chave estrangeira com a tabela Account
		public long DestinationAccount { get; set ; } // Não aceita nulo, chave estrangeira com a tabela Account
	}
}

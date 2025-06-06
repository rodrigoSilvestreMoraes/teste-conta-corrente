namespace bank_system.Application.Domain.Entities
{
	public class Balance
	{
		public long AccountId { get; set; } //Id da conta, chave primaria e tem relacao com a tabela Account
		public decimal CurrentBalance { get; set; } //Não aceita nulo
		public DateTime UpdateDate { get; set; } //Não aceita nulo
	}
}

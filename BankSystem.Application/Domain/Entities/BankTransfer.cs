using bank_system.Application.Domain.Entities.Enums;

namespace bank_system.Application.Domain.Entities
{
	public class BankTransfer
	{
		public Guid Id { get; set; } // Chave Primaria
		public long AccountId { get; set; } //Chave estrangeira com a tabela Account, não aceita nulo
		public DateTime ReleaseDate { get; set; } // não aceita nulo
		public TypeOperation Operation { get; set; } //não aceita nulo 
		public decimal Value { get; set; } //não aceita nulo
	}
}

using bank_system.Application.Domain.Entities.Enums;

namespace bank_system.Application.Domain.Entities
{
	public class Account
	{
		public long Id { get; set; } // chave primária
		public string Name { get; set; } //Indice para busca
		public string Document { get; set; } //Indice para busca
		public StatusAccount Status { get; set; } //não aceita nulo
		public DateTime OpeningDate { get; set; } //não aceita nulo
		public DateTime UpdateDate {  get; set; } // aceita nulo
	}

}

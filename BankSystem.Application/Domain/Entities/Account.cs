using bank.system.Application.Domain.Entities.Enums;
namespace bank.system.Application.Domain.Entities
{
	public class Account
	{
		public long Id { get; set; } 
		public string Name { get; set; } 
		public string Document { get; set; } 
		public StatusAccount Status { get; set; } 
		public DateTime OpeningDate { get; set; } 
		public DateTime? UpdateDate {  get; set; } 
	}
}

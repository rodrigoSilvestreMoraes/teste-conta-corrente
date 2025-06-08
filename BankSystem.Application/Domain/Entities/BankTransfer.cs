using bank.system.Application.Domain.Entities.Enums;
namespace bank.system.Application.Domain.Entities;
public class BankTransfer
{
	public Guid Id { get; set; } 
	public long AccountId { get; set; } 
	public DateTime ReleaseDate { get; set; } 
	public TypeOperation Operation { get; set; } 
	public decimal Value { get; set; } 
}

namespace bank.system.Application.Domain.Entities;
public class Balance
{
	public long AccountId { get; set; } 
	public decimal CurrentBalance { get; set; } 
	public DateTime UpdateDate { get; set; } 
}

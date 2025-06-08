using bank.system.Application.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace bank.system.Application.Features.Account.List.Model;
public class AccountListResponse
{
	[JsonIgnore]
	public long Id { get; set; }
	public string Name { get; set; }
	public string Document { get; set; }
	public decimal CurrentBalance { get; set; }
	public DateTime OpeningDate { get; set; }
	public StatusAccount Status { get; set; }
}

namespace bank.system.Application.Features.Account.List.Model
{
	public class AccountListResponse
	{
		public string Name { get; set; }
		public string Document { get; set; }
		public decimal CurrentBalance { get; set; }
		public DateTime OpeningDate { get; set; }
		public int Status { get; set; }
	}
}

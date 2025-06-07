namespace bank.system.Application.Domain.Repository.Account
{
	public interface IAccountReposity
	{
		Task<bool> Insert(Entities.Account account, Entities.Balance balance, CancellationToken cancellationToken);
	}
}

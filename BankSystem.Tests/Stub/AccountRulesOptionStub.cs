using bank.system.Application.Shared.Options;
using Microsoft.Extensions.Options;

namespace bank.system.Tests.Stub;

internal class AccountRulesOptionStub : IOptions<AccountRulesOption>
{
	public AccountRulesOption Value { get; }
	
	public AccountRulesOptionStub()
	{
		Value = new AccountRulesOption
		{
			 InitialBalance = 1000
		};
	}
}

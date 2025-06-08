using System.Diagnostics.CodeAnalysis;

namespace bank.system.Application.Shared.Options;

[ExcludeFromCodeCoverage]
public class AccountRulesOption
{
	public decimal InitialBalance { get; set; }
}

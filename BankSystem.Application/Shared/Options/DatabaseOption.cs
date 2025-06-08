using System.Diagnostics.CodeAnalysis;

namespace bank.system.Application.Shared.Options;

[ExcludeFromCodeCoverage]
public class DatabaseOption
{
	public string ConnectionString {  get; set; }
}

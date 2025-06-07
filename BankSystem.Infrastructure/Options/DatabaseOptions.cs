using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.Options
{
	[ExcludeFromCodeCoverage]
	public class DatabaseOptions
	{
		public string ConnectionString {  get; set; }
	}
}

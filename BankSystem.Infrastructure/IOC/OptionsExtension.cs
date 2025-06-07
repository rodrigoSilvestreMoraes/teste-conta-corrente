using bank.system.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.IOC
{
	[ExcludeFromCodeCoverage]
	public static class OptionsExtension
	{
		public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) 
		{
			if(configuration is null)
				throw new ArgumentNullException(nameof(configuration));

			return services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseConfig"));			
		}
	}
}

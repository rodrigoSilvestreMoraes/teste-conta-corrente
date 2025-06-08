using bank.system.Application.Domain.Repository.Account;
using bank.system.Application.Shared.Options;
using bank.system.Infrastructure.Repository.Account;
using bank.system.Infrastructure.Repository.Connection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.IOC;

[ExcludeFromCodeCoverage]
public static class RepositoryExtension
{
	public static IServiceCollection AddRepositorys(this IServiceCollection services) 
	{
		services.AddSingleton<IConnectionFactory, ConnectionFactory>();

		services.AddSingleton(opt =>
		{
			var options = opt.GetRequiredService<IOptions<DatabaseOption>>();
			return new NpgsqlDataSourceBuilder(options.Value.ConnectionString).Build();
		});

		services.AddSingleton<IAccountReposity, AccountRepository>();

		return services;
	}
}

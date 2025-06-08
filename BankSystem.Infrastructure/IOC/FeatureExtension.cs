using bank.system.Application.Features.Account.Create;
using bank.system.Application.Features.Account.List;
using bank.system.Application.Features.Account.Update;
using bank.system.Application.Features.Transfer;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.IOC;

[ExcludeFromCodeCoverage]
public static class FeatureExtension
{
	public static IServiceCollection AddFeatures(this IServiceCollection services)
	{
		services.AddScoped<ICreateAccountUseCase, CreateAccountUseCase>()
				.AddScoped<IListAccountUseCase, ListAccountUseCase>()
				.AddScoped<IDesactiveAccountUseCase, DesactiveAccountUseCase>()
				.AddScoped<ITransferAccountUseCase, TransferAccountUseCase>();

		return services;
	}
}

using bank.system.Application.Features.Account.Create;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.Infrastructure.IOC
{
	[ExcludeFromCodeCoverage]
	public static class FeatureExtension
	{
		public static IServiceCollection AddFeatures(this IServiceCollection services)
		{
			services.AddSingleton<ICreateAccountUseCase, CreateAccountUseCase>();

			return services;
		}
	}
}

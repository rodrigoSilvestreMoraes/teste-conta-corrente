using bank.system.Application.Features.Account.Create.Validation;
using bank.system.Application.Features.Account.Update.Validation;
using bank.system.Application.Features.Transfer.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace bank.system.Infrastructure.IOC;

[ExcludeFromCodeCoverage]
public static class ValidationDependencyExtension
{
	public static IServiceCollection AddValidations(this IServiceCollection services)
	{
		services.AddControllers()
			.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

		services.AddValidatorsFromAssemblyContaining<AccountCreateRequestValidator>()
				.AddValidatorsFromAssemblyContaining<DesactiveAccountRequestValidator>()
				.AddValidatorsFromAssemblyContaining<TransferAccountRequestValidator>();

		return services;
	}
}

using bank.system.Application.Features.Account.Create.Validation;
using bank.system.Application.Features.Account.Update.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace bank.system.Infrastructure.IOC;
public static class ValidationDependencyExtension
{
	public static IServiceCollection AddValidations(this IServiceCollection services)
	{
		services.AddControllers()
			.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

		services.AddValidatorsFromAssemblyContaining<AccountCreateRequestValidator>()
				.AddValidatorsFromAssemblyContaining<DesactiveAccountRequestValidator>();

		return services;
	}
}

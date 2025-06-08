using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using bank.system.Infrastructure.IOC;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;
using bank.system.Application.Shared.Results;
using FluentValidation.AspNetCore;
using System.IO;
using System.Reflection;

namespace bank.system.API
{
	[ExcludeFromCodeCoverage]
	public class Startup
	{
		public IConfiguration Configuration { get; set; }
		const string _title = "API Bank System";
		const string _version = "v1.0.0";

		public Startup()
		{
			var builder = new ConfigurationBuilder();
			builder.AddJsonFile("appsettings.json").AddEnvironmentVariables();
			builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore().AddApiExplorer();
			services.AddMvcCore().AddControllersAsServices();
			services.AddMemoryCache();
			services.AddCors();
			services.AddControllers();

			services.AddSettings(Configuration);
			services.AddRepositorys();
			services.AddFeatures();
			services.AddValidations();

			services.AddControllersWithViews()
				.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = _title,
					Version = _version
				});

				c.EnableAnnotations();
				c.CustomSchemaIds(x => x.FullName);

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				var appXmlFile = "bank.system.Application.xml";
				var appXmlPath = Path.Combine(AppContext.BaseDirectory, appXmlFile);
				c.IncludeXmlComments(appXmlPath);
				c.IncludeXmlComments(xmlPath);
			});
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(a => a.Run(async context =>
			{
				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				var exception = exceptionHandlerPathFeature.Error;

				var vndErrors = new RestClientVndErrors();
				vndErrors.VndErrors.Errors.Add(new ErrorDetail
				{
					ErrorCode = "InternalServerError",
					Message = exception.Message
				});

				var result = JsonConvert.SerializeObject(vndErrors);

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsync(result);

			}));

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"/swagger/v1/swagger.json", _title);
				c.RoutePrefix = "swagger";
			});

			app.UseRouting();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace bank.system.API
{

	[ExcludeFromCodeCoverage]
	public static class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}
		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args).UseKestrel().UseStartup<Startup>();
		}
	}
}

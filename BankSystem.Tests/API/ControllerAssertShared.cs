using bank.system.Application.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace bank.system.Tests.API;

public static class ControllerAssertShared
{
	public static void InvalidInputAsserts(IActionResult result)
	{
		Assert.NotNull(result);
		var okResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(400, okResult.StatusCode ?? 400);

		var responseObject = Assert.IsType<RestClientVndErrors>(okResult.Value);
		Assert.True(responseObject.VndErrors.Errors.Any());
	}
	public static void InternalServerErrorAsserts(IActionResult result)
	{
		Assert.NotNull(result);
		var okResult = Assert.IsType<ObjectResult>(result);
		Assert.Equal(500, okResult.StatusCode ?? 500);

		var responseObject = Assert.IsType<RestClientVndErrors>(okResult.Value);
		Assert.True(responseObject.VndErrors.Errors.Any());
	}
}

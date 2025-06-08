using bank.system.Application.Shared.Results;

namespace bank.system.Tests.Features;

public static class UseCaseAssertShared
{
	public static void ValidateErrorDefault<T>(AppResponse<T> result)
	{
		Assert.NotNull(result);
		Assert.True(result.Invalid);
		Assert.True(result.Validation.VndErrors.Errors.Any());
	}
}

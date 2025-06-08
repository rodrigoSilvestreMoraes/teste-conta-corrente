using Newtonsoft.Json;

namespace bank.system.Application.Shared.Results;
public class RestClientVndErrors
{
	[JsonProperty("_embedded")]
	public Embedded VndErrors { get; set; } = new();

	public static RestClientVndErrors CreateError(string message, string errorCode)
	{
		return new RestClientVndErrors
		{
			VndErrors = new Embedded
			{
				Errors = new List<ErrorDetail>
			{
				new ErrorDetail
				{
					Message = message,
					ErrorCode = errorCode
				}
			}
			}
		};
	}

	public static RestClientVndErrors CreateError(ErrorDetail errorDetail)
	{
		return new RestClientVndErrors
		{
			VndErrors = new Embedded
			{
				Errors = new List<ErrorDetail> { errorDetail }
			}
		};
	}
}

public class Embedded
{
	[JsonProperty("errors")]
	public List<ErrorDetail> Errors { get; set; } = new();
}
public class ErrorDetail
{
	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("errorCode")]
	public string ErrorCode { get; set; }
}

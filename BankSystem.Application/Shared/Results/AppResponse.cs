namespace bank.system.Application.Shared.Results;
public class AppResponse<T>
{
	public T Response { get; set; }
	public RestClientVndErrors Validation { get; set; } = new RestClientVndErrors();
	public bool Invalid
	{
		get
		{
			if (Validation.VndErrors.Errors.Any()) return true;
			return false;
		}
	}
}

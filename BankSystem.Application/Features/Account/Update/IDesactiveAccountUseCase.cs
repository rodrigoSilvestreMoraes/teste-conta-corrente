using bank.system.Application.Features.Account.Update.Model;
using bank.system.Application.Shared.Results;

namespace bank.system.Application.Features.Account.Update;
public interface IDesactiveAccountUseCase
{
	Task<AppResponse<bool>> DesactiveAccountAsync(DesactiveAccountRequest desactiveAccountRequest, CancellationToken cancellationToken);
}

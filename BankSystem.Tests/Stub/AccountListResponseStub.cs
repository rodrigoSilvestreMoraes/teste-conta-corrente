using bank.system.Application.Domain.Entities.Enums;
using bank.system.Application.Features.Account.List.Model;

namespace bank.system.Tests.Stub;

internal static class AccountListResponseStub
{
	internal static AccountListResponse GetMock()
	{
		return new AccountListResponse
		{ 
			 CurrentBalance = 10,
			 Document = "666.127.640-15",
			 Id = 1,
			 Name = "Test",
			 OpeningDate = DateTime.Now,
			 Status = StatusAccount.Active
		};
	}
}

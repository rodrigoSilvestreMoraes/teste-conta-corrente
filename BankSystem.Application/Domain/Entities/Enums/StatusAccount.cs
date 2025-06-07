using System.ComponentModel;

namespace bank.system.Application.Domain.Entities.Enums
{
	public enum StatusAccount
	{
		[Description("Inativo")]
		Inactive = 0,

		[Description("Ativo")]
		Active = 1		
	}
}

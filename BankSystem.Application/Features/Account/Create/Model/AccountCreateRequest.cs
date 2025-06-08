namespace bank.system.Application.Features.Account.Create.Model;

/// <summary>
/// Representa os dados necessários para criar uma conta bancária.
/// </summary>
public class AccountCreateRequest
{
	/// <summary>
	/// Nome completo do titular da conta. Este campo é obrigatório.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Documento do titular (CPF com 11 dígitos ou CNPJ com 14 dígitos, sem pontos ou traços). Este campo é obrigatório.
	/// </summary>
	public string Document {  get; set; }
}

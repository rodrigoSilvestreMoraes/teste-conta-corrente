namespace bank.system.Application.Features.Account.Update.Model;

/// <summary>
/// Representa a requisição para desativar uma conta.
/// </summary>
public class DesactiveAccountRequest
{
	/// <summary>
	/// Documento do titular da conta (CPF ou CNPJ, sem formatação).
	/// </summary>
	public string Document { get; set; }

	/// <summary>
	/// Nome do usuário que está solicitando a desativação.
	/// </summary>
	public string UserName { get; set; }
}

using bank.system.Application.Domain.Entities.Enums;

namespace bank.system.Application.Features.Account.List.Model;

/// <summary>
/// Representa os dados de uma conta listada na resposta da API.
/// </summary>
public class AccountListResponse
{
	/// <summary>
	/// Identificador único da conta.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Nome do titular da conta.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Documento do titular (CPF ou CNPJ, sem formatação).
	/// </summary>
	public string Document { get; set; }

	/// <summary>
	/// Saldo atual da conta.
	/// </summary>
	public decimal CurrentBalance { get; set; }

	/// <summary>
	/// Data de abertura da conta.
	/// </summary>
	public DateTime OpeningDate { get; set; }

	/// <summary>
	/// Situação atual da conta (ex: Ativa, Inativa).
	/// </summary>
	public StatusAccount Status { get; set; }
}

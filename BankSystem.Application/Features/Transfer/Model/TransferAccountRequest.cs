namespace bank.system.Application.Features.Transfer.Model;

/// <summary>
/// Representa a requisição para realizar uma transferência entre contas.
/// </summary>
public class TransferAccountRequest
{
	/// <summary>
	/// Identificador da conta de origem.
	/// </summary>
	public long AccountSource { get; set; }

	/// <summary>
	/// Identificador da conta de destino.
	/// </summary>
	public long AccountDestination { get; set; }

	/// <summary>
	/// Valor a ser transferido.
	/// </summary>
	public decimal Value { get; set; }
}

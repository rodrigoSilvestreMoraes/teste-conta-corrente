namespace bank.system.Application.Features.Transfer.Model;

/// <summary>
/// Representa a resposta da operação de transferência entre contas.
/// </summary>
public class TransferAccountResponse
{
	/// <summary>
	/// Identificador da transação gerado para a transferência.
	/// </summary>
	public string TransactionId { get; set; }
}

using System.Text.RegularExpressions;

namespace bank.system.Application.Shared.Extension;
public static class ValidatorExtension
{
	public static bool IsValidCpfOrCnpj(string document)
	{
		if (string.IsNullOrWhiteSpace(document))
			return false;

		var cleaned = CleanDocument(document);

		return cleaned.Length switch
		{
			11 => IsValidCpf(cleaned),
			14 => IsValidCnpj(cleaned),
			_ => false
		};
	}

	public static string CleanDocument(string document)
	{
		return Regex.Replace(document ?? "", @"[^\d]", "");
	}
	public static bool IsValidCpf(string cpf)
	{
		cpf = CleanDocument(cpf);

		if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
			return false;

		var digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();

		int sum1 = 0, sum2 = 0;
		for (int i = 0; i < 9; i++)
		{
			sum1 += digits[i] * (10 - i);
			sum2 += digits[i] * (11 - i);
		}

		int digit1 = sum1 % 11 < 2 ? 0 : 11 - (sum1 % 11);
		sum2 += digit1 * 2;
		int digit2 = sum2 % 11 < 2 ? 0 : 11 - (sum2 % 11);

		return digits[9] == digit1 && digits[10] == digit2;
	}
	public static bool IsValidCnpj(string cnpj)
	{
		cnpj = CleanDocument(cnpj);

		if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
			return false;

		int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
		int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

		var temp = cnpj[..12];
		int sum1 = 0;
		for (int i = 0; i < 12; i++)
			sum1 += int.Parse(temp[i].ToString()) * multiplicador1[i];

		int digit1 = sum1 % 11 < 2 ? 0 : 11 - (sum1 % 11);
		temp += digit1;
		int sum2 = 0;
		for (int i = 0; i < 13; i++)
			sum2 += int.Parse(temp[i].ToString()) * multiplicador2[i];

		int digit2 = sum2 % 11 < 2 ? 0 : 11 - (sum2 % 11);

		return cnpj.EndsWith($"{digit1}{digit2}");
	}
}
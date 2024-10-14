using System.Text;
using System.Text.RegularExpressions;

namespace IOBBank.Core.Extensions;

public static class StringExtensions
{
    public static string FormarChaveAleatoria(this string str, int lengthChave)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, lengthChave)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string ApenasNumeros(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        return new string(str.Where(char.IsDigit).ToArray());
    }
    public static string ParaNomeDiaDaSemana(this int valor)
    {
        switch (valor)
        {
            case 0:
                return "Domingo";
            case 1:
                return "Segunda-feira";
            case 2:
                return "Terça-feira";
            case 3:
                return "Quarta-feira";
            case 4:
                return "Quinta-feira";
            case 5:
                return "Sexta-feira";
            case 6:
                return "Sábado";
            default:
                return "Domingo";
        }
    }

    public static string ToPlural(this string word, int quantity)
    {
        if (quantity > 1)
        {
            return word + "s";
        }
        return word;
    }

    public static bool ValidaExisteApenasLetras(this string str)
    {
        return string.IsNullOrEmpty(str) ? false : str.Any(char.IsLetter);
    }
    public static bool ValidaExisteApenasNumeros(this string str)
    {
        return string.IsNullOrEmpty(str) ? false : str.Any(char.IsNumber);
    }

    public static string ApenasLetrasNumeros(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        var rgx = new Regex("[^a-zA-Z0-9]");
        return rgx.Replace(str, "");
    }

    public static string FormatarCNPJ(this string str)
    {
        return Convert.ToUInt64(str).ToString(@"00\.000\.000\/0000\-00");
    }

    public static string FormatarCPF(this string str)
    {
        return Convert.ToUInt64(str).ToString(@"000\.000\.000\-00");
    }

    public static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;

        string valor = cpf.Replace(".", "");
        valor = valor.Replace("-", "");

        if (valor.Length != 11)

            return false;

        bool igual = true;

        for (int i = 1; i < 11 && igual; i++)

            if (valor[i] != valor[0])

                igual = false;


        if (igual || valor == "12345678909")
            return false;

        int[] numeros = new int[11];

        for (int i = 0; i < 11; i++)
        {
            numeros[i] = int.Parse(valor[i].ToString());
        }

        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (10 - i) * numeros[i];

        int resultado = soma % 11;

        if (resultado == 1 || resultado == 0)
        {
            if (numeros[9] != 0)

                return false;
        }

        else if (numeros[9] != 11 - resultado)

            return false;

        soma = 0;
        for (int i = 0; i < 10; i++)

            soma += (11 - i) * numeros[i];

        resultado = soma % 11;

        if (resultado == 1 || resultado == 0)
        {
            if (numeros[10] != 0)

                return false;
        }
        else
        {
            if (numeros[10] != 11 - resultado)
            {
                return false;
            }
        }
        return true;
    }

    public static string OcultarNome(string nomeCompleto)
    {
        // Separar o nome completo em partes
        var partes = nomeCompleto.Trim().Split(' ');

        if (partes.Length < 2)
        {
            // Se houver apenas um nome, retorna o nome original
            return partes[0];
        }

        // O primeiro sobrenome permanece visível
        string sobrenomeVisivel = partes[1];

        // O primeiro nome é ocultado com asteriscos
        string primeiroNomeOcultado = new string('*', partes[0].Length);

        // As demais partes (exceto o primeiro sobrenome) são substituídas por asteriscos
        string restanteOcultado = string.Join(" ", partes.Skip(2).Select(p => new string('*', p.Length)));

        // Retorna o resultado formatado
        return $"{primeiroNomeOcultado} {sobrenomeVisivel} {restanteOcultado}";
    }

    public static bool ValidarCNPJ(string cnpj)
    {

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma;
        int resto;
        string digito;
        string tempCnpj;

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        resto = (soma % 11);

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }

    public static bool ValidarCpfOuCnpj(string cpfCnpj)
    {
        if (cpfCnpj.Length == 11)
        {
            return ValidarCpf(cpfCnpj);
        }
        else if (cpfCnpj.Length == 14)
        {
            return ValidarCNPJ(cpfCnpj);
        }

        return false;
    }


    public static bool ValidaBase64(this string base64)
    {
        if (!string.IsNullOrEmpty(base64) || !string.IsNullOrWhiteSpace(base64))
        {
            return Convert.TryFromBase64String(base64, new byte[base64.Length], out var base64EmBytes);
        }

        return false;
    }

    public static string LimparCaracteresEspeciais(string input)
    {
        input = input.Replace("ç", "c");
        input = input.Replace("Ç", "C");

        return Regex.Replace(input, "[^a-zA-Z0-9_.-]", "_");
    }
}

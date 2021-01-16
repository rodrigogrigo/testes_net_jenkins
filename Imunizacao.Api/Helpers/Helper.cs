using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace RgCidadao.Api.Helpers
{
    public static class Helper
    {
        public static string GetVersao = $@"2020.12.1.9";

        public static string GerarSenhaProvisoria()
        {
            Random random = new Random();
            var senhaprovisoria = random.Next(10000000, 99999999);
            return senhaprovisoria.ToString();
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string RemoveCaracteresTelefone(string telefone)
        {
            telefone = telefone.Replace(" ", "");
            telefone = telefone.Replace("(", "");
            telefone = telefone.Replace(")", "");
            telefone = telefone.Replace("-", "");

            return telefone;
        }

        public static string GetFiltro(string fields, string search)
        {
            try
            {
                string filtro = string.Empty;
                foreach (var item in fields.Split(","))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += " OR ";

                    filtro += $"{item} CONTAINING '{search}'";
                }
                filtro = "AND (" + filtro + ")";

                return filtro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetFiltroInicial(string fields, string search)
        {
            try
            {
                string filtro = string.Empty;
                foreach (var item in fields.Split(","))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += " OR ";

                    filtro += $"{item} CONTAINING '{search}'";
                }
                //filtro = "(" + filtro + ")";

                return filtro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReformataTelefone(string telefone)
        {
            try
            {
                telefone = telefone.Replace(" ", "");
                telefone = telefone.Replace("(", "");
                telefone = telefone.Replace(")", "");
                telefone = telefone.Replace("-", "");

                telefone = telefone.Length <= 10 ? long.Parse(telefone).ToString(@"(00) 0000-0000") : long.Parse(telefone).ToString(@"(00) 00000-0000");

                return telefone;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveAcentos(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static bool soContemNumeros(string texto)
        {
            try
            {
                var valor = Convert.ToInt32(texto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool soContemNumerosDouble(string texto)
        {
            try
            {
                var valor = Convert.ToDouble(texto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Tuple<int, int, int> CalculateAge(DateTime birthDate)
        {
            DateTime anotherDate = DateTime.Now;

            int years = anotherDate.Year - birthDate.Year;
            int months = 0;
            int days = 0;

            // Check if the last year, was a full year.
            if (anotherDate < birthDate.AddYears(years) && years != 0)
            {
                years--;
            }

            // Calculate the number of months.
            birthDate = birthDate.AddYears(years);

            if (birthDate.Year == anotherDate.Year)
            {
                months = anotherDate.Month - birthDate.Month;
            }
            else
            {
                months = (12 - birthDate.Month) + anotherDate.Month;
            }

            // Check if last month was a complete month.
            if (anotherDate < birthDate.AddMonths(months) && months != 0)
            {
                months--;
            }

            // Calculate the number of days.
            birthDate = birthDate.AddMonths(months);

            days = (anotherDate - birthDate).Days;

            return new Tuple<int, int, int>(years, months, days);
        }

        public static Tuple<int, int, int> CalculaIdadeDataImunizacao(DateTime birthDate, DateTime dataImunizacao)
        {
            try
            {



                DateTime anotherDate = dataImunizacao;

                int years = anotherDate.Year - birthDate.Year;
                int months = 0;
                int days = 0;

                // Check if the last year, was a full year.
                if (anotherDate < birthDate.AddYears(years) && years != 0)
                {
                    years--;
                }

                // Calculate the number of months.
                birthDate = birthDate.AddYears(years);

                if (birthDate.Year == anotherDate.Year)
                {
                    months = anotherDate.Month - birthDate.Month;
                }
                else
                {
                    months = (12 - birthDate.Month) + anotherDate.Month;
                }

                // Check if last month was a complete month.
                if (anotherDate < birthDate.AddMonths(months) && months != 0)
                {
                    months--;
                }

                // Calculate the number of days.
                birthDate = birthDate.AddMonths(months);

                days = (anotherDate - birthDate).Days;

                return new Tuple<int, int, int>(years, months, days);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static double CalculaPorcentagem(double total, double valor)
        {
            try
            {
                double? resultado = (valor / total) * 100;
                return Convert.ToDouble(resultado?.ToString("N2"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Agendamento de pacientes
        public const string Consultado = "Consultado";
        public const string AConsultar = "Aguardando Consulta";
        public const string Internado = "Internado";
        public const string AInternar = "Aguardando Internamento";
        public const string TeveAlta = "Alta Curado";
        public const string TeveAltaPedido = "Alta a Pedido";
        public const string Encaminhado = "Encaminhado";
        public const string Obito = "Óbito";
        public const string TodasPendentes = "Todas Pendentes";
        public const string AguardandoConfirmacao = "Aguardando Confirmação";
        public const string AguardandoExame = "Aguardando Exame";
        public const string Cancelada = "Cancelada";
        public const string NaoCompareceu = "Não Compareceu";
        public const string NaoConfirmou = "Não Confirmou";
        public const string HorarioReservado = "Horario Reservado";

        public static bool DisponivelParaCancelar(string status)
        {
            bool retorno = false;
            switch (status)
            {
                case Consultado:
                    retorno = false;
                    break;
                case Internado:
                    retorno = false;
                    break;
                case AInternar:
                    retorno = false;
                    break;
                case TeveAlta:
                    retorno = false;
                    break;
                case TeveAltaPedido:
                    retorno = false;
                    break;
                case Encaminhado:
                    retorno = false;
                    break;
                case Obito:
                    retorno = false;
                    break;
                case NaoCompareceu:
                    retorno = false;
                    break;
                case NaoConfirmou:
                    retorno = false;
                    break;
                case Cancelada:
                    retorno = false;
                    break;
                case AguardandoExame:
                    retorno = false;
                    break;
                default:
                    retorno = true;
                    break;
            }
            return retorno;
        }
    }
}

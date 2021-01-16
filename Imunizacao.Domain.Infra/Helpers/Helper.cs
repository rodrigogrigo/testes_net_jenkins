using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Infra.Helpers
{
    public static class Helper
    {
        //remove caracters e reformata para o caso de ter sido gravado sem a formatação correta
        public static string ReformataTelefone(string telefone)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(telefone))
                {
                    telefone = telefone.Replace(" ", "");
                    telefone = telefone.Replace("(", "");
                    telefone = telefone.Replace(")", "");
                    telefone = telefone.Replace("-", "");

                    telefone = telefone.Length <= 10 ? long.Parse(telefone).ToString(@"(00) 0000-0000") : long.Parse(telefone).ToString(@"(00) 00000-0000");
                }
                else
                    telefone = string.Empty;

                return telefone;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //calcula a idade a partir da data de nascimento, retorna anos, meses e dias, respectivamente
        public static Tuple<int, int, int> CalculaIdade(DateTime birthDate)
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

    
    }
}

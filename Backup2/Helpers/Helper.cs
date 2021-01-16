using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Infra.Helpers
{
    public static class Helper
    {
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Infra
{
    public static class ConfigConnection
    {
        public static string GetConnectionMunicipio(string ibge)
        {
            string stringconnection = string.Empty;
            switch (ibge)
            {
                case "3203908":
                    stringconnection = "DefaultConnection";
                    break;
            }
            return stringconnection;
        }
    }
}

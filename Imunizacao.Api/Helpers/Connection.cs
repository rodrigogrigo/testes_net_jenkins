using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.Helpers
{
    public static class Connection
    {
        public static string GetConnection(string ibge)
        {
            string connect = string.Empty;
#if DEBUG
            connect = $"{ibge}TesteConnection";
#else
            connect = $"{ibge}Connection";
#endif
            return connect;
        }

        public static string GetConnectionFoto(string ibge)
        {
            string connect = string.Empty;
#if DEBUG
            connect = $"{ibge}TesteFotosConnection";
#else
            connect = $"{ibge}FotosConnection";
#endif
            return connect;
        }
    }
}

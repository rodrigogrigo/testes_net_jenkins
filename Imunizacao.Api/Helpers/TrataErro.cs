using RgCidadao.Api.ViewModels;
using RgCidadao.Api.ViewModels.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.Helpers
{
    public static class TrataErro
    {
        public static ResponseViewModel GetResponse(string msgerro, bool erro)
        {
            var response = new ResponseViewModel();
            response.erro = erro;
            response.message = msgerro;
            return response;
        }

        public static ResponseViewModel GetResponseCicloEdit(string msgerro, bool erro, DateTime? datainicial, DateTime? datafinal)
        {
            var response = new ResponseViewModel();
            response.erro = erro;
            response.message = msgerro;
            response.datainicial = datainicial;
            response.datafinal = datafinal;
            return response;
        }
    }
}

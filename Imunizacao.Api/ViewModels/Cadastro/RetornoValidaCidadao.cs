using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class RetornoValidaCidadao
    {
        public RetornoValidaCidadao()
        {
            cidadao = new List<Cidadao>();
        }
        public List<Cidadao> cidadao { get; set; }
        public bool permiteEnviar { get; set; }
    }
}

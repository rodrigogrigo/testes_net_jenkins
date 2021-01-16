using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Prontuario
{
    public class AgrupamentoExames
    {
        public int codigo_agrupamento { get; set; }
        public string nome_agrupamento { get; set; }
    }

    public class ExamesAgrupados
    {
        public string nome_agrupamento { get; set; }

        public List<Exame> exames { get; set; }

        public ExamesAgrupados()
        {
            this.exames = new List<Exame>();
        }
    }
}

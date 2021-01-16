using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class GestaoFamiliaViewModel
    {
        public GestaoFamiliaViewModel()
        {
            imoveis = new List<ImoveisEstabelecimento>();
        }

        public int? id { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string sigla_estado { get; set; }
        public int? tipo_imovel { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public List<ImoveisEstabelecimento> imoveis { get; set; }
    }

    public class ImoveisEstabelecimento
    {
        public ImoveisEstabelecimento()
        {
            visita = new VisitaEstabelecimento();
            familias = new List<FamiliaEstabelecimento>();
        }
        public int? id { get; set; }
        public string numero_logradouro { get; set; }
        public int? tipo_imovel { get; set; }
        public List<FamiliaEstabelecimento> familias { get; set; }
        public VisitaEstabelecimento visita { get; set; }
    }

    public class FamiliaEstabelecimento
    {
        public int? id { get; set; }
        public int? num_prontuario_familiar { get; set; }
        public string responsavel { get; set; }
        public int? id_responsavel { get; set; }
    }

    public class VisitaEstabelecimento
    {
        public int? id { get; set; }
        public string data { get; set; }
        public int desfecho { get; set; }
    }
}

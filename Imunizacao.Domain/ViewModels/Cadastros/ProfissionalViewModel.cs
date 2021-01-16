using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Cadastros
{
    public class ProfissionalViewModel
    {
        public ProfissionalViewModel()
        {
            cbos = new List<CBO>();
        }

        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public string csi_cns { get; set; }
        public string csi_cbo { get; set; }
        public string descricao_cbo { get; set; }
        public string cbo { get; set; }
        public int? csi_iduser { get; set; }
        public string csi_cpf { get; set; }
        public string csi_ativo { get; set; }

        public string equipe { get; set; }
        public int? id_equipe { get; set; }
        public int? id_lotacao { get; set; }

        public string csi_inativo_profissional { get; set; }

        public List<CBO> cbos { get; set; }
    }
}

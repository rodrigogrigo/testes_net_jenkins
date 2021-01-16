using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    [Serializable]
    public class CBO
    {
        public string codigo { get; set; }
        public string descricao { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public int? id_competencia { get; set; }
    }
}

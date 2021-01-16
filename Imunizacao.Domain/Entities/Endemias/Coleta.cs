using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class Coleta
    {
        public Coleta()
        {
            itens = new List<ColetaResultado>();
        }

        public int? id { get; set; }
        public string uuid_registro_mobile { get; set; }
        public int? id_visita { get; set; }
        public int? deposito { get; set; }
        public int? amostra { get; set; }
        public int? id_profissional { get; set; }
        public int? id_ciclo { get; set; }
        public DateTime? data_inicial { get; set; }
        public DateTime? data_final { get; set; }
        public int? qtde_larvas { get; set; }

        //usado em metodos especificos
        public int? qtde { get; set; }

        public List<ColetaResultado> itens { get; set; }
    }
}

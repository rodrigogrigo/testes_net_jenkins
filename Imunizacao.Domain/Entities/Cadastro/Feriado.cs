using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Feriado
    {
        public DateTime? csi_data { get; set; }
        public string csi_descricao { get; set; }
        public string csi_obs { get; set; }
        public DateTime? csi_datainc { get; set; }
        public string csi_nomusu { get; set; }
        public DateTime? csi_dataalt { get; set; }
    }
}

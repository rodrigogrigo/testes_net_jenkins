using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Cadastros
{
    public class CidadaoFamiliaViewModel
    {
        public int? id_individuo { get; set; }
        public string individuo { get; set; }
        public string sexo { get; set; }
        public int? idade { get; set; }
        public DateTime? data_nascimento { get; set; }
        public int? id_familia { get; set; }
        public int? id_domicilio { get; set; }
        public int? id_profissional { get; set; }
        public int? id_microarea { get; set; }
        public int? responsavel { get; set; }
        public int? num_prontuario_familiar { get; set; }
        public string usa_tablet { get; set; }
        public string nome_profissional { get; set; }
    }
}

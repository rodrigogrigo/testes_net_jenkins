﻿namespace Imunizacao.Domain.Entities
{
    public class Configuracao_Usuario
    {
        public int? id { get; set; }
        public int? id_usuario { get; set; }
        public int? id_ultima_unidade { get; set; }
        public int? qtde_registro_tabela { get; set; }
        public int? tipo_menu { get; set; }
    }
}

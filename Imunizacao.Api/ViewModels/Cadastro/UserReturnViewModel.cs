using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class UserReturnViewModel
    {
        public UserReturnViewModel()
        {
            user = new Seg_Usuario();
            chave_configuracao = new Configuracao_Usuario();
            versao = new Versionamento();
        }

        public Seg_Usuario user { get; set; }
        public string Token { get; set; }
        public Configuracao_Usuario chave_configuracao { get; set; }
        public Versionamento versao { get; set; }
    }

    public class Versionamento
    {
        public string versao { get; set; }
    }
}

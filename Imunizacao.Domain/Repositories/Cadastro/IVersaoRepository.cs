using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IVersaoRepository
    {
        void AtualizaBanco(string ibge, int? id_command);
        void AtualizaBancoFotos(string ibgemun,string ibgefotos, int? id_command);
    }
}

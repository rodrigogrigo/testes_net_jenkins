using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Seguranca
{
    public interface IPerfilCommand
    {
        //perfil de usuario
        string GetAllPerfis { get; }
        string InsertSegPerfilAcesso { get; }
        string UpdateSegPerfilAcesso { get; }
        string GetPerfilById { get; }
        string GetPerfilNewId { get; }
        string GetPerfilByDescricao { get; }
        string GetPerfilPagination { get; }
        string GetCountAll { get; }

        string GetModulosByPerfil { get; }

        string AtualizaPermissaoPerfil { get; }


    }
}

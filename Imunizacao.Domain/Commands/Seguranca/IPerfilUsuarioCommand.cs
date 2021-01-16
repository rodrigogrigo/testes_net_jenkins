using RgCidadao.Domain.Entities.Seguranca;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Seguranca
{
    public interface IPerfilUsuarioCommand
    {
        string InsertOrUpdate { get; }
        string GetNewId { get; }
        string Delete { get; }
        string GetByIdUsuario { get; }
        string GetPermissaoTelaModulo { get; }

        string GetPermissaoUsuarios { get; }
        string GetPermissaoUsuarioTipo1e2 { get; }

        string GetUsuarioPermissaoTipo1e2 { get; }
        string GetPermissaoModuloTela { get; }
    }
}

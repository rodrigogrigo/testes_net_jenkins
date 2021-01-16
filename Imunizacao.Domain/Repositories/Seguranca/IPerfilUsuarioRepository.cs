using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Seguranca
{
    public interface IPerfilUsuarioRepository
    {
        void InsertOrUpdate(string ibge, Seg_Perfil_Usuario model);
        int GetNewId(string ibge);
        void Delete(string ibge, int id);
        List<Seg_Perfil_Usuario> GetByIdUsuario(string ibge, int id_usuario);

        bool GetPermissaoByUserTelaModulo(string ibge, int usuario, int unidade, string modulo, string tela, string acao);
        List<SegPermissoesUsuarioViewModel> GetPermissaoUsuarios(string ibge, int usuario, int unidade);
        List<SegPermissoesUsuarioViewModel> GetPermissaoUsuarioTipo1e2(string ibge, int unidade);
        int? GetUsuarioPermissaoTipo1e2(string ibge, int id_usuario);
        int? GetPermissaoModuloTela(string ibge, int usuario, string unidade, string modulo, string tela);
    }
}

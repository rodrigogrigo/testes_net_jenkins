using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Seguranca
{
    public interface IPerfilRepository
    {
        //perfil de usuário
        List<Seg_Perfil_Acesso> GetAllPerfis(string ibge, string filtro);
        void InsertSegPerfilAcesso(string ibge, Seg_Perfil_Acesso model);
        void UpdateSegPerfilAcesso(string ibge, Seg_Perfil_Acesso model);
        Seg_Perfil_Acesso GetPerfilById(string ibge, int id);
        int? GetPerfilNewId(string ibge);
        Seg_Perfil_Acesso GetPerfilByDescricao(string ibge, string descricao);
        List<Seg_Perfil_Acesso> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);

        //modulo por perfil
        List<SegModuloViewModel> GetModulosByPerfil(string ibge, int id_perfil);
        void AtualizaPermissaoPerfil(string ibge, int permissao, int idpermissao);
    }
}

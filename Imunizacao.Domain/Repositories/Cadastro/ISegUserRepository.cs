using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface ISegUserRepository
    {
        Seg_Usuario GetSegUsuarioById(int ID, string ibge);
        Seg_Usuario GetLogin(string ibge, string login, string senha);
        Seg_Usuario GetTelefoneByUser(string ibge, string login);
        void AtualizarSenhaProvisoria(string ibge, int id, string senha);
        void Update(string ibge, int id, string telefone1, string telefone2, string email);
        void InsertUser(string ibge, Seg_Usuario model);
        void UpdatetUser(string ibge, Seg_Usuario model);
        int GetNewId(string ibge);
        int GetCountAll(string ibge, string filtro);
        List<Seg_Usuario> GetAllPagination(string ibge, int pagesize, int page, string filtro);

        //Configuração de usuário
        Configuracao_Usuario GetConfigUsuario(string ibge, int id);
        int GetNewIdConfiguration(string ibge);
        void InsertOrUpdateConfigUsuario(string ibge, Configuracao_Usuario model);
        List<dynamic> GetTestePerformance(string ibge);
        int GetPermissaoUser(string ibge, int id);
        int? GetTipoUsuarioById(string ibge, int id);


        byte[] GetCabecalhoPaisagem(string ibge, int unidade);
        byte[] GetCabecalhoRetrato(string ibge, int unidade);


    }
}

using Imunizacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface ISegUserRepository
    {
        Seg_Usuario GetSegUsuarioById(int ID, string ibge);
        Seg_Usuario GetLogin(string ibge, string login, string senha);
        Seg_Usuario GetTelefoneByUser(string ibge, string login);
        void AtualizarSenhaProvisoria(string ibge, int id, string senha);
        void Update(string ibge, int id, string telefone1, string telefone2, string email);

        //Configuração de usuário
        Configuracao_Usuario GetConfigUsuario(string ibge, int id);
        int GetNewIdConfiguration(string ibge);
        void InsertOrUpdateConfigUsuario(string ibge, Configuracao_Usuario model);

        List<dynamic> GetTestePerformance(string ibge);
    }
}

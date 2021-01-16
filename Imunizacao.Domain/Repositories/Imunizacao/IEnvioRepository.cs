using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IEnvioRepository
    {
        int? GetNewIdEnvio(string ibge);
        List<Envio> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
        Envio GetEnvioById(string ibge, int id);
        Envio InsertOrUpdate(string ibge, Envio model);
        void Delete(string ibge, int id);
        void UpdateStatusEnviado(string ibge, int id);

        List<EnvioItem> GetAllItensByPai(string ibge, int idpai);
        EnvioItem GetItemById(string ibge, int iditem);
        void DeleteItem(string ibge, int iditem);
        int ValidaEstoqueItensEnvio(string ibge, int id, int unidade);

        List<Envio> GetTranferenciaByUnidadeDestino(string ibge, int id);
    }
}

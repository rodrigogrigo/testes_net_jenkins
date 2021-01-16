using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IEstoqueRepository
    {
        List<UnidadeEstoqueViewModel> GetAllUnidadeWithEstoque(string ibge, int produto, int usuario);
        List<EstoqueProduto> GetEstoqueLoteByUnidadeAndProduto(string ibge, int unidade, int produto);
        List<AuditoriaEstoque> GetAuditoria(string ibge, int id_produto, DateTime? datainicial, DateTime? datafinal, string lote, int? unidade, int page, int pagesize, int? id_produtor);
        int GetCountAuditoria(string ibge, int id_produto, DateTime? datainicial, DateTime? datafinal, string lote, int? unidade,int? id_produtor);
    }
}

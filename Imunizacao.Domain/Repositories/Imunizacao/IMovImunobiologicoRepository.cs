using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IMovImunobiologicoRepository
    {
        List<MovimentoImunobiologico> GetMovimentoByUnidade(string ibge, int unidade, string filtro, int page, int pagesize);
        MovimentoImunobiologico GetMovimentoById(string ibge, int id);
        int GetNewId(string ibge);
        void Inserir(string ibge, MovimentoImunobiologico model);
        void Atualizar(string ibge, MovimentoImunobiologico model);
        void Excluir(string ibge, int id);
        void ExcluirMovimentoProdutoById(string ibge, int id, string tabela_origem);
        DateTime GetDataMovimentoProduto(string ibge, string tabela_origem, int id_entrada_produto_item);
        bool GetExisteMovimentoProdutoAposEntrada(string ibge, int id_produtor, int id_produto, string lote, DateTime DataEntrada);
        bool GetMovRetiradaEstoqueByLote(string ibge, string lote, DateTime? data);
        int GetMovimentoSaidaLote(string ibge, string lote, int id_produto, int id_produtor);

        List<MovimentoImunobiologico> GetMovimentoByLote(string ibge, string id_lote, DateTime? data);
        int GetCountAll(string ibge, string filtro, int unidade);

    }
}

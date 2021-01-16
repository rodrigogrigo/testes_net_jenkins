using RgCidadao.Domain.Commands;
using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class VacinaApresentacaoCommandText : IVacinaApresentacaoCommand
    {
        public string sqlGetAll = $@"SELECT ID, DESCRICAO, QUANTIDADE
                                     FROM PNI_APRESENTACAO
                                     @filtro
                                     ORDER BY QUANTIDADE ASC";

        string IVacinaApresentacaoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetById = $@"SELECT ID, DESCRICAO, QUANTIDADE
                                      FROM PNI_APRESENTACAO
                                      WHERE ID = @id";
        string IVacinaApresentacaoCommand.GetById { get => sqlGetById; }

        public string sqlGetId = $@"SELECT GEN_ID(GEN_PNI_APRESENTACAO_ID, 1) AS VLR FROM RDB$DATABASE";

        string IVacinaApresentacaoCommand.GetIdVacinaApresentacao { get => sqlGetId; }

        public string sqlInsertVacinaApresentacao = $@"INSERT INTO PNI_APRESENTACAO (ID, DESCRICAO, QUANTIDADE)
                                                       VALUES (@id, @descricao, @quantidade)";

        string IVacinaApresentacaoCommand.GetInsertVacinaApresentacao { get => sqlInsertVacinaApresentacao; }

        public string sqlAtualizaVacinaApresentacao = $@"UPDATE PNI_APRESENTACAO
                                                         SET DESCRICAO = @descricao,
                                                             QUANTIDADE = @quantidade
                                                         WHERE ID = @id";

        string IVacinaApresentacaoCommand.GetAtualizaVacinaApresentacao { get => sqlAtualizaVacinaApresentacao; }

        public string sqlExcluirVacinaApresentacao = $@" DELETE FROM PNI_APRESENTACAO
                                                         WHERE ID = @id";

        string IVacinaApresentacaoCommand.GetExcluirVacinaApresentacao { get => sqlExcluirVacinaApresentacao; }

    }
}

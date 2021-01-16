using RgCidadao.Domain.Commands.AtencaoBasica;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class EstabelecimentoSaudeCommandText : IEstabelecimentoSaudeCommand
    {
        public string sqlGetAll = $@"SELECT 
                ID, NOME_FANTASIA, CNPJ, CNES, COD_ESF_ADM, COD_TIPO_UNID, TELEFONE1, TELEFONE2,
                FAX, E_MAIL, NUMERO, COMPLEMENTO, PONTO_REF, EXCLUIDO, COMPLEXIDADE, ID_USUARIO,
                ID_LOGRADOURO
            FROM ESUS_ESTABELECIMENTO_SAUDE
            ORDER BY NOME_FANTASIA";

        string IEstabelecimentoSaudeCommand.GetAll { get => sqlGetAll; }

        public string sqlGetById = $@"SELECT 
                ID, NOME_FANTASIA, CNPJ, CNES, COD_ESF_ADM, COD_TIPO_UNID, TELEFONE1, TELEFONE2,
                FAX, E_MAIL, NUMERO, COMPLEMENTO, PONTO_REF, EXCLUIDO, COMPLEXIDADE, ID_USUARIO,
                ID_LOGRADOURO
            FROM ESUS_ESTABELECIMENTO_SAUDE
            WHERE ID = @id";

        string IEstabelecimentoSaudeCommand.GetById { get => sqlGetById; }
    }
}

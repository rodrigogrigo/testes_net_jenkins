using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class UnidadeCommandText : IUnidadeCommand
    {
        public string sqlGetAll = $@"SELECT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
                                                      UN.CSI_ENDUNI ENDERECO, UN.CSI_BAIUNI BAIRRO, UN.FLG_UNIDADE_PA UNIDADE_PA, ES.ID ID_ESTABELECIMENTO_SAUDE
                                     FROM TSI_UNIDADE UN
                                     LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.CNES = UN.CSI_CNES   
                                     @filtro
                                     ORDER BY UN.CSI_NOMUNI";
        string IUnidadeCommand.GetAll { get => sqlGetAll; }

        public string sqlGetUnidadeByUser = $@"SELECT DISTINCT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
                                                      UN.CSI_ENDUNI ENDERECO, UN.CSI_BAIUNI BAIRRO, UN.FLG_UNIDADE_PA UNIDADE_PA, ES.ID ID_ESTABELECIMENTO_SAUDE
                                               FROM TSI_UNIDADE UN 
                                               INNER JOIN SEG_PERFIL_USUARIO UU ON (UU.ID_UNIDADE = UN.CSI_CODUNI)
                                               LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.CNES = UN.CSI_CNES
                                               WHERE ((UN.EXCLUIDO = 'F') OR (UN.EXCLUIDO IS NULL))
                                                    AND UU.ID_USUARIO = @user
                                               ORDER BY UN.CSI_NOMUNI";

        string IUnidadeCommand.GetUnidadesByUser { get => sqlGetUnidadeByUser; }

        //SELECT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
        //       UN.CSI_ENDUNI ENDERECO, UN.CSI_BAIUNI BAIRRO, UN.FLG_UNIDADE_PA UNIDADE_PA, UU.CSI_ATIVO ATIVO
        //FROM TSI_UNIDADE UN
        //INNER JOIN TSI_USERUNIDADE UU ON (UU.CSI_CODUNI = UN.CSI_CODUNI)
        //WHERE((UN.EXCLUIDO = 'F') OR(UN.EXCLUIDO IS NULL)) AND
        //   UU.CSI_ATIVO = 'True'
        //     AND UU.CSI_IDUSER = @user
        //ORDER BY UN.CSI_NOMUNI

        public string sqlGetLocaisAtendimentoByUnidade = $@"SELECT P.CSI_CODPONTO CODIGO, P.CSI_NOMPONTO DESCRICAO
                                                            FROM TSI_PONTOS P
                                                            WHERE P.CSI_CODUNI = @cod_uni";
        string IUnidadeCommand.GetLocaisAtendimentoByUnidade { get => sqlGetLocaisAtendimentoByUnidade; }
    }
}

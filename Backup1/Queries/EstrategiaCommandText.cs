using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class EstrategiaCommandText : IEstrategiaCommand
    {
        public string sqlEstrategiaByProduto = $@"SELECT DISTINCT E.ID, E.DESCRICAO
                                                  FROM PNI_PRODUTO P
                                                  JOIN PNI_IMUNOBIOLOGICO IMB ON (P.ID_IMUNOBIOLOGICO = IMB.ID)
                                                  JOIN PNI_REGRA_VACINAL RV ON (IMB.ID = RV.ID_IMUNOBIOLOGICO)
                                                  JOIN PNI_ESTRATEGIA E ON (RV.ID_ESTRATEGIA = E.ID)
                                                  JOIN PNI_DOSE D ON (RV.ID_DOSE = D.ID)
                                                  WHERE P.ID = @id";
        string IEstrategiaCommand.GetEstrategiaByProduto { get => sqlEstrategiaByProduto; }

        public string sqlGetAll = $@"SELECT *
                                     FROM PNI_ESTRATEGIA
                                     ORDER BY DESCRICAO";
        string IEstrategiaCommand.GetAll { get => sqlGetAll; }
        
    }
}

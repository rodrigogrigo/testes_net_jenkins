using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class DoseCommandText : IDoseCommand
    {
        public string sqlGetAll = $@"SELECT * FROM PNI_DOSE";
        string IDoseCommand.GetAll { get => sqlGetAll; }

        public string sqlGetDoseByEstrategiaAndProduto = $@"SELECT DISTINCT D.ID, D.DESCRICAO, D.SIGLA
                                                            FROM PNI_PRODUTO P
                                                            JOIN PNI_IMUNOBIOLOGICO IMB ON (P.ID_IMUNOBIOLOGICO = IMB.ID)
                                                            JOIN PNI_REGRA_VACINAL RV ON (IMB.ID = RV.ID_IMUNOBIOLOGICO)
                                                            JOIN PNI_ESTRATEGIA E ON (RV.ID_ESTRATEGIA = E.ID)
                                                            JOIN PNI_DOSE D ON (RV.ID_DOSE = D.ID)
                                                            WHERE P.ID = @id_imunobiologico AND 
                                                                  E.ID = @id_estrategia";
        string IDoseCommand.GetDoseByEstrategiaAndProduto { get => sqlGetDoseByEstrategiaAndProduto; }
        
            
    }
}

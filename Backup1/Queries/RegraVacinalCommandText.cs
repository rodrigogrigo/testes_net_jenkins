﻿using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class RegraVacinalCommandText : IRegraVacinalCommand
    {
        public string sqlGetRegraVacinalByParams = $@"SELECT *
                                                      FROM PNI_REGRA_VACINAL RV
                                                      WHERE RV.ID_IMUNOBIOLOGICO = @id_imunobiologico,
                                                            RV.ID_ESTRATEGIA = @id_estrategia,
                                                            RV.ID_DOSE = @id_dose";
        string IRegraVacinalCommand.GetRegraVacinalByParams { get => sqlGetRegraVacinalByParams; }
    }
}

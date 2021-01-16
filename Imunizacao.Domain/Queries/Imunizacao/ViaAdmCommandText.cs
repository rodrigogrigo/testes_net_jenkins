using RgCidadao.Domain.Commands;
using RgCidadao.Domain.Commands.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class ViaAdmCommandText : IViaAdmCommand
    {
        public string sqlGetAllViaAdm = $@"SELECT * FROM PNI_VIA_ADM 
                                            @filtro";
        string IViaAdmCommand.GetAllViaAdm { get => sqlGetAllViaAdm; }

        public string sqlGetLocalAplicacaoByViaAdm = $@"SELECT LA.*
                                                        FROM PNI_LOCAL_VIA_ADM LVA
                                                        JOIN PNI_LOCAL_APLICACAO LA ON LVA.ID_LOCAL = LA.ID
                                                        WHERE LVA.ID_VIA_ADM = @id_via_adm";
        string IViaAdmCommand.GetLocalAplicacaoByViaAdm { get => sqlGetLocalAplicacaoByViaAdm; }
    }
}

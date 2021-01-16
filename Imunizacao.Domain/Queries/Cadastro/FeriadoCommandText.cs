using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class FeriadoCommandText : IFeriadoCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) *
                                               FROM TSI_FERIADOS  
                                               @filtro
                                               ORDER BY CSI_DATA";
        string IFeriadoCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetAll = $@"SELECT *
                                     FROM TSI_FERIADOS  
                                     ORDER BY CSI_DATA";
        string IFeriadoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM TSI_FERIADOS  
                                          @filtro";
        string IFeriadoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetFeriadoById = $@"SELECT * FROM TSI_FERIADOS
                                             WHERE CSI_DATA = @data";
        string IFeriadoCommand.GetFeriadoById { get => sqlGetFeriadoById; }

        public string sqlInsert = $@"INSERT INTO TSI_FERIADOS (CSI_DATA, CSI_DESCRICAO, CSI_OBS, CSI_DATAINC, CSI_NOMUSU)
                                     VALUES (@csi_data,@csi_descricao, @csi_obs, @csi_datainc, @csi_nomusu)";
        string IFeriadoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE TSI_FERIADOS
                                     SET CSI_DESCRICAO = @csi_descricao,
                                         CSI_OBS = @csi_obs,
                                         CSI_NOMUSU = @csi_nomusu,
                                         CSI_DATAALT = @csi_dataalt
                                     WHERE CSI_DATA = @csi_data";
        string IFeriadoCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE TSI_FERIADOS
                                     WHERE CSI_DATA = @csi_data";
        string IFeriadoCommand.Delete { get => sqlDelete; }
    }
}

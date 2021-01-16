using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class EspecimeCommandText : IEspecimeCommand
    {
        public string sqlInsert = $@"INSERT INTO VA_ESPECIME (ID, ESPECIME, TIPO_ESPECIME)
                                     VALUES (@id, @especime, @tipo_especime)";
        string IEspecimeCommand.Insert { get => sqlInsert; }

        public string sqlGetEspecimeById = $@"SELECT * FROM VA_ESPECIME
                                              WHERE ID = @id";
        string IEspecimeCommand.GetEspecimeById { get => sqlGetEspecimeById; }

        public string sqlUpdate = $@"UPDATE VA_ESPECIME
                                     SET ESPECIME = @especime,
                                         TIPO_ESPECIME = @tipo_especime
                                     WHERE ID = @id";
        string IEspecimeCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM VA_ESPECIME
                                     WHERE ID = @id";
        string IEspecimeCommand.Delete { get => sqlDelete; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) *
                                               FROM VA_ESPECIME 
                                               @filtro";
        string IEspecimeCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM VA_ESPECIME 
                                          @filtro";
        string IEspecimeCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetEspecimeNewId = $@"SELECT GEN_ID(GEN_VA_ESPECIME, 1) AS VLR FROM RDB$DATABASE";
        string IEspecimeCommand.GetEspecimeNewId { get => sqlGetEspecimeNewId; }

        public string sqlGetAllEspecime = $@"SELECT * FROM VA_ESPECIME";
        string IEspecimeCommand.GetAllEspecime { get => sqlGetAllEspecime; }
    }
}

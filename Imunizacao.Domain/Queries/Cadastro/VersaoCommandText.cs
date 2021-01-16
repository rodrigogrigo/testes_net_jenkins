using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class VersaoCommandText : IVersaoCommand
    {
        public string sqlGetUltimoCodigoScript = $@"SELECT VERSAO FROM CONTROLE_SCRIPTS_WEB";
        string IVersaoCommand.GetUltimoCodigoScript { get => sqlGetUltimoCodigoScript; }

        public string sqlUpdateCodigoScript = $@"UPDATE CONTROLE_SCRIPTS_WEB SET VERSAO = @versao";
        string IVersaoCommand.UpdateCodigoScript { get => sqlUpdateCodigoScript; }

        public string sqlVerificaExisteRegistroTabelaVersao = $@"SELECT COUNT(*) FROM CONTROLE_SCRIPTS_WEB";
        string IVersaoCommand.VerificaExisteRegistroTabelaVersao { get => sqlVerificaExisteRegistroTabelaVersao; }

        public string sqlInserePrimeiroRegTabVersao = $@"INSERT INTO CONTROLE_SCRIPTS_WEB (VERSAO)
                                                         VALUES (0)";
        string IVersaoCommand.InserePrimeiroRegTabVersao { get => sqlInserePrimeiroRegTabVersao; }

        public string sqlCriaTabelaVersao = $@"CREATE TABLE CONTROLE_SCRIPTS_WEB (
                                                VERSAO INTEGER NOT NULL);";
        string IVersaoCommand.CriaTabelaVersao { get => sqlCriaTabelaVersao; }

        public string sqlCriaContraintTabelaVersao = $@"ALTER TABLE CONTROLE_SCRIPTS_WEB
                                                        ADD CONSTRAINT PK_CONTROLE_SCRIPTS_WEB
                                                        PRIMARY KEY (VERSAO);";
        string IVersaoCommand.CriaContraintTabelaVersao { get => sqlCriaContraintTabelaVersao; }

        public string sqlExisteTabelaControleVersao = $@"SELECT COUNT(*) QTDE
                                                         FROM RDB$RELATIONS
                                                         WHERE RDB$FLAGS=1 AND RDB$RELATION_NAME= 'CONTROLE_SCRIPTS_WEB'";
        string IVersaoCommand.VerificaTabelaVersaoExiste { get => sqlExisteTabelaControleVersao; }
    }
}

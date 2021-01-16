using Dapper;
using Microsoft.Extensions.Configuration;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Infra.Shared;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class VersaoRepository : IVersaoRepository
    {
        private IVersaoCommand _command;
        public VersaoRepository(IVersaoCommand command)
        {
            _command = command;
        }

        public void AtualizaBanco(string ibge, int? id_command)
        {
            try
            {
                //verifica se tabela de ver~soa existe
                var existetabelaversao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                              conn.QueryFirstOrDefault<int>(_command.VerificaTabelaVersaoExiste));
                if (existetabelaversao == 0)
                {
                    //cria tabela de versão
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.Execute(_command.CriaTabelaVersao));

                    //cria constraint de tabela de versão
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                         conn.Execute(_command.CriaContraintTabelaVersao));
                }

                //verifica se ja existe algum registro na tabela
                var existeregistrotabversao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                              conn.QueryFirstOrDefault<int>(_command.VerificaExisteRegistroTabelaVersao));

                //caso não exista, cria o primeiro = 0
                if (existeregistrotabversao == 0)
                {
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.InserePrimeiroRegTabVersao));
                }


                int ultimoComando = 0;
                if (id_command != null)
                    ultimoComando = (int)id_command - 1; //usa o numero da versão passada via parametro
                else
                    ultimoComando = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.QueryFirstOrDefault<int>(_command.GetUltimoCodigoScript)); //recupera ultimo comando

                var proximos = ScriptRepository.GetScript(ultimoComando);
                foreach (var item in proximos)
                {
                    try
                    {
                        Helpers.HelperConnection.ExecuteCommandBloco(ibge, item.script);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao Executar script de nº {item.codigo}. {ex.Message}");
                    }

                    ultimoComando = (int)item.codigo;
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.Execute(_command.UpdateCodigoScript, new { @versao = ultimoComando }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaBancoFotos(string ibgemun, string ibgefotos, int? id_command)
        {
            try
            {
                int ultimoComando = 0;
                if (id_command != null)
                    ultimoComando = (int)id_command - 1; //usa o numero da versão passada via parametro
                else
                    ultimoComando = Helpers.HelperConnection.ExecuteCommand(ibgemun, conn =>
                                          conn.QueryFirstOrDefault<int>(_command.GetUltimoCodigoScript)); //recupera ultimo comando

                var proximos = ScriptRepository.GetScriptBancoFotos(ultimoComando);
                foreach (var item in proximos)
                {
                    try
                    {
                        Helpers.HelperConnection.ExecuteCommand(ibgefotos, conn =>
                                                   conn.Execute(item.script));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao Executar script de nº {item.codigo}. {ex.Message}");
                    }

                    ultimoComando = (int)item.codigo;
                    string scriptAtualizaVersao = _command.UpdateCodigoScript.Replace("@versao", ultimoComando.ToString());
                    Helpers.HelperConnection.ExecuteCommandBloco(ibgemun, scriptAtualizaVersao);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

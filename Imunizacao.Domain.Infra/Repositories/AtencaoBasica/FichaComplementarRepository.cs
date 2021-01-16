using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class FichaComplementarRepository : IFichaComplementarRepository
    {
        private IFichaComplementarCommand _command;

        public FichaComplementarRepository(IFichaComplementarCommand command)
        {
            _command = command;
        }

        public List<FichaComplementarViewModel> GetAllPagination(string ibge, string filtro, int page, int pagesize, int usuario)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand<List<FichaComplementarViewModel>>(ibge, conn =>
                           conn.Query<FichaComplementarViewModel>(sql, new
                           {
                               @pagesize = pagesize,
                               @page = page,
                               @user = usuario
                           }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetCountAll.Replace("@filtros", filtro);
                else
                    sql = _command.GetCountAll.Replace("@filtros", string.Empty);

                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(sql));

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FichaComplementarViewModel GetFichaComplementarById(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<FichaComplementarViewModel>(_command.GetFichaComplementarById, new { @id = id }));
                
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, FichaComplementarViewModel model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.Insert, new
                                     {
                                         @id = model.id,
                                         @id_profissional = model.id_profissional,
                                         @id_unidade = model.id_unidade,
                                         @cns_profissional = model.cns_profissional,
                                         @cbo_profissional = model.cbo_profissional,
                                         @cnes = model.cnes,
                                         @ine_unidade = model.ine_unidade,
                                         @data = model.data,
                                         @turno = model.turno,
                                         @cns_cidadao = model.cns_cidadao,
                                         @cns_responsavel_familiar = model.cns_responsavel_familiar,
                                         @flg_teste_olhinho = model.flg_teste_olhinho,
                                         @data_teste_olhinho = model.data_teste_olhinho,
                                         @flg_exame_fundo_olho = model.flg_exame_fundo_olho,
                                         @data_exame_fundo_olho = model.data_exame_fundo_olho,
                                         @data_teste_orelhinha_peate = model.data_teste_orelhinha_peate,
                                         @flg_teste_orelhinha_peate = model.flg_teste_orelhinha_peate,
                                         @flg_us_transfontanela = model.flg_us_transfontanela,
                                         @data_us_transfontanela = model.data_us_transfontanela,
                                         @flg_tomografia = model.flg_tomografia,
                                         @data_tomografia = model.data_tomografia,
                                         @flg_ressonancia = model.flg_ressonancia,
                                         @data_ressonancia = model.data_ressonancia,
                                         //@uuid = model.uuid,
                                         //@id_esus_exportacao_item = model.id_esus_exportacao_item,
                                         @id_usuario = model.id_usuario,
                                         @id_equipe = model.id_equipe,
                                         //@id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote
                                     }));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, FichaComplementarViewModel model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Update, new
                                    {
                                        @id = model.id,
                                        @id_profissional = model.id_profissional,
                                        @id_unidade = model.id_unidade,
                                        @cns_profissional = model.cns_profissional,
                                        @cbo_profissional = model.cbo_profissional,
                                        @cnes = model.cnes,
                                        @ine_unidade = model.ine_unidade,
                                        @data = model.data,
                                        @turno = model.turno,
                                        @cns_cidadao = model.cns_cidadao,
                                        @cns_responsavel_familiar = model.cns_responsavel_familiar,
                                        @flg_teste_olhinho = model.flg_teste_olhinho,
                                        @data_teste_olhinho = model.data_teste_olhinho,
                                        @flg_exame_fundo_olho = model.flg_exame_fundo_olho,
                                        @data_exame_fundo_olho = model.data_exame_fundo_olho,
                                        @data_teste_orelhinha_peate = model.data_teste_orelhinha_peate,
                                        @flg_teste_orelhinha_peate = model.flg_teste_orelhinha_peate,
                                        @flg_us_transfontanela = model.flg_us_transfontanela,
                                        @data_us_transfontanela = model.data_us_transfontanela,
                                        @flg_tomografia = model.flg_tomografia,
                                        @data_tomografia = model.data_tomografia,
                                        @flg_ressonancia = model.flg_ressonancia,
                                        @data_ressonancia = model.data_ressonancia,
                                        //@uuid = model.uuid,
                                        //@id_esus_exportacao_item = model.id_esus_exportacao_item,
                                        @id_usuario = model.id_usuario,
                                        @id_equipe = model.id_equipe,
                                        //@id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote
                                    }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Delete, new
                                    {
                                        @id = id
                                    }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

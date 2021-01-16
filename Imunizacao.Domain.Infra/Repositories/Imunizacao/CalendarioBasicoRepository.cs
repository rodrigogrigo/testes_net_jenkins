using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class CalendarioBasicoRepository : ICalendarioBasicoRepository
    {
        public ICalendarioBasicoCommand _calendarioCommand;
        public CalendarioBasicoRepository(ICalendarioBasicoCommand _command)
        {
            _calendarioCommand = _command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Execute(_calendarioCommand.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalendarioBasico> GetAllPagination(string ibge, int page, int pagesize, string filtro, int? publico_alvo)
        {
            try
            {
                var lista = new List<CalendarioBasico>();
                if (publico_alvo != null)
                    filtro += $@" AND CB.PUBLICO_ALVO = {publico_alvo}";

                string command = string.Empty;
                if (string.IsNullOrWhiteSpace(filtro))
                    command = _calendarioCommand.GetAll.Replace("@filtro", "");
                else
                    command = _calendarioCommand.GetAll.Replace("@filtro", filtro);

                lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Query<CalendarioBasico>(command, new
                 {
                     @pagesize = pagesize,
                     @page = page
                 }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CalendarioBasico GetById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.QueryFirstOrDefault<CalendarioBasico>(_calendarioCommand.GetById, new { @id = id }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalendarioBasico> GetCalendarioByProduto(string ibge, int idproduto)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<CalendarioBasico>(_calendarioCommand.GetCalendarioBasicoByProduto, new { @id_produto = idproduto }).ToList());
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro, int? publico_alvo)
        {
            try
            {
                int item = 0;

                if (publico_alvo != null)
                    filtro += $@" AND CB.PUBLICO_ALVO = {publico_alvo}";

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<int>(_calendarioCommand.GetCountAll.Replace("@filtro", "")));
                }
                else
                {
                    item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_calendarioCommand.GetCountAll.Replace("@filtro", filtro)));
                }

                return item;
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
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_calendarioCommand.GetNewId));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, CalendarioBasico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_calendarioCommand.Insert, new
                           {
                               @id = model.id,
                               @id_faixa_etaria = model.id_faixa_etaria,
                               @id_produto = model.id_produto,
                               @id_dose = model.id_dose,
                               @publico_alvo = model.publico_alvo,
                               @dias_antes_aprazamento = model.dias_antes_aprazamento,
                               @faixa_etaria = model.faixa_etaria,
                               @produto = model.produto,
                               @dose = model.dose,
                               @idade_minima = model.idade_minima,
                               @idade_maxima = model.idade_maxima,
                               @flg_excluir_aprazamento = model.flg_excluir_aprazamento,
                               @vigencia_inicio = model.vigencia_inicio,
                               @vigencia_fim = model.vigencia_fim,
                               @id_estrategia = model.id_estrategia
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, CalendarioBasico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_calendarioCommand.Update, new
                          {
                              @id_faixa_etaria = model.id_faixa_etaria,
                              @id_produto = model.id_produto,
                              @id_dose = model.id_dose,
                              @publico_alvo = model.publico_alvo,
                              @dias_antes_aprazamento = model.dias_antes_aprazamento,
                              @faixa_etaria = model.faixa_etaria,
                              @produto = model.produto,
                              @dose = model.dose,
                              @id = model.id,
                              @idade_minima = model.idade_minima,
                              @idade_maxima = model.idade_maxima,
                              @flg_excluir_aprazamento = model.flg_excluir_aprazamento,
                              @vigencia_inicio = model.vigencia_inicio,
                              @vigencia_fim = model.vigencia_fim,
                              @id_estrategia = model.id_estrategia
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateInativo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_calendarioCommand.UpdateInativo, new
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

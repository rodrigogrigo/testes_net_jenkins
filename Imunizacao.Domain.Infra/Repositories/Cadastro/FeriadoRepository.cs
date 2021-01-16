using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class FeriadoRepository : IFeriadoRepository
    {
        public IFeriadoCommand _command;
        public FeriadoRepository(IFeriadoCommand command)
        {
            _command = command;
        }

        public void Atualizar(string ibge, Feriado model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.Update, new
                           {
                               @csi_descricao = model.csi_descricao,
                               @csi_obs = model.csi_obs,
                               @csi_nomusu = model.csi_nomusu,
                               @csi_dataalt = DateTime.Now,
                               @csi_data = model.csi_data
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deletar(string ibge, DateTime? data)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Delete, new
                        {
                            @csi_data = data
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Feriado> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.Query<Feriado>(_command.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Feriado> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var lista = new List<Feriado>();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Query<Feriado>(_command.GetAllPagination.Replace("@filtro", filtro), new
                                  {
                                      @pagesize = pagesize,
                                      @page = page,

                                  })).ToList();
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Query<Feriado>(_command.GetAllPagination.Replace("@filtro", string.Empty), new
                                  {
                                      @pagesize = pagesize,
                                      @page = page
                                  })).ToList();
                }

                return lista;
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
                int count = 0;
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", filtro)));
                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", string.Empty)));
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Feriado GetFeriadoById(string ibge, DateTime? data)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<Feriado>(_command.GetFeriadoById, new
                                {
                                    @data = data
                                }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inserir(string ibge, Feriado model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.Insert, new
                          {
                              @csi_descricao = model.csi_descricao,
                              @csi_obs = model.csi_obs,
                              @csi_nomusu = model.csi_nomusu,
                              @csi_datainc = DateTime.Now,
                              @csi_data = model.csi_data
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

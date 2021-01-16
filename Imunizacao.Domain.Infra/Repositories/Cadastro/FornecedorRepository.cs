using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IFornecedorCommand _fornecedorcommand;

        public FornecedorRepository(IFornecedorCommand commandText)
        {
            _fornecedorcommand = commandText;
        }

        public List<Fornecedor> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                var forn = new List<Fornecedor>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fornecedor>(_fornecedorcommand.GetAllPagination.Replace("@filtro", ""), new { @pagesize = pagesize, @page = page }).ToList());
                }
                else
                {
                    forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fornecedor>(_fornecedorcommand.GetAllPagination.Replace("@filtro", $" {filtro} "), new { @pagesize = pagesize, @page = page }).ToList());
                }

                forn = forn.Select(x => new Fornecedor
                {
                    csi_codfor = x.csi_codfor,
                    csi_nomfor = x.csi_nomfor,
                    csi_telfor = Helpers.Helper.ReformataTelefone(x.csi_telfor),
                    csi_tipfor = x.csi_tipfor
                }).ToList();

                return forn;
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
                int forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_fornecedorcommand.GetNewId));

                return forn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(FornecedorViewModel model, string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_fornecedorcommand.Insert, new
                        {
                            @csi_codfor = model.csi_codfor,
                            @csi_nomfan = model.csi_nomfan,
                            @csi_pessoa = model.csi_pessoa,
                            @csi_datcad = model.csi_datcad,
                            @csi_endfor = model.csi_endfor,
                            @csi_baifor = model.csi_baifor,
                            @csi_cepfor = model.csi_cepfor,
                            @csi_telfor = model.csi_telfor,
                            @csi_cgcfor = model.csi_cgcfor,
                            @csi_insfor = model.csi_insfor,
                            @csi_nomusu = model.csi_nomusu,
                            @csi_nomfor = model.csi_nomfor,
                            @csi_fabricante = model.csi_fabricante,
                            @csi_datainc = model.csi_datainc,
                            @excluido = "F",
                            @csi_tipfor = model.csi_tipfor,
                            @num_cnes = model.num_cnes,
                            @csi_emafor = model.csi_emafor
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(FornecedorViewModel model, string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_fornecedorcommand.Update, new
                          {
                              @csi_nomfan = model.csi_nomfan,
                              @csi_pessoa = model.csi_pessoa,
                              @csi_endfor = model.csi_endfor,
                              @csi_baifor = model.csi_baifor,
                              @csi_cepfor = model.csi_cepfor,
                              @csi_telfor = model.csi_telfor,
                              @csi_cgcfor = model.csi_cgcfor,
                              @csi_insfor = model.csi_insfor,
                              @csi_nomusu = model.csi_nomusu,
                              @csi_nomfor = model.csi_nomfor,
                              @csi_fabricante = model.csi_fabricante,
                              @csi_dataalt = model.csi_dataalt,
                              @csi_tipfor = model.csi_tipfor,
                              @num_cnes = model.num_cnes,
                              @csi_emafor = model.csi_emafor,
                              @csi_codfor = model.csi_codfor
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(DateTime csi_dataexc, int csi_codfor, string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_fornecedorcommand.Delete, new
                          {
                              @csi_dataexc = DateTime.Now.ToString("yyyy/MM/dd"),
                              @csi_codfor = csi_codfor
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fornecedor GetById(string ibge, int id)
        {
            try
            {
                var forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<Fornecedor>(_fornecedorcommand.GetById, new { @id = id }));

                return forn;
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
                int forn = 0;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_fornecedorcommand.GetCountAll.Replace("@filtro", "")));

                }
                else
                {
                    forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<int>(_fornecedorcommand.GetCountAll.Replace("@filtro", filtro)));
                }

                return forn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Fornecedor> GetAll(string ibge)
        {
            try
            {
                var forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fornecedor>(_fornecedorcommand.GetAll).ToList());

                return forn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteCPFCNPJ(string ibge, string cpfcnpj)
        {
            try
            {
                //valida repetição de cpf ou cnpj
                var existe = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<dynamic>(_fornecedorcommand.ValidaExistenciaFornecedorCNPJ, new
                               {
                                   @cpfcnpj = cpfcnpj
                               })).ToList();
                if (existe.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrestadorAgendaViewModel> GetPrestadoresVigencia(string ibge, int codmed, int coduni, string data_ini, string data_fim)
        {
            try
            {
                var forn = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<PrestadorAgendaViewModel>(_fornecedorcommand.GetPrestadoresVigencia, new
                         {
                             @cod_uni = coduni,
                             @data_ini = data_ini,
                             @data_fim = data_fim,
                             @cod_med = codmed
                         }).ToList());

                return forn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class ProcedimentoAvulsoController : ControllerBase
    {
        public IProcedimentoAvulsoRepository _repository;
        private IConfiguration _config;
        public ProcedimentoAvulsoController(IProcedimentoAvulsoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProcedimentoAvulso>> GetAllPagination([FromHeader]string ibge, int page,
                                                          int pagesize, string search, string fields, 
                                                          int? csi_coduni, DateTime? csi_data, DateTime? csi_dataini, int? usuario)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if ( !string.IsNullOrWhiteSpace(fields) && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltro(fields, search);
                    else
                    {
                        var filtronumero = string.Empty;
                        if (Helper.soContemNumeros(search))
                            filtronumero = $@" PE.CSI_CONTROLE = {search} OR
                                               PE.CSI_CODMED = {search} OR";

                        filtro += $@" AND ( {filtronumero}
                                              P.CSI_NOMPAC CONTAINING '{search}' OR
                                              P.CSI_NOMUSU CONTAINING '{search}' OR
                                              UNI.CSI_NOMUNI CONTAINING '{search}')";
                    }
                }

                if (csi_data != null)
                    filtro += $@" AND CAST(PE.CSI_DATA AS DATE) = '{csi_data?.ToString("dd.MM.yyyy")}'";
                //else if (csi_data != null && string.IsNullOrWhiteSpace(filtro))
                //    filtro += $@" CAST(PE.CSI_DATA AS DATE) = '{csi_data?.ToString("dd.MM.yyyy")}'";

                if (csi_dataini != null)
                    filtro += $@" AND CAST(PE.CSI_DATAINC AS DATE) = '{csi_dataini?.ToString("dd.MM.yyyy")}'";
                //else if (csi_data != null && string.IsNullOrWhiteSpace(filtro))
                //    filtro += $@" CAST(PE.CSI_DATAINC AS DATE) = '{csi_dataini?.ToString("dd.MM.yyyy")}'";

                if (csi_coduni != null)
                    filtro += $@" AND PE.CSI_CODUNI = {csi_coduni}";
                //else if (csi_coduni != null && string.IsNullOrWhiteSpace(filtro))
                //    filtro += $@" PE.CSI_CODUNI = {csi_coduni}";

                //if (!string.IsNullOrWhiteSpace(filtro))
                //    filtro = " WHERE " + filtro;

                int count = _repository.GetCountAll(ibge, filtro, (int)usuario);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ProcedimentoAvulso> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize, (int)usuario);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcEnfermagemById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ProcEnfermagem> GetProcEnfermagemById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                ProcEnfermagem itens = _repository.GetProcEnfermagemById(ibge, id);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] ProcEnfermagem model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_controle = _repository.GetNewId(ibge);
                _repository.Insert(ibge, model);

                foreach (var item in model.itens)
                {
                    item.csi_controle = model.csi_controle;
                    item.id_sequencial = _repository.GetNewIdItem(ibge);
                    _repository.InsertItem(ibge, item);
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{csi_controle}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] ProcEnfermagem model, [FromRoute] int csi_controle)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_controle = csi_controle;
                _repository.Update(ibge, model);

                foreach (var item in model.itens)
                {
                    item.csi_controle = model.csi_controle;
                    if (item.id_sequencial == null)
                        item.id_sequencial = _repository.GetNewIdItem(ibge);
                    _repository.InsertItem(ibge, item);
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.Delete(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("ExcluirItem/{id_controle:int}/{id_codproc:int}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult ExcluirItem([FromHeader] string ibge, int id_controle, int id_codproc)//[FromRoute] ParamProcAvusloViewModel model
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.DeleteItem(ibge, id_controle, id_codproc);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcedimentosConsolidados")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProcedimentosConsolidadosViewModel>> GetProcedimentosConsolidados([FromHeader] string ibge, string cod_cbo, int? idade, string sexo, int? codpac)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (codpac != null)
                    filtro = $@" AND {idade} BETWEEN P.IDADE_MIN AND P.IDADE_MAX
                                 AND P.SEXO IN ('{sexo}','I','N')";
                else
                    filtro = $@" AND PR.COD_PADRAO_REGISTRO_BPA = '01'";

                List<ProcedimentosConsolidadosViewModel> itens = _repository.GetProcedimentosConsolidados(ibge, cod_cbo, filtro);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcedimentosIndividualizado")]
        [ParameterTypeFilter("visualizar")] //(vAno*12) + Trunc(vMes)
        public ActionResult<List<ProcedimentosConsolidadosViewModel>> GetProcedimentosIndividualizado([FromHeader] string ibge, string cod_cbo, int? ano, int? mes, string sexo, string codpac)

        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtroSIGTAP = string.Empty;
                string filtroESUS = string.Empty;

                if (!string.IsNullOrWhiteSpace(codpac))
                {
                    filtroSIGTAP = $@" AND (({(ano * 12) + mes} BETWEEN P.IDADE_MIN AND P.IDADE_MAX) OR ((P.IDADE_MIN = 9999) AND (P.IDADE_MAX = 9999)))
                                       AND P.SEXO IN ('{sexo}','I','N')";
                    filtroESUS = $@" AND (({(ano * 12) + mes} BETWEEN P.IDADE_MIN AND P.IDADE_MAX) OR ((P.IDADE_MIN = 9999) AND (P.IDADE_MAX = 9999)))
                                     AND P.SEXO IN ('{sexo}','I','N')";
                }
                else
                {
                    filtroSIGTAP = $@" AND PR.COD_PADRAO_REGISTRO_BPA = '01'";
                    filtroESUS = $@" AND PR.COD_PADRAO_REGISTRO_BPA = '01'";
                }

                List<ProcedimentosConsolidadosViewModel> itens = _repository.GetProcedimentosIndividualizado(ibge, cod_cbo, filtroSIGTAP, filtroESUS);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
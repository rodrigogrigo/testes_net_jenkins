using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Endemias;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Repositories.Endemias;
using Rotativa.AspNetCore.Options;

namespace RgCidadao.Api.Areas.Endemias.Controllers
{
    [Route("api/Endemias/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Endemias")]
    public class ReportController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IReportEndemiasRepository _repository;
        private readonly ICicloRepository _ciclorepository;
        private readonly ISegUserRepository _segrepository;
        private IServiceProvider _service;
        public ReportController(IConfiguration configuration, IReportEndemiasRepository repository, IServiceProvider service,
                                                                        ISegUserRepository segrepository, ICicloRepository ciclo)
        {
            _config = configuration;
            _repository = repository;
            _service = service;
            _segrepository = segrepository;
            _ciclorepository = ciclo;
        }

        #region Levantamento de Índice de Infestação Predial 
        [HttpPost("ReportLevantamentoPredial")]
        public async Task<byte[]> ReportLevantamentoPredial([FromHeader]string ibge, [FromBody]ReportParamsLevPredialViewModel report)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var relatorio = new ReportEndemiasViewModel();
                relatorio.ibge = ibge;
                relatorio.unidade = (int)report.unidadelogadaParam;

                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                if (report.dataInicial != null)
                {
                    filtro += $@" AND VI.DATA_HORA_ENTRADA >= '{report.dataInicial?.ToString("dd.MM.yyyy")}'";
                    filtrotexto += $@"Entre {report.dataInicial?.ToString("dd/MM/yyyy")} ";
                }

                if (report.dataFinal != null)
                {
                    filtro += $@" AND VI.DATA_HORA_SAIDA <= '{report.dataFinal?.ToString("dd.MM.yyyy")}'";
                    filtrotexto += $@"e {report.dataFinal?.ToString("dd/MM/yyyy")} .";
                }

                if (!string.IsNullOrWhiteSpace(report.bairro))
                    filtro += $@" AND B.CSI_CODBAI IN({report.bairro})";

                var itens = _repository.GetInfestacaoPredialReport(ibge, filtro);
                relatorio.itens.AddRange(itens);
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoPaisagem(ibge, (int)report.unidadelogadaParam);

                //if (itens.Count == 0)
                //    return BadRequest(TrataErro.GetResponse("Não foram encontrados registros!", true));
                //return BadRequest(TrataErro.GetResponse("Existem doses desse Lote que já foram retiradas do Estoque. Não é possível editar o Lote.", true));

                var model = GeraReportLevantamentoPredial(relatorio, (int)report.tipo);
                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportLevantamentoPredial(ReportEndemiasViewModel model, int tipo)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            string caminhobd = string.Empty;
            if (tipo == 0)
                caminhobd = "Report/Endemias/ReportLevPredialAnalitico";
            else
                caminhobd = "Report/Endemias/ReportLevPredialSintetico";

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf(caminhobd, model)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportLevantamentoPredial", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);
            return await bytePDF;
        }
        #endregion

        #region Antivetorial visitas
        [HttpPost("ReportAntivetorialAnalitico")]
        public async Task<byte[]> ReportAntivetorialAnalitico([FromHeader]string ibge, [FromBody]ReportParamsLevPredialViewModel report)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var relatorio = new ReportEndemiasViewModel();
                relatorio.ibge = ibge;
                relatorio.unidade = (int)report.unidadelogadaParam;
                relatorio.datainicial = report.dataInicial;
                relatorio.datafinal = report.dataFinal;
                relatorio.ciclo = _ciclorepository.ValidaExistenciaCicloPeriodo(ibge, report.dataInicial, report.dataFinal).Take(1).FirstOrDefault()?.num_ciclo;

                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                if (report.dataInicial == null)
                    throw new Exception("A data inicial deve ser informada!");

                if (report.dataFinal == null)
                    throw new Exception("A data final deve ser informada!");

                filtrotexto += $@"Período: {report.dataInicial?.ToString("dd/MM/yyyy")} a {report.dataFinal?.ToString("dd/MM/yy")}";
                filtro += $@"WHERE CAST(VI.DATA_HORA_ENTRADA AS DATE) BETWEEN '{report.dataInicial?.ToString("dd.MM.yyyy")}' AND '{report.dataFinal?.ToString("dd.MM.yyyy")}'";

                //if (report.tipo == 0)
                filtrotexto += $@", Tipo de Relatório: Analítico;";
                //else
                //    filtrotexto += $@", Tipo de Relatório: Totalizador;";

                if (!string.IsNullOrWhiteSpace(report.profissionais))
                {
                    filtrotexto += $@", Profissionais: {report.nomeProfissional}";
                    filtro += $@"  AND VI.ID_PROFISSIONAL IN ({report.profissionais})";
                }
                else
                    filtrotexto += $@", Profissionais: TODOS";

                if (!string.IsNullOrWhiteSpace(report.nomeBairro))
                {
                    filtrotexto += $@", Bairro: {report.nomeBairro}";
                    filtro += $@" AND B.CSI_CODBAI = {report.bairro}";
                }

                var itens = _repository.GetAntivetorialAnatico(ibge, filtro);
                relatorio.AntivetorialAnalitico.AddRange(itens);
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoPaisagem(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportAntivetorialAnalitico(relatorio);
                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportAntivetorialAnalitico(ReportEndemiasViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            string caminhobd = "Report/Endemias/ReportAntivetorialAnalitico";

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf(caminhobd, model)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportAntivetorial", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);
            return await bytePDF;
        }

        [HttpPost("ReportAntivetorialTotalizador")]
        public async Task<byte[]> ReportAntivetorialTotalizador([FromHeader]string ibge, [FromBody]ReportParamsLevPredialViewModel report)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var relatorio = new ReportEndemiasViewModel();
                relatorio.ibge = ibge;
                relatorio.unidade = (int)report.unidadelogadaParam;

                string filtrotexto = string.Empty;
                string filtrodata = string.Empty;
                string filtrobairro = string.Empty;
                string filtroprofissional = string.Empty;

                if (report.dataInicial == null)
                    throw new Exception("A data inicial deve ser informada!");

                if (report.dataFinal == null)
                    throw new Exception("A data final deve ser informada!");

                filtrotexto += $@"Período: {report.dataInicial?.ToString("dd/MM/yyyy")} a {report.dataFinal?.ToString("dd/MM/yy")}";
                filtrodata += $@" CAST(VI.DATA_HORA_ENTRADA AS DATE) BETWEEN '{report.dataInicial?.ToString("dd.MM.yyyy")}' AND '{report.dataFinal?.ToString("dd.MM.yyyy")}' ";

                if (!string.IsNullOrWhiteSpace(report.bairro))
                {
                    filtrotexto += $@", Bairro: {report.nomeBairro}";
                    filtrobairro += $@" AND B.CSI_CODBAI = {report.bairro} ";
                }

                if (!string.IsNullOrWhiteSpace(report.profissionais))
                {
                    //filtrotexto += $@", Profissionais: {report.nomeProfissional}";
                    filtroprofissional += $@" AND VI.ID_PROFISSIONAL IN ({report.profissionais}) ";
                }
                else
                    filtrotexto += $@", Profissionais: TODOS";

                //preencher aqui a view model de relatorio com as informações consultadas

                var antivetorialTotalizador = _repository.GetAntivetorialTotalizador(ibge, filtrodata + filtrobairro, filtrodata + filtrobairro + filtroprofissional);
                var antivetorialTrabalhoCampoTotais = _repository.GetAntivetorialTrabalhoCampoTotais(ibge, filtrodata + filtrobairro + filtroprofissional);
                var antivetorialResumoLab = _repository.GetAntivetorialResumoLab(ibge, filtrodata + filtrobairro + filtroprofissional, filtrodata + filtrobairro + filtroprofissional);
                var antivetorialInfectados = _repository.GetAntivetorialInfectados(ibge, filtrodata + filtrobairro + filtroprofissional);

                relatorio.AntivetorialTotalizador = antivetorialTotalizador;
                relatorio.AntivetorialCamposTotais = antivetorialTrabalhoCampoTotais;
                relatorio.AntivetorialResumoLab.AddRange(antivetorialResumoLab);
                relatorio.AntivetorialInfectados.AddRange(antivetorialInfectados);

                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoPaisagem(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportAntivetorialTotalizador(relatorio);
                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportAntivetorialTotalizador(ReportEndemiasViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            string caminhobd = "Report/Endemias/ReportAntivetorialTotalizador";

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf(caminhobd, model)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportAntivetorialTotalizador", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);
            return await bytePDF;
        }

        #endregion
    }
}
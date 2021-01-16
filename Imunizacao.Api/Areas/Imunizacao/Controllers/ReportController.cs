using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Api.ViewModels.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using Rotativa.AspNetCore.Options;

namespace RgCidadao.Api.Areas.Imunizacao.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class ReportController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IReportRepository _repository;
        private readonly ISegUserRepository _segrepository;
        private readonly ICidadaoRepository _cidadaoRepository;
        private IServiceProvider _service;
        public ReportController(
            IConfiguration configuration,
            IReportRepository repository,
            IServiceProvider service,
            ISegUserRepository seguserrepository,
            ICidadaoRepository cidadaoRepository
        )
        {
            _config = configuration;
            _repository = repository;
            _service = service;
            _segrepository = seguserrepository;
            _cidadaoRepository = cidadaoRepository;
        }

        #region Report Imunobiologico
        [HttpPost("ReportImunobiologico")]
        [Route("/api/Imunizacao/ReportImunobiologico")]
        //[ParameterTypeFilter("visualizar")]
        public async Task<byte[]> ReportImunobiologico([FromHeader]string ibge, [FromBody]ReportImunobiologicoViewModel report)
        {
            try
            {
                var relatorio = new ReportImunizacaoViewModel();
                relatorio.ibge = ibge;
                relatorio.unidade = (int)report.unidadelogadaParam;

                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                filtrotexto = $@"Período: {Convert.ToDateTime(report.datainicio).ToString("dd/MM/yyyy")} a {Convert.ToDateTime(report.datafim).ToString("dd/MM/yyyy")} ;";

                if (report.id_paciente != null && report.id_paciente != 0)
                {
                    filtro += $" AND V.ID_PACIENTE = {report.id_paciente}";
                    filtrotexto += $" Paciente: {report.nome_paciente} ;";
                }

                if (report.count_unidades == 1)
                    filtrotexto += $" Unidade: {report.nom_unidade}; ";
                else if (report.count_unidades > 1)
                    filtrotexto += $" Várias Unidades Selecionadas; ";

                if (report.count_profissional == 1)
                    filtrotexto += $"Profissional: {report.nom_profissional};";
                else if (report.count_profissional > 1)
                    filtrotexto += $" Vários Profissionais Selecionados; ";

                if (report.count_produto == 1)
                    filtrotexto += $" Produto: {report.nom_produto}; ";
                else if (report.count_produto > 1)
                    filtrotexto += $" Vários Produtos Selecionados; ";

                if (report.count_estrategias == 1)
                    filtrotexto += $" Estratégia: {report.nom_estrategia}; ";
                else if (report.count_estrategias > 1)
                    filtrotexto += $" Várias Estratégias Selecionados; ";

                if (!string.IsNullOrWhiteSpace(report.unidade))
                    filtro += $" AND V.ID_UNIDADE IN ({report.unidade})";

                if (!string.IsNullOrWhiteSpace(report.profissional))
                    filtro += $" AND V.ID_PROFISIONAL IN ({report.profissional})";

                if (!string.IsNullOrWhiteSpace(report.produto))
                    filtro += $" AND V.ID_PRODUTO IN ({report.produto})";

                if (report.gp_atendimento != null && report.gp_atendimento != 0)
                    filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {report.gp_atendimento}";

                if (!string.IsNullOrWhiteSpace(report.estrategia))
                    filtro += $" AND V.ID_ESTRATEGIA IN ({report.estrategia})";

                if (report.agrupamento == 3)
                    filtro += $@"  ) T1
                                  GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
                                  ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
                else if (report.agrupamento == 0)
                    filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
                else if (report.agrupamento == 1)
                    filtro += $@" ORDER BY PACIENTE, DATA DESC ";
                else if (report.agrupamento == 2)
                    filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE ";

                var info = _repository.GetReportImunizacao(ibge, Convert.ToDateTime(report.datainicio), Convert.ToDateTime(report.datafim), filtro, string.Empty);

                relatorio.itens.AddRange(info);
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoRetrato(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportImunobiologico(relatorio);
                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportImunobiologico(ReportImunizacaoViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportImunobiologico", model)
            {
                PageOrientation = Orientation.Portrait,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportImunobiologico", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);
            return await bytePDF;
        }
        #endregion

        #region Report Paciente
        [HttpPost("ReportPaciente")]
        [Route("/api/Imunizacao/ReportPaciente")]
        //[ParameterTypeFilter("visualizar")]
        public async Task<byte[]> ReportPaciente([FromHeader]string ibge, [FromBody]ReportImunobiologicoViewModel report)
        {
            try
            {
                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                filtrotexto = $@" Período: {Convert.ToDateTime(report.datainicio).ToString("dd/MM/yyyy")} a {Convert.ToDateTime(report.datafim).ToString("dd/MM/yyyy")} ;";

                if (report.id_paciente != null && report.id_paciente != 0)
                {
                    filtro += $" AND V.ID_PACIENTE = {report.id_paciente}";
                    filtrotexto += $" Paciente: {report.nome_paciente} ;";
                }

                if (report.count_unidades == 1)
                    filtrotexto += $" Unidade: {report.nom_unidade}; ";
                else if (report.count_unidades > 1)
                    filtrotexto += $" Várias Unidades Selecionadas; ";

                if (report.count_profissional == 1)
                    filtrotexto += $"Profissional: {report.nom_profissional};";
                else if (report.count_profissional > 1)
                    filtrotexto += $" Vários Profissionais Selecionados; ";

                if (report.count_produto == 1)
                    filtrotexto += $" Produto: {report.nom_produto}; ";
                else if (report.count_produto > 1)
                    filtrotexto += $" Vários Produtos Selecionados; ";

                if (report.count_estrategias == 1)
                    filtrotexto += $" Estratégia: {report.nom_estrategia}; ";
                else if (report.count_estrategias > 1)
                    filtrotexto += $" Várias Estratégias Selecionados; ";

                if (!string.IsNullOrWhiteSpace(report.unidade))
                    filtro += $" AND V.ID_UNIDADE IN ({report.unidade})";

                if (!string.IsNullOrWhiteSpace(report.profissional))
                    filtro += $" AND V.ID_PROFISIONAL IN ({report.profissional})";

                if (!string.IsNullOrWhiteSpace(report.produto))
                    filtro += $" AND V.ID_PRODUTO IN ({report.produto})";

                if (report.gp_atendimento != null && report.gp_atendimento != 0)
                    filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {report.gp_atendimento}";

                if (!string.IsNullOrWhiteSpace(report.estrategia))
                    filtro += $" AND V.ID_ESTRATEGIA IN ({report.estrategia})";

                if (report.agrupamento == 3)
                    filtro += $@"  ) T1
                                  GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
                                  ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
                else if (report.agrupamento == 0)
                    filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
                else if (report.agrupamento == 1)
                    filtro += $@" ORDER BY PACIENTE, DATA DESC ";
                else if (report.agrupamento == 2)
                    filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE ";

                var info = _repository.GetReportImunizacao(ibge, Convert.ToDateTime(report.datainicio), Convert.ToDateTime(report.datafim), filtro, string.Empty);

                var relatorio = new ReportImunizacaoViewModel();
                relatorio.itens.AddRange(info);
                relatorio.ibge = ibge;
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoRetrato(ibge, (int)report.unidadelogadaParam);
                var model = GeraReportPaciente(relatorio);

                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportPaciente(ReportImunizacaoViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportPaciente", model)
            {
                PageOrientation = Orientation.Portrait,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportPaciente", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);

            return await bytePDF;
        }
        #endregion

        #region Report Unidade
        [HttpPost("ReportUnidade")]
        //[ParameterTypeFilter("visualizar")]
        public async Task<byte[]> ReportUnidade([FromHeader]string ibge, [FromBody]ReportImunobiologicoViewModel report)
        {
            try
            {
                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                filtrotexto = $@"Período: {Convert.ToDateTime(report.datainicio).ToString("dd/MM/yyyy")} a {Convert.ToDateTime(report.datafim).ToString("dd/MM/yyyy")} ;";

                if (report.id_paciente != null && report.id_paciente != 0)
                {
                    filtro += $" AND V.ID_PACIENTE = {report.id_paciente}";
                    filtrotexto += $" Paciente: {report.nome_paciente} ;";
                }

                if (report.count_unidades == 1)
                    filtrotexto += $" Unidade: {report.nom_unidade}; ";
                else if (report.count_unidades > 1)
                    filtrotexto += $" Várias Unidades Selecionadas; ";

                if (report.count_profissional == 1)
                    filtrotexto += $"Profissional: {report.nom_profissional};";
                else if (report.count_profissional > 1)
                    filtrotexto += $" Vários Profissionais Selecionados; ";

                if (report.count_produto == 1)
                    filtrotexto += $" Produto: {report.nom_produto}; ";
                else if (report.count_produto > 1)
                    filtrotexto += $" Vários Produtos Selecionados; ";

                if (report.count_estrategias == 1)
                    filtrotexto += $" Estratégia: {report.nom_estrategia}; ";
                else if (report.count_estrategias > 1)
                    filtrotexto += $" Várias Estratégias Selecionados; ";

                if (!string.IsNullOrWhiteSpace(report.unidade))
                    filtro += $" AND V.ID_UNIDADE IN ({report.unidade})";

                if (!string.IsNullOrWhiteSpace(report.profissional))
                    filtro += $" AND V.ID_PROFISIONAL IN ({report.profissional})";

                if (!string.IsNullOrWhiteSpace(report.produto))
                    filtro += $" AND V.ID_PRODUTO IN ({report.produto})";

                if (report.gp_atendimento != null && report.gp_atendimento != 0)
                    filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {report.gp_atendimento}";

                if (!string.IsNullOrWhiteSpace(report.estrategia))
                    filtro += $" AND V.ID_ESTRATEGIA IN ({report.estrategia})";

                if (report.agrupamento == 3)
                    filtro += $@"  ) T1
                                  GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
                                  ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
                else if (report.agrupamento == 0)
                    filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
                else if (report.agrupamento == 1)
                    filtro += $@" ORDER BY PACIENTE, DATA DESC ";
                else if (report.agrupamento == 2)
                    filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE";

                var info = _repository.GetReportImunizacao(ibge, Convert.ToDateTime(report.datainicio), Convert.ToDateTime(report.datafim), filtro, string.Empty);

                var relatorio = new ReportImunizacaoViewModel();
                relatorio.itens.AddRange(info);
                relatorio.ibge = ibge;
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoRetrato(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportUnidade(relatorio);

                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportUnidade(ReportImunizacaoViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportUnidade", model)
            {
                PageOrientation = Orientation.Portrait,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportUnidade", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);

            return await bytePDF;
        }
        #endregion

        #region Report Sintético
        [HttpPost("ReportSintetico")]
        //[ParameterTypeFilter("visualizar")]
        public async Task<byte[]> ReportSintetico([FromHeader]string ibge, [FromBody]ReportImunobiologicoViewModel report)
        {
            try
            {
                string filtrotexto = string.Empty;
                string filtroini = string.Empty;
                string filtro = string.Empty;

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                filtrotexto = $@"Período: {Convert.ToDateTime(report.datainicio).ToString("dd/MM/yyyy")} a {Convert.ToDateTime(report.datafim).ToString("dd/MM/yyyy")} ;";

                if (report.id_paciente != null && report.id_paciente != 0)
                {
                    filtro += $" AND V.ID_PACIENTE = {report.id_paciente}";
                    filtrotexto += $" Paciente: {report.nome_paciente} ;";
                }

                if (report.count_unidades == 1)
                    filtrotexto += $" Unidade: {report.nom_unidade}; ";
                else if (report.count_unidades > 1)
                    filtrotexto += $" Várias Unidades Selecionadas; ";

                if (report.count_profissional == 1)
                    filtrotexto += $"Profissional: {report.nom_profissional};";
                else if (report.count_profissional > 1)
                    filtrotexto += $" Vários Profissionais Selecionados; ";

                if (report.count_produto == 1)
                    filtrotexto += $" Produto: {report.nom_produto}; ";
                else if (report.count_produto > 1)
                    filtrotexto += $" Vários Produtos Selecionados; ";

                if (report.count_estrategias == 1)
                    filtrotexto += $" Estratégia: {report.nom_estrategia}; ";
                else if (report.count_estrategias > 1)
                    filtrotexto += $" Várias Estratégias Selecionados; ";

                if (!string.IsNullOrWhiteSpace(report.unidade))
                    filtro += $" AND V.ID_UNIDADE IN ({report.unidade})";

                if (!string.IsNullOrWhiteSpace(report.profissional))
                    filtro += $" AND V.ID_PROFISIONAL IN ({report.profissional})";

                if (!string.IsNullOrWhiteSpace(report.produto))
                    filtro += $" AND V.ID_PRODUTO IN ({report.produto})";

                if (report.gp_atendimento != null && report.gp_atendimento != 0)
                    filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {report.gp_atendimento}";

                if (!string.IsNullOrWhiteSpace(report.estrategia))
                    filtro += $" AND V.ID_ESTRATEGIA IN ({report.estrategia})";

                if (report.agrupamento == 3)
                {
                    filtroini = $@"SELECT T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE, CAST(COUNT(T1.ID) AS INTEGER) QTD FROM ( ";

                    filtro += $@"  )T1
                                  GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
                                  ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
                }
                else if (report.agrupamento == 0)
                    filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
                else if (report.agrupamento == 1)
                    filtro += $@" ORDER BY PACIENTE, DATA DESC ";
                else if (report.agrupamento == 2)
                    filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE";
                var info = _repository.GetReportImunizacao(ibge, Convert.ToDateTime(report.datainicio), Convert.ToDateTime(report.datafim), filtro, filtroini);

                var relatorio = new ReportImunizacaoViewModel();
                relatorio.itens.AddRange(info);
                relatorio.ibge = ibge;
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoRetrato(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportSintetico(relatorio);

                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportSintetico(ReportImunizacaoViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportSintetico", model)
            {
                PageOrientation = Orientation.Portrait,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportSintetico", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);

            return await bytePDF;
        }
        #endregion

        #region Movimento de doses
        [HttpPost("ReportMovimento")]
        [Route("/api/Imunizacao/ReportMovimento")]
        //[ParameterTypeFilter("visualizar")]
        public async Task<byte[]> ReportMovimento([FromHeader]string ibge, [FromBody]ReportImunobiologicoViewModel report)
        {
            try
            {
                var relatorio = new ReportImunizacaoViewModel();
                relatorio.ibge = ibge;
                relatorio.unidade = (int)report.unidadelogadaParam;

                string filtrotexto = string.Empty;
                string filtro = string.Empty;

                filtrotexto = $@"Período: {Convert.ToDateTime(report.datainicio).ToString("dd/MM/yyyy")} a {Convert.ToDateTime(report.datafim).ToString("dd/MM/yyyy")} ;";

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                var info = _repository.GetBoletimMovimentacao(ibge, report.unidade, report.produto, Convert.ToDateTime(report.datafim), Convert.ToDateTime(report.datainicio));
                var cabecalho = _segrepository.GetCabecalhoPaisagem(ibge, (int)report.unidadelogadaParam);

                relatorio.itensBoletim.AddRange(info);
                relatorio.impresso_por = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                relatorio.filtro = filtrotexto;
                relatorio.cabecalho = _segrepository.GetCabecalhoPaisagem(ibge, (int)report.unidadelogadaParam);

                var model = GeraReportMovimento(relatorio);

                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportMovimento(ReportImunizacaoViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportMovimento", model)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportMovimento", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);

            return await bytePDF;
        }
        #endregion

        #region Impressão de Cartão de Vacina
        [HttpPost("ReportCartaoVacina")]
        [Route("/api/Imunizacao/ReportCartaoVacina")]
        //[ParameterTypeFilter("imprimir_cartao_vacina")]
        public async Task<byte[]> ReportCartaoVacina([FromHeader]string ibge, [FromBody]ParametersVacina report)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                string sql_estrutura = string.Empty;
                if (_cidadaoRepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA FAM ON (PAC.ID_FAMILIA = FAM.ID)
                                       LEFT JOIN VS_ESTABELECIMENTOS D ON D.ID = FAM.ID_DOMICILIO";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON PAC.ID_ESUS_CADDOMICILIAR = D.ID";

                var info = _repository.GetCartaoVacinaByIndividuo(ibge, (int)report.id_cidadao, sql_estrutura);

                info.cabecalho = _segrepository.GetCabecalhoRetrato(ibge, (int)report.id_unidade);

                var model = GeraReportCartaoVacina(info);

                return await model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GeraReportCartaoVacina(CartaoVacinaReportViewModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _service };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var bytePDF = new Rotativa.AspNetCore.ViewAsPdf("Report/Imunobiologico/ReportCartaoVacina", model)
            {
                PageOrientation = Orientation.Portrait,
                PageMargins = { Top = 8, Right = 8, Bottom = 8, Left = 8 },
                FileName = string.Concat("ReportCartaoVacina", ".pdf"),
                CustomSwitches = "--footer-center \"| RG System - Tecnologia em Software! (27) 3150-9770 - www.rgsystem.com.br | Página [page] de [toPage] |\"" + " --footer-font-size \"9\" --footer-font-name \"calibri light\""
            }.BuildFile(actionContext);

            return await bytePDF;
        }
        #endregion
    }
}
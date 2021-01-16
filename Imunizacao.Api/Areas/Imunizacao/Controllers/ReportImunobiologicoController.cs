using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels;
using RgCidadao.Api.Helpers;
using System.Data;
using Newtonsoft.Json;
using Syncfusion.Reporting.Web.ReportViewer;
using Syncfusion.Reporting.Web;
using RgCidadao.Api.ViewModels.Imunizacao;

namespace RgCidadao.Api.Areas.Imunizacao.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public partial class ReportImunobiologicoController : Controller, IReportController
    {
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IReportRepository _repository;
        private IConfiguration _config;
        private Microsoft.Extensions.Primitives.StringValues authenticationHeader;

        public ReportImunobiologicoController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IConfiguration configuration, IReportRepository repository)//
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
            _config = configuration;
        }

        Dictionary<string, object> jsonArray = null;
        private ReportImunobiologicoViewModel report = new ReportImunobiologicoViewModel();

        [HttpGet("GetResource")]
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, this._cache);
        }

        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory())}";
            FileStream reportStream = new FileStream(basePath + @"\Imunizacao.Report\Reports\Imunizacao\ImunizacaoProdutoReport.rdlc", FileMode.Open, FileAccess.Read);
            reportOption.ReportModel.Stream = reportStream;
            reportOption.ReportModel.ProcessingMode = ProcessingMode.Local;
        }

        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            string filtro = string.Empty;
            string filtrotexto = string.Empty;

            //if (report.ibge != null)
            //{
            //    report.ibge = _config.GetConnectionString(Connection.GetConnection(report.ibge));

            //    filtrotexto = $@"Período: {report.datainicio} a {report.datafim} ;";

            //    if (report.id_paciente != null && report.id_paciente != 0)
            //    {
            //        filtro += $" AND V.ID_PACIENTE = {report.id_paciente}";
            //        filtrotexto += $" Paciente: {report.nome_paciente} ;";
            //    }

            //    if (report.count_unidades == 1)
            //        filtrotexto += $" Unidade: {report.nom_unidade}; ";
            //    else if (report.count_unidades > 1)
            //        filtrotexto += $" Várias Unidades Selecionadas; ";

            //    if (report.count_profissional == 1)
            //        filtrotexto += $"Profissional: {report.nom_profissional};";
            //    else if (report.count_profissional > 1)
            //        filtrotexto += $" Vários Profissionais Selecionados; ";

            //    if (report.count_produto == 1)
            //        filtrotexto += $" Produto: {report.nom_produto}; ";
            //    else if (report.count_produto > 1)
            //        filtrotexto += $" Vários Produtos Selecionados; ";

            //    if (report.gp_atendimento != null && report.gp_atendimento != 0)
            //        filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {report.gp_atendimento}";

            //    if (report.agrupamento == 3)
            //        filtro += $@"  ) T1
            //                      GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
            //                      ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
            //    else if (report.agrupamento == 0)
            //        filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
            //    else if (report.agrupamento == 1)
            //        filtro += $@" ORDER BY PACIENTE, DATA DESC ";
            //    else if (report.agrupamento == 2)
            //        filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE ";
            //    filtro = string.Empty;
            //    var info = _repository.GetReportImunizacao(report.ibge, report.unidade, report.profissional, report.produto, Convert.ToDateTime(report.datainicio), Convert.ToDateTime(report.datafim), filtro);
            //    var cabecalho = _repository.GetCabecalhoRetrato(report.ibge, (int)report.unidadelogadaParam);

            //    //Adds data sources to report model.
            //    reportOption.ReportModel.DataSources.Clear();

            //    //CABEÇALHO
            //    var dtCabecalho = new DataTable();
            //    dtCabecalho.Columns.Add(new DataColumn("filtro", typeof(string)));
            //    dtCabecalho.Columns.Add(new DataColumn("img_cabecalho", typeof(string)));
            //    dtCabecalho.Columns.Add(new DataColumn("impresso_por", typeof(string)));
            //    dtCabecalho.Columns.Add(new DataColumn("total_geral", typeof(int)));

            //    var row = dtCabecalho.NewRow();
            //    row["filtro"] = filtrotexto;
            //    row["img_cabecalho"] = cabecalho;
            //    row["impresso_por"] = $"Relatório emitido por {report.usuarioParam} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
            //    row["total_geral"] = info.Count();

            //    dtCabecalho.Rows.Add(row);

            //    //INFO produto
            //    var dtProd = new DataTable();
            //    dtProd.Columns.Add(new DataColumn("profissional", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("nome_paciente", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("imunobiologico", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("idade", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("id_paciente", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("dose", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("data_imunizacao", typeof(string)));
            //    dtProd.Columns.Add(new DataColumn("lote", typeof(string)));

            //    foreach (var item in info)
            //    {
            //        var rowpac = dtProd.NewRow();
            //        int idade = 0;
            //        if (item.data_nascimento != null)
            //            idade = Helpers.Helper.CalculateAge(Convert.ToDateTime(item.data_nascimento)).Item1;

            //        rowpac["profissional"] = item.profissional;
            //        rowpac["nome_paciente"] = $"Indivíduo: {item.paciente} - Idade:{idade}";
            //        rowpac["imunobiologico"] = $"{item.produto} ({item.sigla})";
            //        rowpac["idade"] = idade;
            //        rowpac["id_paciente"] = item.id_paciente;
            //        rowpac["dose"] = item.dose;
            //        rowpac["data_imunizacao"] = item.data?.ToString("dd/MM/yyyy");
            //        rowpac["lote"] = item.lote;

            //        dtProd.Rows.Add(rowpac);
            //    }

            //    reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "Imunobiologico", Value = dtCabecalho });
            //    reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "ImunizacaoImunobiologico", Value = dtProd });
            //}
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }

        [HttpPost("PostReportAction")]
        public object PostReportAction([FromBody]Dictionary<string, object> jsonResult)

        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out authenticationHeader);
            //Perform your custom validation here
            if (authenticationHeader == "")
            {
                return new Exception("Authentication failed!!!");
            }

            if (jsonResult.ContainsKey("parameters"))
            {
                dynamic retorno = JsonConvert.DeserializeObject(jsonResult["parameters"].ToString());

                foreach (var item in retorno)
                {
                    //if (item.ibge != null)
                    //    report.ibge = item.ibge;
                    if (item.unidade != null)
                        report.unidade = item.unidade;
                    if (item.nom_unidade != null)
                        report.nom_unidade = item.nom_unidade;
                    if (item.count_unidades != null)
                        report.count_unidades = item.count_unidades;
                    if (item.profissional != null)
                        report.profissional = item.profissional;
                    if (item.count_profissional != null)
                        report.count_profissional = item.count_profissional;
                    if (item.nom_profissional != null)
                        report.nom_profissional = item.nom_profissional;
                    if (item.produto != null)
                        report.produto = item.produto;
                    if (item.count_produto != null)
                        report.count_produto = item.count_produto;
                    if (item.nom_produto != null)
                        report.nom_produto = item.nom_produto;
                    if (item.datainicio != null)
                        report.datainicio = item.datainicio;
                    if (item.datafim != null)
                        report.datafim = item.datafim;

                    if (item.id_paciente != null)
                        report.id_paciente = item.id_paciente;
                    if (item.nome_paciente != null)
                        report.nome_paciente = item.nome_paciente;
                    if (item.gp_atendimento != null)
                        report.gp_atendimento = item.gp_atendimento;
                    if (item.agrupamento != null)
                        report.agrupamento = item.agrupamento;
                    if (item.unidadelogadaParam != null)
                        report.unidadelogadaParam = item.unidadelogadaParam;
                    if (item.usuarioParam != null)
                        report.usuarioParam = item.usuarioParam;
                }
            }

            return ReportHelper.ProcessReport(jsonResult, this, this._cache);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using Syncfusion.EJ.ReportViewer;
using Syncfusion.Report;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;

namespace RgCidadao.Api.Areas.Imunizacao.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class ReportSinteticoController : ControllerBase//, Syncfusion.EJ.ReportViewer.IReportController
    {
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private IReportRepository _repository;
        private IConfiguration _config;

        public string unidadeParam = null;
        public string ibgeParam = null;
        public string nom_unidadeParam = null;
        public int? count_unidadesParam = null;
        public string profissionalParam = null;
        public int? count_profissionalParam = null;
        public string nom_profissionalParam = null;
        public string produtoParam = null;
        public int? count_produtoParam = null;
        public string nom_produtoParam = null;
        public string datainicioParam = null;
        public string datafimParam = null;
        public int? id_pacienteParam = null;
        public string nome_pacienteParam = null;
        public int? gp_atendimentoParam = null;
        public int? agrupamentoParam = null;
        public int? unidadelogadaParam = null;
        public string usuarioParam = null;

        //public ReportSinteticoController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
        //    Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IReportRepository repository, IConfiguration configuration)
        //{
        //    _cache = memoryCache;
        //    _hostingEnvironment = hostingEnvironment;
        //    _repository = repository;
        //    _config = configuration;
        //}

        //[HttpPost("PostReportAction")]
        //public object PostReportAction(Dictionary<string, object> jsonResult)
        //{
        //    if (jsonResult.ContainsKey("ibge"))
        //        ibgeParam = jsonResult["ibge"].ToString();
        //    if (jsonResult.ContainsKey("unidade"))
        //        unidadeParam = jsonResult["unidade"].ToString();
        //    if (jsonResult.ContainsKey("nom_unidade"))
        //        nom_unidadeParam = jsonResult["nom_unidade"].ToString();
        //    if (jsonResult.ContainsKey("count_unidades"))
        //        count_unidadesParam = (int)jsonResult["count_unidades"];
        //    if (jsonResult.ContainsKey("profissional"))
        //        profissionalParam = jsonResult["profissional"].ToString();
        //    if (jsonResult.ContainsKey("count_profissional"))
        //        count_profissionalParam = (int)jsonResult["count_profissional"];
        //    if (jsonResult.ContainsKey("nom_profissional"))
        //        nom_profissionalParam = jsonResult["nom_profissional"].ToString();
        //    if (jsonResult.ContainsKey("produto"))
        //        produtoParam = jsonResult["produto"].ToString();
        //    if (jsonResult.ContainsKey("count_produto"))
        //        count_produtoParam = (int)jsonResult["count_produto"];
        //    if (jsonResult.ContainsKey("nom_produto"))
        //        nom_produtoParam = jsonResult["nom_produto"].ToString();
        //    if (jsonResult.ContainsKey("datainicio"))
        //        datainicioParam = jsonResult["datainicio"].ToString();
        //    if (jsonResult.ContainsKey("datafim"))
        //        datafimParam = jsonResult["datafim"].ToString();
        //    if (jsonResult.ContainsKey("id_paciente"))
        //        id_pacienteParam = (int)jsonResult["id_paciente"];
        //    if (jsonResult.ContainsKey("nome_paciente"))
        //        nome_pacienteParam = jsonResult["nome_paciente"].ToString();
        //    if (jsonResult.ContainsKey("gp_atendimento"))
        //        gp_atendimentoParam = (int)jsonResult["gp_atendimento"];
        //    if (jsonResult.ContainsKey("agrupamento"))
        //        agrupamentoParam = (int)jsonResult["agrupamento"];
        //    if (jsonResult.ContainsKey("unidadelogadaParam"))
        //        unidadelogadaParam = (int)jsonResult["unidadelogadaParam"];
        //    if (jsonResult.ContainsKey("usuarioParam"))
        //        usuarioParam = jsonResult["usuarioParam"].ToString();
        //    return Syncfusion.EJ.ReportViewer.ReportHelper.ProcessReport(jsonResult, this, this._cache);
        //}

        //public void OnInitReportOptions(ReportViewerOptions reportOption)
        //{
        //    string basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory())}";
        //    FileStream reportStream = new FileStream(basePath + @"\Imunizacao.Report\Reports\Imunizacao\ImunizacaoSinteticoReport.rdlc", FileMode.Open, FileAccess.Read);
        //    reportOption.ReportModel.Stream = reportStream;
        //    reportOption.ReportModel.ProcessingMode = Syncfusion.EJ.ReportViewer.ProcessingMode.Local;
        //}

        //public void OnReportLoaded(ReportViewerOptions reportOption)
        //{
        //    try
        //    {
        //        string filtro = string.Empty;
        //        string filtrotexto = string.Empty;

        //        string ibge = string.Empty;
        //        string unidade = string.Empty;
        //        string nom_unidade = string.Empty;
        //        int? count_unidades = 0;
        //        string profissional = string.Empty;
        //        int? count_profissional = 0;
        //        string nom_profissional = string.Empty;
        //        string produto = string.Empty;
        //        int? count_produto = 0;
        //        string nom_produto = string.Empty;
        //        string datainicio = string.Empty;
        //        string datafim = string.Empty;
        //        int? id_paciente = 0;
        //        string nome_paciente = string.Empty;
        //        int? gp_atendimento = 0;
        //        int? agrupamento = 0;
        //        int? unidade_logada = 0;
        //        string usuario = string.Empty;

        //        #region Recupera parâmetros

        //        if (!string.IsNullOrWhiteSpace(unidadeParam))
        //            unidade = JsonConvert.DeserializeObject<string>(unidadeParam);
        //        if (!string.IsNullOrWhiteSpace(ibgeParam))
        //            ibge = JsonConvert.DeserializeObject<string>(ibgeParam);
        //        if (!string.IsNullOrWhiteSpace(nom_unidadeParam))
        //            nome_paciente = JsonConvert.DeserializeObject<string>(nom_unidadeParam);
        //        if (count_unidadesParam != null)
        //            count_unidades = JsonConvert.DeserializeObject<int>(count_unidadesParam.ToString());
        //        if (!string.IsNullOrWhiteSpace(profissionalParam))
        //            profissional = JsonConvert.DeserializeObject<string>(profissionalParam);
        //        if (count_profissionalParam != null)
        //            count_profissional = JsonConvert.DeserializeObject<int>(count_profissionalParam.ToString());
        //        if (!string.IsNullOrWhiteSpace(nom_profissionalParam))
        //            nom_profissional = JsonConvert.DeserializeObject<string>(nom_profissionalParam);
        //        if (!string.IsNullOrWhiteSpace(produtoParam))
        //            produto = JsonConvert.DeserializeObject<string>(produtoParam);
        //        if (count_produtoParam != null)
        //            count_produto = JsonConvert.DeserializeObject<int>(count_produtoParam.ToString());
        //        if (!string.IsNullOrWhiteSpace(nom_produtoParam))
        //            nom_produto = JsonConvert.DeserializeObject<string>(nom_produtoParam);
        //        if (!string.IsNullOrWhiteSpace(datainicioParam))
        //            datainicio = JsonConvert.DeserializeObject<string>(datainicioParam);
        //        if (!string.IsNullOrWhiteSpace(datafimParam))
        //            datafim = JsonConvert.DeserializeObject<string>(datafimParam);
        //        if (id_pacienteParam != null)
        //            id_paciente = JsonConvert.DeserializeObject<int>(id_pacienteParam.ToString());
        //        if (!string.IsNullOrWhiteSpace(nome_pacienteParam))
        //            nome_paciente = JsonConvert.DeserializeObject<string>(nome_pacienteParam);
        //        if (gp_atendimentoParam != null)
        //            gp_atendimento = JsonConvert.DeserializeObject<int>(gp_atendimentoParam.ToString());
        //        if (agrupamentoParam != null)
        //            agrupamento = JsonConvert.DeserializeObject<int>(agrupamentoParam.ToString());
        //        if (unidadelogadaParam != null)
        //            unidade_logada = JsonConvert.DeserializeObject<int>(unidadelogadaParam.ToString());
        //        if (!string.IsNullOrWhiteSpace(usuarioParam))
        //            usuario = JsonConvert.DeserializeObject<string>(usuarioParam.ToString());
        //        #endregion

        //        ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

        //        filtrotexto = $@"Período: {datainicio} a {datafim} ;";

        //        if (id_paciente != null && id_paciente != 0)
        //        {
        //            filtro += $" AND V.ID_PACIENTE = {id_paciente}";
        //            filtrotexto += $" Paciente: {nome_paciente} ;";
        //        }

        //        if (count_unidades == 1)
        //            filtrotexto += $" Unidade: {nom_unidade}; ";
        //        else if (count_unidades > 1)
        //            filtrotexto += $" Várias Unidades Selecionadas; ";

        //        if (count_profissional == 1)
        //            filtrotexto += $"Profissional: {nom_profissional};";
        //        else if (count_profissional > 1)
        //            filtrotexto += $" Vários Profissionais Selecionados; ";

        //        if (count_produto == 1)
        //            filtrotexto += $" Produto: {nom_produto}; ";
        //        else if (count_produto > 1)
        //            filtrotexto += $" Vários Produtos Selecionados; ";

        //        if (gp_atendimento != null && gp_atendimento != 0)
        //            filtro += $@" AND V.ID_GRUPO_ATENDIMENTO = {gp_atendimento}";

        //        if (agrupamento == 3)
        //            filtro += $@"  ) T1
        //                          GROUP BY T1.ID_UNIDADE, T1.UNIDADE, T1.ID_PRODUTO, T1.PRODUTO, T1.ID_DOSE, T1.DOSE
        //                          ORDER BY T1.UNIDADE, T1.PRODUTO, T1.DOSE";
        //        else if (agrupamento == 0)
        //            filtro += $@" ORDER BY PRODUTO, DOSE, DATA DESC, PACIENTE";
        //        else if (agrupamento == 1)
        //            filtro += $@" RDER BY PACIENTE, DATA DESC ";
        //        else if (agrupamento == 2)
        //            filtro += $@" ORDER BY UNIDADE, PRODUTO, DOSE, DATA DESC, PACIENTE ";

        //        var info = _repository.GetReportImunizacao(ibge, unidade, profissional, produto, Convert.ToDateTime(datainicio), Convert.ToDateTime(datafim), filtro);
        //        var cabecalho = _repository.GetCabecalhoRetrato(ibge, (int)unidade_logada);

        //        var infogroup = info.Select(x => new
        //        {
        //            nomeunidade = x.unidade,
        //            produto = x.produto,
        //            dose = x.dose,
        //            quantidade = info.Where(p => p.dose == x.dose &&
        //                                       p.produto == x.produto &&
        //                                       p.unidade == x.unidade).Count()
        //        }).Distinct()
        //          .ToList();

        //        //Adds data sources to report model.
        //        reportOption.ReportModel.DataSources.Clear();

        //        //CABEÇALHO
        //        var dtCabecalho = new DataTable();
        //        dtCabecalho.Columns.Add(new DataColumn("filtro", typeof(string)));
        //        dtCabecalho.Columns.Add(new DataColumn("img_cabecalho", typeof(string)));
        //        dtCabecalho.Columns.Add(new DataColumn("impresso_por", typeof(string)));
        //        dtCabecalho.Columns.Add(new DataColumn("total_geral", typeof(int)));

        //        var row = dtCabecalho.NewRow();
        //        row["filtro"] = filtrotexto;
        //        row["img_cabecalho"] = cabecalho;
        //        row["impresso_por"] = $"Relatório emitido por {usuario} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        //        row["total_geral"] = infogroup.Sum(x => x.quantidade);

        //        dtCabecalho.Rows.Add(row);

        //        //INFO sintetico
        //        var dtProd = new DataTable();
        //        dtProd.Columns.Add(new DataColumn("nome_unidade", typeof(string)));
        //        dtProd.Columns.Add(new DataColumn("imunobiologico", typeof(string)));
        //        dtProd.Columns.Add(new DataColumn("dose", typeof(string)));
        //        dtProd.Columns.Add(new DataColumn("qtde", typeof(int)));

        //        foreach (var item in infogroup)
        //        {
        //            var rowsint = dtProd.NewRow();

        //            rowsint["nome_unidade"] = item.nomeunidade;
        //            rowsint["imunobiologico"] = item.produto;
        //            rowsint["dose"] = item.dose;
        //            rowsint["qtde"] = item.quantidade;

        //            dtProd.Rows.Add(rowsint);
        //        }

        //        reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "cabecalhoSintetico", Value = dtCabecalho });
        //        reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "ImunizacaoSintetico", Value = dtProd });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public object PostFormReportAction()
        //{
        //    return ReportHelper.ProcessReport(null, this, this._cache);
        //}

        //[HttpGet("GetResource")]
        //public object GetResource(ReportResource resource)
        //{
        //    try
        //    {
        //        return Syncfusion.EJ.ReportViewer.ReportHelper.GetResource(resource, this, _cache);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
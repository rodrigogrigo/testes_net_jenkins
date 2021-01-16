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
    public class ReportMovimentacaoController : ControllerBase//, Syncfusion.EJ.ReportViewer.IReportController
    {
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private IReportRepository _repository;
        private IConfiguration _config;

        public string ibgeParam = null;
        public string unidadeParam = null;
        public string produtoParam = null;
        public string datainicioParam = null;
        public string datafimParam = null;
        public int? unidadelogadaParam = null;
        public string usuarioParam = null;

        public ReportMovimentacaoController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
             Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IReportRepository repository, IConfiguration configuration)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
            _config = configuration;
        }

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

        //public void OnInitReportOptions(ReportViewerOptions reportOption)
        //{
        //    string basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory())}";
        //    FileStream reportStream = new FileStream(basePath + @"\Imunizacao.Report\Reports\Imunizacao\ImunizacaoMovimentoReport.rdlc", FileMode.Open, FileAccess.Read);
        //    reportOption.ReportModel.Stream = reportStream;
        //    reportOption.ReportModel.ProcessingMode = Syncfusion.EJ.ReportViewer.ProcessingMode.Local;
        //}

        //public void OnReportLoaded(ReportViewerOptions reportOption)
        //{
        //    string filtrotexto = string.Empty;
        //    string ibge = string.Empty;
        //    string unidade = string.Empty;
        //    string produto = string.Empty;
        //    string datainicio = string.Empty;
        //    string datafim = string.Empty;
        //    int? unidade_logada = 0;
        //    string usuario = string.Empty;

        //    #region Recupera parâmetros

        //    if (!string.IsNullOrWhiteSpace(unidadeParam))
        //        unidade = JsonConvert.DeserializeObject<string>(unidadeParam);
        //    if (!string.IsNullOrWhiteSpace(ibgeParam))
        //        ibge = JsonConvert.DeserializeObject<string>(ibgeParam);
        //    if (!string.IsNullOrWhiteSpace(produtoParam))
        //        produto = JsonConvert.DeserializeObject<string>(produtoParam);
        //    if (!string.IsNullOrWhiteSpace(datainicioParam))
        //        datainicio = JsonConvert.DeserializeObject<string>(datainicioParam);
        //    if (!string.IsNullOrWhiteSpace(datafimParam))
        //        datafim = JsonConvert.DeserializeObject<string>(datafimParam);
        //    if (unidadelogadaParam != null)
        //        unidade_logada = JsonConvert.DeserializeObject<int>(unidadelogadaParam.ToString());
        //    if (!string.IsNullOrWhiteSpace(usuarioParam))
        //        usuario = JsonConvert.DeserializeObject<string>(usuarioParam.ToString());
        //    #endregion

        //    ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

        //    filtrotexto = $@"Período: {datainicio} a {datafim} ;";

        //    var info = _repository.GetReportMovimento(ibge, unidade, produto, Convert.ToDateTime(datafim), Convert.ToDateTime(datainicio));
        //    var cabecalho = _repository.GetCabecalhoPaisagem(ibge, (int)unidade_logada);

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
        //    row["impresso_por"] = $"Relatório emitido por {usuario} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        //    row["total_geral"] = info.Count();

        //    dtCabecalho.Rows.Add(row);

        //    //INFO Unidade
        //    var dtmov = new DataTable();
        //    dtmov.Columns.Add(new DataColumn("entrada", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("fabricante", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("falha_transporte", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("falta_energia", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("falta_equipamento", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("frascos_transferidos", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("id_produto", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("id_produtor", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("lote", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("outros_motivos", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("procedimento_inadequado", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("produto", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("quebra_frascos", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("saida", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("saldo_final", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("saldo_inicial", typeof(string)));
        //    dtmov.Columns.Add(new DataColumn("validade_vencida", typeof(string)));

        //    foreach (var item in info)
        //    {
        //        var rowpac = dtmov.NewRow();
        //        rowpac["entrada"] = item.entrada;
        //        rowpac["fabricante"] = item.fabricante;
        //        rowpac["falha_transporte"] = item.falha_transporte;
        //        rowpac["falta_energia"] = item.falta_energia;
        //        rowpac["falta_equipamento"] = item.falta_equipamento;
        //        rowpac["frascos_transferidos"] = item.frascos_transferidos;
        //        rowpac["id_produto"] = item.id_produto;
        //        rowpac["id_produtor"] = item.id_produtor;
        //        rowpac["lote"] = item.lote;
        //        rowpac["outros_motivos"] = item.outros_motivos;
        //        rowpac["procedimento_inadequado"] = item.procedimento_inadequado;
        //        rowpac["produto"] = $"{item.produto} - ({item.sigla})";
        //        rowpac["quebra_frascos"] = item.quebra_frascos;
        //        rowpac["saida"] = item.saida;
        //        rowpac["saldo_final"] = item.saldo_final;
        //        rowpac["saldo_inicial"] = item.saldo_inicial;
        //        rowpac["validade_vencida"] = item.validade_vencida;
        //        dtmov.Rows.Add(rowpac);
        //    }

        //    reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "cabecalhoMovimento", Value = dtCabecalho });
        //    reportOption.ReportModel.DataSources.Add(new ReportDataSource { Name = "ImunizacaoMovimento", Value = dtmov });
        //}

        //public object PostFormReportAction()
        //{
        //    return ReportHelper.ProcessReport(null, this, this._cache);
        //}

        //[HttpPost("PostReportAction")]
        //public object PostReportAction(Dictionary<string, object> jsonResult)
        //{
        //    if (jsonResult.ContainsKey("ibge")) //ibge do municipio logado
        //        ibgeParam = jsonResult["ibge"].ToString();
        //    if (jsonResult.ContainsKey("unidade")) //unidades selecionadas no filtro (formato "3" ou "3,4,5")
        //        unidadeParam = jsonResult["unidade"].ToString();
        //    if (jsonResult.ContainsKey("produto")) //produtos selecionados no filtro (formato "3" ou "3,4,5")
        //        produtoParam = jsonResult["produto"].ToString();
        //    if (jsonResult.ContainsKey("datainicio")) //data de inicio informada no filtro
        //        datainicioParam = jsonResult["datainicio"].ToString();
        //    if (jsonResult.ContainsKey("datafim")) //data de fim informada no filtro
        //        datafimParam = jsonResult["datafim"].ToString();
        //    if (jsonResult.ContainsKey("unidadelogada")) //id da unidade logada - usada para recuperar cabeçalho
        //        unidadelogadaParam = Convert.ToInt32(jsonResult["unidadelogada"].ToString());
        //    if (jsonResult.ContainsKey("usuario")) //nome do usuário logado que deve sair no relatorio
        //        usuarioParam = jsonResult["usuario"].ToString();

        //    return Syncfusion.EJ.ReportViewer.ReportHelper.ProcessReport(jsonResult, this, this._cache);
        //}
    }
}
using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class ReportRepository : IReportRepository
    {
        public IReportCommand _command;
        public ICidadaoCommand _cidadaocommand;
        public ReportRepository(IReportCommand command, ICidadaoCommand cidadaocommand)
        {
            _command = command;
            _cidadaocommand = cidadaocommand;
        }

        public List<ImunizacaoPacienteImunobiologico> GetReportImunizacao(string ibge, DateTime? dataini, DateTime? datafim, string filtros, string filtroini)
        {
            try
            {

                var itens = new List<ImunizacaoPacienteImunobiologico>();

                var sql = _command.GetReportImunizacao;

                if (!string.IsNullOrWhiteSpace(filtroini))
                    sql = sql.Replace("@filtroini", filtroini);
                else
                    sql = sql.Replace("@filtroini", string.Empty);

                if (!string.IsNullOrWhiteSpace(filtros))
                    sql = sql.Replace("@filtro", filtros);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<ImunizacaoPacienteImunobiologico>(sql, new
                               {
                                   @datainicio = dataini,
                                   @datafim = datafim
                               }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalhamentoMovReportViewModel> GetReportMovimento(string ibge, string unidade, string produto, DateTime? datafinal, DateTime? datainicial)
        {
            try
            {
                string filtro1 = string.Empty;
                string filtro2 = string.Empty;
                string filtro3 = string.Empty;
                if (!string.IsNullOrWhiteSpace(unidade))
                {
                    filtro1 += $@" E.ID_UNIDADE IN ({unidade}) AND ";
                    filtro2 += $@" V.ID_UNIDADE IN ({unidade}) AND ";
                    filtro3 += $@" MP.ID_UNIDADE IN ({unidade}) AND ";
                }

                if (!string.IsNullOrWhiteSpace(produto))
                {
                    filtro1 += $@" PLP.ID_PRODUTO IN ({produto}) AND ";
                    filtro2 += $@" V.ID_PRODUTO IN ({produto}) AND ";
                    filtro3 += $@" MP.ID_PRODUTO IN ({produto}) AND ";
                }

                var sqllistaprincipal = _command.GetReportMovimento.Replace("@filtro1", filtro1).Replace("@filtro2", filtro2).Replace("@filtro3", filtro3);
                var sqllistadetalhamento = _command.GetReportDetalhamento.Replace("@filtro1", filtro1).Replace("@filtro2", filtro2).Replace("@filtro3", filtro3);

                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Query<DetalhamentoMovReportViewModel>(sqllistaprincipal, new
                                     {
                                         @datafinal = datafinal
                                     })).ToList();

                var listadetalhamento = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                        conn.Query<DetalhamentoMovReportViewModel>(sqllistadetalhamento, new
                                        {
                                            @datainicial = datainicial,
                                            @datafinal = datafinal
                                        })).ToList();

                foreach (var item in listaprincipal)
                {
                    var itemdetalhe = listadetalhamento.Where(x => x.id_produto == item.id_produto &&
                                                                   x.id_produtor == item.id_produtor)
                                                       .FirstOrDefault();
                    if (itemdetalhe != null)
                    {
                        item.entrada = itemdetalhe.entrada;
                        item.saida = itemdetalhe.saida;
                        item.frascos_transferidos = itemdetalhe.frascos_transferidos;
                        item.quebra_frascos = itemdetalhe.quebra_frascos;
                        item.falta_energia = itemdetalhe.falta_energia;
                        item.falta_equipamento = itemdetalhe.falta_equipamento;
                        item.validade_vencida = itemdetalhe.validade_vencida;
                        item.procedimento_inadequado = itemdetalhe.procedimento_inadequado;
                        item.falha_transporte = itemdetalhe.falha_transporte;
                        item.outros_motivos = itemdetalhe.outros_motivos;

                        item.saldo_inicial = itemdetalhe.saldo_inicial;
                    }
                }

                return listaprincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CartaoVacinaReportViewModel GetCartaoVacinaByIndividuo(string ibge, int id, string sql_estrutura)
        {
            try
            {
                string sql = _cidadaocommand.GetCidadaoById;
                sql = sql.Replace("@csi_codpac", id.ToString());
                sql = sql.Replace("@sql_estrutura", sql_estrutura);

                var cidadao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<Cidadao>(sql));

                var vacinas = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<VacinaReportViewModel>(_command.GetReportCartaoVacina, new { @id = id }));

                var cartaovacina = new CartaoVacinaReportViewModel();
                cartaovacina.itens.AddRange(vacinas);
                cartaovacina.paciente = $"{cidadao.csi_codpac} - {cidadao.csi_nompac}";
                cartaovacina.dataNasc = cidadao.csi_dtnasc?.ToString("dd/MM/yyyy");
                if (cidadao.csi_dtnasc != null)
                {
                    var idade = Helpers.Helper.CalculaIdade(Convert.ToDateTime(cidadao.csi_dtnasc));
                    string mes = string.Empty;
                    if (idade.Item2 != 0)
                        mes = $@"{idade.Item2} meses";

                    cartaovacina.idade = $"{idade.Item1} anos {mes} {idade.Item3} Dias";
                }

                cartaovacina.sexo = cidadao.csi_sexpac;
                cartaovacina.cns = cidadao.csi_ncartao;
                cartaovacina.nomemae = cidadao.csi_maepac;
                cartaovacina.cpf = cidadao.csi_cpfpac;

                return cartaovacina;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BoletimMovimentacao> GetBoletimMovimentacao(string ibge, string unidade, string produto, DateTime? datafinal, DateTime? datainicial)
        {
            try
            {
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(unidade))
                    filtro += $@" AND MP.ID_UNIDADE IN ({unidade})";

                if (!string.IsNullOrWhiteSpace(produto) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MP.ID_PRODUTO IN ({produto})";

                var sql = _command.GetReportBoletimMovimento.Replace("@filtro", filtro);
                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<BoletimMovimentacao>(sql, new
                                    {
                                        @datainicial = datainicial,
                                        @datafinal = datafinal
                                    })).ToList();
                return listaprincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
